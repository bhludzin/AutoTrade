using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CCXSharp.Interfaces;
using CCXSharp.Models;
using CCXSharp.MtGox;
using CCXSharp.MtGox.Models;
using System.Data.SqlClient;
using Wintellect.PowerCollections;
using Cinch;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.Win32;
using Logger;
using System.Configuration;
using System.Net;

namespace AutoTrade
{
    public partial class frmMain : Form
    {
        SqlConnection _connDB = new SqlConnection(ConfigurationManager.AppSettings["DBConnection"]);
        IMtGoxExchange exchangeConnection;

        private BackgroundWorker tickerWorker;
        private Timer tickerTimer;
        private decimal m_decLastPrice;
	    private DateTime dtPollingStart;
	    private DateTime dtLastUpdate;
		private static object lastUpdateLock = new object();
		private const string smsResource = @"https://api.sendhub.com/v1/messages/?username={0}&api_key={1}";
	    private string smsTextDown = @"AutoTrade socket and HTTP APIs are down";
		private string smsTextUp = @"AutoTrade socket and HTTP APIs have been restored";
		private string smsContactId = null;
		private string smsApiNumber = null;
		private string smsApiKey = null;
	    private int smsDelay = 30000;
	    private int smsRenotify = 0;	//0 = do not renotify
		private Timer smsRenotifyTimer = new Timer();
	    private bool isApiUp = true;
		private PollingSource pollingSource = PollingSource.None;

        public frmMain()
        {
            InitializeComponent();

            try
            {
                _connDB.Open();

                exchangeConnection = new MtGoxExchange();
	            int temp;
				if (int.TryParse(ConfigurationManager.AppSettings["FailoverTimeout"], out temp))
					exchangeConnection.FailoverTimeout = temp;
				if (int.TryParse(ConfigurationManager.AppSettings["HTTPApiPriceDelay"], out temp))
					exchangeConnection.HTTPApiPriceDelay = temp;
				if (int.TryParse(ConfigurationManager.AppSettings["HTTPApiDepthDelay"], out temp))
					exchangeConnection.HTTPApiDepthDelay = temp;

				if (int.TryParse(ConfigurationManager.AppSettings["HTTPApiTimeout"], out temp))
					exchangeConnection.HTTPApiTimeout = temp;
				if (int.TryParse(ConfigurationManager.AppSettings["SMSRenotifyPeriod"], out temp))
					smsRenotify = temp;
				smsContactId = ConfigurationManager.AppSettings["SMSNotifyContactId"];
				smsApiKey = ConfigurationManager.AppSettings["SendHubAPIKey"];
				smsApiNumber = ConfigurationManager.AppSettings["SendHubUserNumber"];
				smsTextDown = ConfigurationManager.AppSettings["SMSTextAPIDown"];
				smsTextUp = ConfigurationManager.AppSettings["SMSTextAPIUp"];
                //Mediator.Instance.RegisterHandler<MtGoxTicker>("MtGoxTicker", SetTicker);
                //            Mediator.Instance.RegisterHandler<Trade>("Trade", UpdateTradeChartDelegate);
                //            Mediator.Instance.RegisterHandler<DepthUpdate>("DepthUpdate", UpdateDepthChartDelegate);
                exchangeConnection.GoxTickerHandlers += exchangeConnection_GoxTickerHandlers;
                exchangeConnection.GoxDepthUpdateHandlers += exchangeConnection_GoxDepthHandlers;
				exchangeConnection.GoxFullDepthHandlers += exchangeConnection_GoxFullDepthHandlers;
	            exchangeConnection.GoxSocketFailedHandlers += (sender, args) =>
		            {
						timerDepth.Enabled = false;
		            };
	            exchangeConnection.GoxSocketRestoredHandlers += (sender, args) =>
		            {
						timerDepth.Enabled = true;
		            };
				exchangeConnection.GoxPollingSourceChangedHandlers += exchangeConnection_GoxPollingSourceChangedHandlers;
	            //            exchangeConnection.GoxDepthStringHandlers += exchangeConnection_GoxDepthStringHandlers;

				smsRenotifyTimer.Interval = smsRenotify;
				smsRenotifyTimer.Tick += (sender, args) => NotifySMS(smsTextDown);
				smsRenotifyTimer.Enabled = false;
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
            }
        }

		//BB - polling source change event
		void exchangeConnection_GoxPollingSourceChangedHandlers(PollingSource source)
		{
			pollingSource = source;
			UpdatePollingState(source);
		}

	    private void UpdatePollingState(PollingSource source)
	    {
		    switch (source)
		    {
			    case PollingSource.SocketAPI:
				    lblPollingStatus.BackColor = Color.Green;
				    lblPollingStatus.Text = "Polling (Socket)";
				    if (!isApiUp)
				    {
					    NotifySMS(smsTextUp);
					    if (smsRenotifyTimer.Enabled)
						    smsRenotifyTimer.Enabled = false;
				    }
				    isApiUp = true;
				    break;
			    case PollingSource.HTTPAPI:
				    lblPollingStatus.BackColor = Color.Orange;
				    lblPollingStatus.Text = "Polling (HTTP)";
				    if (!isApiUp)
				    {
					    NotifySMS(smsTextUp);
					    if (smsRenotifyTimer.Enabled)
						    smsRenotifyTimer.Enabled = false;
				    }
				    isApiUp = true;
				    break;
			    case PollingSource.None:
				    lblPollingStatus.BackColor = Color.Red;
				    lblPollingStatus.Text = "Polling Failure";
				    if (isApiUp)
				    {
					    NotifySMS(smsTextDown);
					    //BB - if we want to renotify, enable that timer now
					    if (smsRenotify > 0)
						    smsRenotifyTimer.Enabled = true;
				    }
				    isApiUp = false;
				    break;
		    }
	    }

	    private void NotifySMS(string text)
		{
			try
			{
				var msg = string.Format("{{\"contacts\" : [{0}],\"text\" : \"{1}\"}}", smsContactId, text);
				byte[] msgBytes = Encoding.UTF8.GetBytes(msg);
				var request = (HttpWebRequest)WebRequest.Create(string.Format(smsResource, smsApiNumber, RestSharp.Contrib.HttpUtility.UrlEncode(smsApiKey)));
				request.Method = "POST";
				request.ContentType = "application/json";
				request.ContentLength = msgBytes.Length;

				using (var requestStream = request.GetRequestStream())
				{
					requestStream.Write(msgBytes, 0, msgBytes.Length);
				}

				using (var response = (HttpWebResponse) request.GetResponse())
				{
					if (response.StatusCode != HttpStatusCode.Created)
					{
						Logger.Logger.LogEvent("Unable to send SMS");
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Logger.LogException(ex);
			}
		}

		void exchangeConnection_GoxFullDepthHandlers(Depth depth)
		{
			RecordOrdersFromFullDepth(depth);
			lock (lastUpdateLock)
			{
				dtLastUpdate = DateTime.Now;
			}
		}

	    private void RecordOrdersFromFullDepth(Depth depth)
	    {
		    //BB - we want to break this into a series of orders and feed it into the stored proc
		    var insertedItems = new List<DepthItem>();

		    foreach (var item in depth.Asks)
		    {
			    if (RecordBidAskConditional(Convert.ToDecimal(item.Price), 0, 2, item.TimeStamp.ToString()) > 0)
				    insertedItems.Add(item);
		    }

		    foreach (var item in depth.Bids)
		    {
			    if (RecordBidAskConditional(Convert.ToDecimal(item.Price), 0, 1, item.TimeStamp.ToString()) > 0)
				    insertedItems.Add(item);
		    }

		    UpdateDepthFromHTTP(depth);
		    UpdateOrdersFromHTTP(insertedItems);
	    }

	    private void UpdateOrdersFromHTTP(List<DepthItem> insertedItems)
		{
			try
			{
				foreach (var item in insertedItems)
				{
					if (item.Type == OrderType.Ask)
					{
						if (item.Price > (double) (2 * m_decLastPrice)) return;
						//if (depthUpdate.Volume < (double) 0.02) return;
						lblLastAskHTTP.Invoke((Action) (() =>
							{
								lblLastAskHTTP.Text = item.Price.ToString();
							}));

					}
					else
					{
						if (item.Price < (double)(0.5 * (double)m_decLastPrice)) return;
						//if (depthUpdate.Volume < (double) 0.02) return;
						lblLastBidHTTP.Invoke((Action)(() =>
							{
								lblLastBidHTTP.Text = item.Price.ToString();
							}));
					}

					lblLastResultOrderHTTP.Invoke((Action) (() =>
						{
							lblLastResultOrderHTTP.Text = item.TimeStamp.ToString("M/dd/yyyy hh:mm:ss tt");
						}));

					int cBidAsk = Convert.ToInt32(lblOrderCountHTTP.Text.Length == 0 ? "0" : lblOrderCountHTTP.Text);

					++cBidAsk;
					lblOrderCountHTTP.Invoke((Action)(() =>
						{
							lblOrderCountHTTP.Text = cBidAsk.ToString();
						}));
				}
			}
			catch (Exception ex)
			{
				Logger.Logger.LogException(ex);
			}
		}

        private void timerDepth_Tick(object sender, EventArgs e)
        {
            try
            {
	            Depth depth = exchangeConnection.GetDepth(Currency.USD); // get the current depth

	            UpdateDepthFromHTTP(depth);
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
            }
        }

	    private void UpdateDepthFromHTTP(Depth depth)
	    {
		    RecordDepthStats(
			    (decimal) depth.FortyCoinVWAPBuy,
				(decimal) depth.FortyCoinVWAPSell,
				System.DateTime.Now.ToLongTimeString());

		    int cDepth = Convert.ToInt32(lblDepthCountHTTP.Text.Length == 0 ? "0" : lblDepthCountHTTP.Text);

		    ++cDepth;
		    lblDepthCountHTTP.Invoke((Action) (() => { lblDepthCountHTTP.Text = cDepth.ToString(); }));


		    lblLastOrderDepthHTTP.Invoke(
			    (Action) (() => { lblLastOrderDepthHTTP.Text = System.DateTime.Now.ToString("M/dd/yyyy hh:mm:ss tt"); }));

		    this.lblHighBidHTTP.Invoke((Action) (() => { lblHighBidHTTP.Text = depth.HighestBid.ToString(); }));

		    this.lblLowAskHTTP.Invoke((Action) (() => { lblLowAskHTTP.Text = depth.LowestAsk.ToString(); }));
	    }

	    void exchangeConnection_GoxTickerHandlers(Ticker ticker)
        {
            try
            {
				switch (ticker.Source)
				{
					case PollingSource.SocketAPI:
						UpdateFromSocketApi(ticker);
						break;
					case PollingSource.HTTPAPI:
						UpdatePriceFromHTTPApi(ticker);
						break;
				}
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
            }

			lock (lastUpdateLock)
			{
				dtLastUpdate = DateTime.Now;
			}
        }

		void UpdateFromSocketApi(Ticker ticker)
		{
			Decimal decBid = Convert.ToDecimal(ticker.Bid.Substring(1, ticker.Bid.Length - 1));
			Decimal decAsk = Convert.ToDecimal(ticker.Ask.Substring(1, ticker.Ask.Length - 1));
			Decimal decPrice = (decBid + decAsk) / 2;

			RecordPrice(decBid, decAsk, decPrice, ticker.TimeStamp.ToString());

			int cPrices = Convert.ToInt32(lblPriceCount.Text.Length == 0 ? "0" : lblPriceCount.Text);
			++cPrices;
			lblPriceCount.Invoke((Action)(() =>
			{
				lblPriceCount.Text = cPrices.ToString();
			}));

			lblLastResult.Invoke((Action)(() =>
			{
				lblLastResult.Text = System.DateTime.Now.ToString("M/dd/yyyy hh:mm:ss tt");
			}));

			lblPrice.Invoke((Action)(() =>
			{
				lblPrice.Text = ticker.Last.ToString();
			}));

			m_decLastPrice = Convert.ToDecimal(ticker.Last.Substring(1, ticker.Last.Length - 1));
		}

		void UpdatePriceFromHTTPApi(Ticker ticker)
		{
			Decimal decBid = Convert.ToDecimal(ticker.Bid.Substring(1, ticker.Bid.Length - 1));
			Decimal decAsk = Convert.ToDecimal(ticker.Ask.Substring(1, ticker.Ask.Length - 1));
			Decimal decPrice = (decBid + decAsk) / 2;

			RecordPrice(decBid, decAsk, decPrice, ticker.TimeStamp.ToString());

			int cPrices = Convert.ToInt32(lblPriceCountHTTP.Text.Length == 0 ? "0" : lblPriceCountHTTP.Text);
			++cPrices;
			lblPriceCountHTTP.Invoke((Action)(() =>
			{
				lblPriceCountHTTP.Text = cPrices.ToString();
			}));

			lblLastResultPriceHTTP.Invoke((Action)(() =>
			{
				lblLastResultPriceHTTP.Text = System.DateTime.Now.ToString("M/dd/yyyy hh:mm:ss tt");
			}));

			lblLastPriceHTTP.Invoke((Action)(() =>
			{
				lblLastPriceHTTP.Text = ticker.Last.ToString();
			}));

			m_decLastPrice = Convert.ToDecimal(ticker.Last.Substring(1, ticker.Last.Length - 1));
		}

        void exchangeConnection_GoxDepthHandlers(DepthUpdate depthUpdate)
        {
            try
            {
                if (depthUpdate.Currency != Currency.USD) return;
                if (depthUpdate.Volume <= 0) return;


                int idOrderType;
                if (depthUpdate.TypeString == "ask")
                {
                    if (depthUpdate.Price > (double)(2 * m_decLastPrice)) return;
                    if (depthUpdate.Volume < (double)0.02) return;
                    idOrderType = 2;
                    lblAsk.Invoke((Action)(() =>
                    {
                        lblAsk.Text = depthUpdate.Price.ToString();
                    }));

                }
                else
                {
                    if (depthUpdate.Price < (double)(0.5 * (double)m_decLastPrice)) return;
                    if (depthUpdate.Volume < (double)0.02) return;
                    idOrderType = 1;
                    lblBid.Invoke((Action)(() =>
                    {
                        lblBid.Text = depthUpdate.Price.ToString();
                    }));
                }

                lblLastResultOrder.Invoke((Action)(() =>
                {
                    lblLastResultOrder.Text = System.DateTime.Now.ToString("M/dd/yyyy hh:mm:ss tt");
                }));

                Decimal decPrice = Convert.ToDecimal(depthUpdate.Price);
                Decimal decVolume = Convert.ToDecimal(depthUpdate.Volume);

                RecordBidAsk(decPrice, decVolume, idOrderType, depthUpdate.TimeStamp.ToString());

                int cBidAsk = Convert.ToInt32(lblBidAskCount.Text.Length == 0 ? "0" : lblBidAskCount.Text);

                ++cBidAsk;
                lblBidAskCount.Invoke((Action)(() =>
                {
                    lblBidAskCount.Text = cBidAsk.ToString();
                }));

                lblLastResult.Invoke((Action)(() =>
                {
                    lblLastResult.Text = System.DateTime.Now.ToString("M/dd/yyyy hh:mm:ss tt");
                }));
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
            }

			lock (lastUpdateLock)
			{
				dtLastUpdate = DateTime.Now;
			}
        }

        void exchangeConnection_GoxDepthStringHandlers(string s)
        {
            try
            {
                MessageBox.Show(s);
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
            }
        }


        public void RecordDepthStats(
                    Decimal decFortyCoinVWAPBuy, 
                    Decimal decFortyCoinVWAPSell,
                    string sDepthDate)
        {
            try
            {
                SqlCommand myCommand = new SqlCommand("C_BITCOIN_DEPTH_INS", _connDB);
                myCommand.CommandType = CommandType.StoredProcedure;
		        myCommand.Parameters.Add(new SqlParameter("@decFortyCoinVWAPBuy", decFortyCoinVWAPBuy));
		        myCommand.Parameters.Add(new SqlParameter("@decFortyCoinVWAPSell", decFortyCoinVWAPSell));
                SqlParameter prmDepthDate = new SqlParameter("@dtDepthEST", SqlDbType.DateTime);
                prmDepthDate.Value = sDepthDate;
                myCommand.Parameters.Add(prmDepthDate);
                myCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
            }
        }


        public void RecordBidAsk(Decimal decPrice, Decimal decVolume, int idOrderType, string sOrderDate)
        {
            try
            {
                SqlCommand myCommand = new SqlCommand("C_BITCOIN_ORDER_INS", _connDB);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add(new SqlParameter("@decPrice", decPrice));
                myCommand.Parameters.Add(new SqlParameter("@decVolume", decVolume));
                myCommand.Parameters.Add(new SqlParameter("@idOrderType", idOrderType));
                SqlParameter prmOrderDate = new SqlParameter("@dtOrderUTC", SqlDbType.DateTime);
                prmOrderDate.Value = sOrderDate;
                myCommand.Parameters.Add(prmOrderDate);
                myCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
            }
        }

		public int RecordBidAskConditional(Decimal decPrice, Decimal decVolume, int idOrderType, string sOrderDate)
		{
			try
			{
				SqlCommand myCommand = new SqlCommand("C_BITCOIN_ORDER_CONDITIONAL_INS", _connDB);
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.Parameters.Add(new SqlParameter("@decPrice", decPrice));
				myCommand.Parameters.Add(new SqlParameter("@decVolume", decVolume));
				myCommand.Parameters.Add(new SqlParameter("@idOrderType", idOrderType));
				SqlParameter prmOrderDate = new SqlParameter("@dtOrderUTC", SqlDbType.DateTime);
				prmOrderDate.Value = sOrderDate;
				myCommand.Parameters.Add(prmOrderDate);
				return myCommand.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				Logger.Logger.LogException(ex);
			}

			return 0;
		}

        public void RecordPrice(Decimal decBid, Decimal decAsk, Decimal decPrice, string sPriceDate)
        {
            try
            {
                SqlCommand myCommand = new SqlCommand("C_BITCOIN_PRICE_INS", _connDB);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add(new SqlParameter("@decBid", decBid));
                myCommand.Parameters.Add(new SqlParameter("@decAsk", decAsk));
                myCommand.Parameters.Add(new SqlParameter("@decPrice", decPrice));
                SqlParameter prmPriceDate = new SqlParameter("@dtPriceUTC", SqlDbType.DateTime);
                prmPriceDate.Value = sPriceDate;
                myCommand.Parameters.Add(prmPriceDate);
                myCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
            }
        }


		private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                exchangeConnection.StopDataPoller();
                exchangeConnection.StartDataPoller();
                lblPollingStatus.BackColor = Color.Green;
                lblPollingStatus.Text = "Polling (Socket)";
	            dtPollingStart = DateTime.Now;
                lblPollingStarted.Text = dtPollingStart.ToString("M/dd/yyyy hh:mm:ss tt");
                timerCurrent.Enabled = true;
                timerDepth.Enabled = true;
	            isApiUp = true;
	            lock (lastUpdateLock)
		            dtLastUpdate = DateTime.Now;
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                exchangeConnection.StopDataPoller();
                lblPollingStatus.Text = "Polling Off";
                lblPollingStatus.BackColor = Color.DarkRed;
                timerCurrent.Enabled = false;
                timerDepth.Enabled = false;
                lblBidAskCount.Text = "";
                lblPriceCount.Text = "";
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
            }
        }

        private void timerCurrent_Tick(object sender, EventArgs e)
        {
            try
            {
				lblCurrentTime.Text = DateTime.Now.ToString("M/dd/yyyy hh:mm:ss tt");
				lblElapsedTime.Text = (DateTime.Now - dtPollingStart).ToString();
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
            }
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btnManualRequestPriceHTTP_Click(object sender, EventArgs e)
        {
            Ticker ticker = exchangeConnection.GetTicker(Currency.USD); // current ticker
            try
            {
				UpdatePriceFromHTTPApi(ticker);
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
            }
        }

		private void btnManualRequestOrderHTTP_Click(object sender, EventArgs e)
		{
			Task t = Task.Factory.StartNew(() => RecordOrdersFromFullDepth(exchangeConnection.GetDepth(Currency.USD)));
			t.Wait();
		}
    }
}
