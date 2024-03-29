﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using CCXSharp.BaseClasses;
using CCXSharp.Models;
using CCXSharp.MtGox.Models;
using RestSharp;
using Logger;

namespace CCXSharp.MtGox
{
    public abstract class MtGoxTradeCommands
    {
        private readonly IMtGoxRestClient restClient = new MtGoxRestClient();

        public bool ValidApiKey
        {
            get { return restClient.ValidApiKey; }
        }

        public string APIKey
        {
            get { return restClient.APIKey; }
            set { restClient.APIKey = value; }
        }
        public string APISecret 
        {
            get { return restClient.APISecret; }
            set { restClient.APISecret = value; }
        }

        public Ticker GetTicker(Currency currency)
        {
            try
            {
                MtGoxTickerResponse tickerResponse = restClient.GetResponse<MtGoxTickerResponse>(String.Format("BTC{0}/money/ticker", currency.ToString()), Method.GET, null, AccessType.Public);
                if (tickerResponse.Ticker == null)
                    throw new Exception("Failed to deserialize JSON object of type " + typeof(MtGoxTicker) + ". " + MtGoxRestClient.lastResponse);
                return new Ticker(tickerResponse.Ticker);
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
                return new Ticker();
            }
        }

        public Depth GetDepth(Currency currency)
        {
            try
            {
                DepthResponse depthResponse = restClient.GetResponse<DepthResponse>(String.Format("BTC{0}/money/depth/fetch", currency.ToString()), Method.GET, null, AccessType.Public);
                if (depthResponse.Depth == null)
                    throw new Exception("Failed to deserialize JSON object of type " + typeof(Depth) + ". " + MtGoxRestClient.lastResponse);
                return depthResponse.Depth;
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
                return new Depth();
            }
        }

        public CurrencyInfo GetCurrencyInfo(Currency currency)
        {
            try
            {
                CurrencyInfoResponse currencyInfoResponse = restClient.GetResponse<CurrencyInfoResponse>(String.Format("BTC{0}/money/currency", currency.ToString()), Method.POST, null, AccessType.Public);
                if (currencyInfoResponse.CurrencyInfo == null)
                    throw new Exception("Failed to deserialize JSON object of type " + typeof(CurrencyInfo) + ". " + MtGoxRestClient.lastResponse);
                return currencyInfoResponse.CurrencyInfo;
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
                return new CurrencyInfo();
            }
        }

        public MtGoxAccountInfo GetAccountInfo()
        {
            try
            {
                MtGoxAccountInfoResponse mtGoxAccountInfoResponse = restClient.GetResponse<MtGoxAccountInfoResponse>("money/info", Method.POST, null, AccessType.Private);
                if (mtGoxAccountInfoResponse.AccountInfo == null)
                    throw new Exception("Failed to deserialize JSON object of type " + typeof(MtGoxAccountInfo) + ". " + MtGoxRestClient.lastResponse);
                return mtGoxAccountInfoResponse.AccountInfo;
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
                return new MtGoxAccountInfo();
            }
        }

        public string GetIdKey()
        {
            try
            {
                IdKeyResponse idKeyResponse = restClient.GetResponse<IdKeyResponse>("money/idkey", Method.POST, null, AccessType.Private);
                if (string.IsNullOrEmpty(idKeyResponse.IdKey))
                    throw new Exception("Failed to deserialize JSON object of type IdKey. " + MtGoxRestClient.lastResponse);
                return idKeyResponse.IdKey;
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
                return "";
            }
        }

        public List<Order> GetOrders()
        {
            try
            {
                OrderResponse orderResponse = restClient.GetResponse<OrderResponse>("money/orders", Method.POST, null, AccessType.Private);
                if (orderResponse.Orders == null)
                    throw new Exception("Failed to deserialize JSON object of type " + typeof(List<Order>) + ". " + MtGoxRestClient.lastResponse);
                return orderResponse.Orders;
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
                return null;
            }
        }

        public OrderCreateResponse CreateOrder(Currency currency, OrderType type, double amount, double? price)
        {
            try
            {
                NameValueCollection nvc = new NameValueCollection
            {
                {"type", type.ToString().ToLower()},
                {"amount_int", (Math.Round(amount, 8)*100000000).ToString()}
            };
                if (price != null)
                    nvc.Add("price_int", (Math.Round((double)price, 5) * 100000).ToString());
                return restClient.GetResponse<OrderCreateResponse>(String.Format("BTC{0}/money/order/add", currency.ToString()), Method.POST, nvc, AccessType.Private);
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
                return null;
            }
        }

        public OrderCancelResponse CancelOrder(Guid oid)
        {
            try
            {
                NameValueCollection nvc = new NameValueCollection { { "oid", oid.ToString() } };
                return restClient.GetResponse<OrderCancelResponse>(String.Format("money/order/cancel"), Method.POST, nvc, AccessType.Private);
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
                return null;
            }
        }

        public Lag GetLag()
        {
            try
            {
                return restClient.GetResponse<LagResponse>(String.Format("money/order/lag"), Method.POST, null, AccessType.Public).Lag;
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
                return null;
            }

        }

        public TradeResponse GetTrades(Currency currency)
        {
            return GetTrades(currency, null);
        }

        public TradeResponse GetTrades(Currency currency, DateTime? fromDateTime)
        {
            try
            {
                TradeResponse tradeResponse;

                if (fromDateTime.HasValue)
                {
                    DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    TimeSpan span = ((DateTime)fromDateTime - epoch);
                    Int64 since = (Int64)Decimal.Round(Decimal.Divide(span.Ticks, 10), 0);

                    tradeResponse = restClient.GetResponse<TradeResponse>(String.Format("BTC{0}/money/trades/fetch?since={1}", currency.ToString(), since), Method.GET, null, AccessType.Public);
                }
                else
                {
                    tradeResponse = restClient.GetResponse<TradeResponse>(String.Format("BTC{0}/money/trades/fetch", currency.ToString()), Method.GET, null, AccessType.Public);
                }

                if (tradeResponse.Trades == null)
                    throw new Exception("Failed to deserialize JSON object of type " + typeof(List<Trade>) + ". " + MtGoxRestClient.lastResponse);
                return tradeResponse;
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
                return null;
            }
        }
    }
}
