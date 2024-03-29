﻿using System;
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

namespace AutoTrade
{
    public partial class frmMain : Form
    {
        SqlConnection _connDB = new SqlConnection(ConfigurationManager.AppSettings["DBConnection"]);
        IMtGoxExchange exchangeConnection;

        private BackgroundWorker tickerWorker;
        private Timer tickerTimer;
        private decimal m_decLastPrice;

        public frmMain()
        {
            InitializeComponent();

            try
            {
                _connDB.Open();

                exchangeConnection = new MtGoxExchange();
                //Mediator.Instance.RegisterHandler<MtGoxTicker>("MtGoxTicker", SetTicker);
                //            Mediator.Instance.RegisterHandler<Trade>("Trade", UpdateTradeChartDelegate);
                //            Mediator.Instance.RegisterHandler<DepthUpdate>("DepthUpdate", UpdateDepthChartDelegate);
                exchangeConnection.GoxTickerHandlers += exchangeConnection_GoxTickerHandlers;
                exchangeConnection.GoxDepthHandlers += exchangeConnection_GoxDepthHandlers;
                //            exchangeConnection.GoxDepthStringHandlers += exchangeConnection_GoxDepthStringHandlers;
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
                RecordDepthStats(
                    (decimal)depth.FortyCoinVWAPBuy,
                    (decimal)depth.FortyCoinVWAPSell,
                    System.DateTime.Now.ToLongTimeString());

                int cDepth = Convert.ToInt16(lblDepthCountHTTP.Text.Length == 0 ? "0" : lblDepthCountHTTP.Text);

                ++cDepth;
                lblDepthCountHTTP.Invoke((Action)(() =>
                {
                    lblDepthCountHTTP.Text = cDepth.ToString();
                }));


                lblLastOrderDepthHTTP.Invoke((Action)(() =>
                {
                    lblLastOrderDepthHTTP.Text = System.DateTime.Now.ToString("M/dd/yyyy hh:mm:ss tt");
                }));

                this.lblHighBidHTTP.Invoke((Action)(() =>
                {
                    lblHighBidHTTP.Text = depth.HighestBid.ToString();
                }));

                this.lblLowAskHTTP.Invoke((Action)(() =>
                {
                    lblLowAskHTTP.Text = depth.LowestAsk.ToString();
                }));

            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
            }

        }


        void exchangeConnection_GoxTickerHandlers(Ticker ticker)
        {
            try
            {
                Decimal decBid = Convert.ToDecimal(ticker.Bid.Substring(1, ticker.Bid.Length - 1));
                Decimal decAsk = Convert.ToDecimal(ticker.Ask.Substring(1, ticker.Ask.Length - 1));
                Decimal decPrice = (decBid + decAsk) / 2;

                RecordPrice(decBid, decAsk, decPrice, ticker.TimeStamp.ToString());

                int cPrices = Convert.ToInt16(lblPriceCount.Text.Length == 0 ? "0" : lblPriceCount.Text);
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
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
            }

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

                int cBidAsk = Convert.ToInt16(lblBidAskCount.Text.Length == 0 ? "0" : lblBidAskCount.Text);

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
                lblPollingStatus.Text = "Polling";
                lblPollingStarted.Text = System.DateTime.Now.ToString("M/dd/yyyy hh:mm:ss tt");
                timerCurrent.Enabled = true;
                timerDepth.Enabled = true;
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
                lblPollingStatus.Text = "Not Polling";
                lblPollingStatus.BackColor = Color.Red;
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
            DateTime dtStarted;

            lblCurrentTime.Text = System.DateTime.Now.ToString("M/dd/yyyy hh:mm:ss tt"); ;
            DateTime.TryParse(lblPollingStarted.Text, out dtStarted);
            lblElapsedTime.Text = (System.DateTime.Now - dtStarted).ToString();
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
                Decimal decBid = Convert.ToDecimal(ticker.Bid.Substring(1, ticker.Bid.Length - 1));
                Decimal decAsk = Convert.ToDecimal(ticker.Ask.Substring(1, ticker.Ask.Length - 1));
                Decimal decPrice = (decBid + decAsk) / 2;

                RecordPrice(decBid, decAsk, decPrice, ticker.TimeStamp.ToString());

                int cPrices = Convert.ToInt16(lblPriceCountHTTP.Text.Length == 0 ? "0" : lblPriceCountHTTP.Text);
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
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
            }
        }

    }
}
