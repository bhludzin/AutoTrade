﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using CCXSharp.Interfaces;
using CCXSharp.Models;
using CCXSharp.MtGox.Models;
using Cinch;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SocketIOClient;
using SocketIOClient.Messages;
using Logger;

namespace CCXSharp.MtGox
{
    public class MtGoxExchange : MtGoxTradeCommands, IMtGoxExchange
    {
        private Client _client;
        private bool ConnectionRunning { get; set; }
        private bool m_bRetry = false;
        private const string TradesChannel = "dbf1dee9-4f2e-4a08-8cb7-748919a71b21";
        private const string TickerChannel = "d5f06780-30a8-4a48-a2f8-7ed181b4a13f";
        private const string DepthChannel = "24e67e0d-1cad-4cc0-9e7a-f8523ef460fe";
        private string OrderChannel { get; set; }
        private const string LagChannel = "85174711-be64-4de1-b783-0628995d7914";
        private DateTime LastMessage = DateTime.Now;
        public bool SocketOpen
        {
            get 
            {
                try
                {
                    return (DateTime.Now - LastMessage).TotalMilliseconds > 5000;
                }
                catch (Exception ex)
                {
                    Logger.Logger.LogException(ex);
                    return false;
                }
            }
        }


        //Notifications
        //    Mediator.Instance.NotifyColleagues("Exception", ex);
        //    Mediator.Instance.NotifyColleagues("Orders", GetOrders());
        //    Mediator.Instance.NotifyColleagues("AccountInfo", GetAccountInfo());
        //    Mediator.Instance.NotifyColleagues("Ticker", new Ticker(JsonConvert.DeserializeObject<MtGoxTicker>(o["ticker"].ToString())));
        //    Mediator.Instance.NotifyColleagues("Trade", JsonConvert.DeserializeObject<Trade>(o["trade"].ToString()));
        //    Mediator.Instance.NotifyColleagues("DepthUpdate", JsonConvert.DeserializeObject<DepthUpdate>(o["depth"].ToString()));
        //    Mediator.Instance.NotifyColleagues("Lag", new LagChannelResponse{age = 0, qid = new Guid(), stamp = DateTime.Now});
        //    Mediator.Instance.NotifyColleagues("Lag", JsonConvert.DeserializeObject<LagChannelResponse>(o["lag"].ToString()));
        //    Mediator.Instance.NotifyColleagues("Wallet", JsonConvert.DeserializeObject<WalletResponse>(o["wallet"].ToString()));
        //    Mediator.Instance.NotifyColleagues("Order", JsonConvert.DeserializeObject<Order>(o["user_order"].ToString()));


        public event GoxExceptionHandler GoxExceptionHandlers;
        //public delegate void GoxExceptionHandler(Exception ex);

        public event GoxOrdersHandler GoxOrdersHandlers;
        //public delegate void GoxOrdersHandler(List<Order> orders);

        public event GoxAccountInfoHandler GoxAccountInfoHandlers;
        //public delegate void GoxAccountInfoHandler(MtGoxAccountInfo info);

        public event GoxTickerHandler GoxTickerHandlers;
        //public delegate void GoxTickerHandler(Ticker t);

        public event GoxTradeHandler GoxTradeHandlers;
        //public delegate void GoxTradeHandler(Trade t);

        public event GoxDepthHandler GoxDepthHandlers;
        public event GoxDepthStringHandler GoxDepthStringHandlers;
        //public delegate void GoxDepthHandler(DepthUpdate d);

        public event GoxLagHandler GoxLagHandlers;
        //public delegate void GoxLagHandler(LagChannelResponse l);

        public event GoxWalletHandler GoxWalletHandlers;
        //public delegate void GoxWalletHandler(WalletResponse wallet);

        public event GoxOrderHandler GoxOrderHandlers;
        //public delegate void GoxOrderHandler(Order o);


        //if (GoxOrdersHandlers != null) { GoxOrdersHandlers(GetOrders()); }
     

        ~MtGoxExchange()
        {
            StopDataPoller();
        }

        public void StartDataPoller()
        {
            try
            {
                m_bRetry = false;
                ConnectionRunning = true;
                Task.Factory.StartNew(DataPollerLoop).ContinueWith(t =>
                {
                    if (t.Exception != null)
                        if (GoxExceptionHandlers != null) { GoxExceptionHandlers(t.Exception.InnerException); }
                    //Mediator.Instance.NotifyColleagues("Exception", t.Exception.InnerException);
                }, TaskContinuationOptions.OnlyOnFaulted);
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
            }
        }

        public void StopDataPoller()
        {
            try
            {
                ConnectionRunning = false;
                if (_client != null && _client.IsConnected)
                    _client.Close();
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
            }
        }

        private void DataPollerLoop()
        {
            try
            {
                while (true)
                {
                    try
                    {
                        InitializeSocket();
                        Stopwatch sw = new Stopwatch();
                        sw.Start();
                        while (ConnectionRunning)
                        {
                            try
                            {
                                if (!SocketOpen && sw.ElapsedMilliseconds > 30000)
                                {
                                    sw.Restart();
                                    if (GoxOrdersHandlers != null) { GoxOrdersHandlers(GetOrders()); }
                                    //Mediator.Instance.NotifyColleagues("Orders", GetOrders());
                                    if (GoxAccountInfoHandlers != null) { GoxAccountInfoHandlers(GetAccountInfo()); }
                                    //Mediator.Instance.NotifyColleagues("AccountInfo", GetAccountInfo());
                                    InitializeSocket();
                                }
                                Thread.Sleep(100);
                            }
                            catch (Exception ex)
                            {
                                Logger.Logger.LogException(ex);
                                break;
                                //if (GoxExceptionHandlers != null)
                                //{ GoxExceptionHandlers(ex); }
                                //Mediator.Instance.NotifyColleagues("Exception", ex);
                            }
                            if (m_bRetry)
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Logger.LogException(ex);
                        //if (GoxExceptionHandlers != null) { GoxExceptionHandlers(ex); }
                        //                Mediator.Instance.NotifyColleagues("Exception", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
            }
        }

        public void InitializeSocket()
        {
            try
            {
                if (_client != null)
                    _client.Close();
                _client = new Client(@"https://socketio.mtgox.com");
                _client.Message += SocketClientMessage;
                _client.Error += SocketClientError;
                _client.SocketConnectionClosed += SocketClientConnectionClosed;

                if (ValidApiKey)
                {
                    SubscribeUserChannel subUser = new SubscribeUserChannel(GetIdKey());
                    JSONMessage userMsg = new JSONMessage(subUser, endpoint: "/mtgox") { Json = new JsonEncodedEventMessage("message", subUser) };
                    SubscribeLag subLag = new SubscribeLag();
                    JSONMessage lagMsg = new JSONMessage(subLag, endpoint: "/mtgox") { Json = new JsonEncodedEventMessage("message", subLag) };
                    _client.On("connect", data =>
                    {
                        _client.Send(userMsg);
                        _client.Send(lagMsg);
                    });
                }

                _client.Connect("/mtgox");
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
            }

        }

        private void SocketClientError(object sender, ErrorEventArgs e)
        {
            try
            {
                m_bRetry = true;
                //if (GoxExceptionHandlers != null)
                //{ GoxExceptionHandlers(new Exception("SocketError: " + e.Message)); }
                //            Mediator.Instance.NotifyColleagues("Exception", new Exception("SocketError: " + e.Message));
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
            }
        }

        private void SocketClientConnectionClosed(object sender, EventArgs e)
        {
            try
            {
                m_bRetry = true;
                //if (GoxExceptionHandlers != null)
                //{ GoxExceptionHandlers(new Exception("Socket connection closed")); }
                //            Mediator.Instance.NotifyColleagues("Exception", new Exception("Socket connection closed"));
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
            }
        }

        private void SocketClientMessage(object sender, MessageEventArgs args)
        {
            try
            {
                if (args.Message.MessageText != null)
                {
                    LastMessage = DateTime.Now;
                    JContainer o = (JContainer)JsonConvert.DeserializeObject(args.Message.MessageText);
                    string op = o["op"].Value<string>();
                    if (op == "remark")
                        return;
                    string channel = o["channel"].Value<string>();
                    if (op.Equals("private"))
                    {
                        switch (channel)
                        {
                            case TickerChannel:
                                if (GoxTickerHandlers != null) { GoxTickerHandlers(new Ticker(JsonConvert.DeserializeObject<MtGoxTicker>(o["ticker"].ToString()))); }
                                //Mediator.Instance.NotifyColleagues("Ticker", new Ticker(JsonConvert.DeserializeObject<MtGoxTicker>(o["ticker"].ToString())));
                                break;
                            case TradesChannel:
                                if (GoxTradeHandlers != null) { GoxTradeHandlers(JsonConvert.DeserializeObject<Trade>(o["trade"].ToString())); }
                                //Mediator.Instance.NotifyColleagues("Trade", JsonConvert.DeserializeObject<Trade>(o["trade"].ToString()));
                                break;
                            case DepthChannel:
                                if (GoxDepthHandlers != null) { GoxDepthHandlers(JsonConvert.DeserializeObject<DepthUpdate>(o["depth"].ToString())); }
                                if (GoxDepthStringHandlers != null) { GoxDepthStringHandlers(args.Message.MessageText); }
                                //Mediator.Instance.NotifyColleagues("DepthUpdate", JsonConvert.DeserializeObject<DepthUpdate>(o["depth"].ToString()));
                                break;
                            case LagChannel:
                                if ((long)o["lag"]["age"] == 0 || (o["lag"]["qid"] == null && o["lag"]["stamp"] == null))
                                    if (GoxLagHandlers != null) { GoxLagHandlers(new LagChannelResponse { age = 0, qid = new Guid(), stamp = DateTime.Now }); }
                                    //Mediator.Instance.NotifyColleagues("Lag", new LagChannelResponse { age = 0, qid = new Guid(), stamp = DateTime.Now });
                                    else
                                        if (GoxLagHandlers != null) { GoxLagHandlers(JsonConvert.DeserializeObject<LagChannelResponse>(o["lag"].ToString())); }
                                //Mediator.Instance.NotifyColleagues("Lag", JsonConvert.DeserializeObject<LagChannelResponse>(o["lag"].ToString()));
                                break;
                            default:
                                if (channel == OrderChannel)
                                {
                                    if (o["private"].Value<String>().Equals("wallet"))
                                    {
                                        if (GoxWalletHandlers != null) { GoxWalletHandlers(JsonConvert.DeserializeObject<WalletResponse>(o["wallet"].ToString())); }
                                        //Mediator.Instance.NotifyColleagues("Wallet", JsonConvert.DeserializeObject<WalletResponse>(o["wallet"].ToString()));
                                    }
                                    else if (o["private"].Value<String>().Equals("user_order"))
                                    {
                                        if (GoxOrderHandlers != null) { GoxOrderHandlers(JsonConvert.DeserializeObject<Order>(o["user_order"].ToString())); }
                                        //Mediator.Instance.NotifyColleagues("Order", JsonConvert.DeserializeObject<Order>(o["user_order"].ToString()));
                                    }
                                }
                                break;
                        }
                    }
                    else
                    {
                        if (op == "subscribe" && channel != TickerChannel && channel != TradesChannel && channel != DepthChannel && channel != LagChannel)
                            OrderChannel = channel;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
            }
        }

        [Serializable]
        public class SubscribeUserChannel
        {
            public string op = "mtgox.subscribe";
            public string key { get; set; }

            public SubscribeUserChannel(string idkey)
            {
                key = idkey;
            }
        }

        [Serializable]
        public class SubscribeLag
        {
            public string op = "mtgox.subscribe";
            public string type = "lag";
        }
    }
}
