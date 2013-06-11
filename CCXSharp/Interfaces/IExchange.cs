using System;
using System.Collections.Generic;
using CCXSharp.Models;
using CCXSharp.MtGox.Models;

namespace CCXSharp.Interfaces
{
    public delegate void GoxExceptionHandler(Exception ex);
    public delegate void GoxOrdersHandler(List<Order> orders);
    public delegate void GoxAccountInfoHandler(MtGoxAccountInfo info);
    public delegate void GoxTickerHandler(Ticker t);
    public delegate void GoxTradeHandler(Trade t);
    public delegate void GoxDepthHandler(DepthUpdate d);
    public delegate void GoxLagHandler(LagChannelResponse l);
    public delegate void GoxWalletHandler(WalletResponse wallet);
	public delegate void GoxOrderHandler(Order o);
    public delegate void GoxDepthStringHandler(string s);

	public interface IExchange
	{
		Ticker GetTicker(Currency currency);
		Depth GetDepth(Currency currency);
		CurrencyInfo GetCurrencyInfo(Currency currency);
		MtGoxAccountInfo GetAccountInfo();
		List<Order> GetOrders();
		OrderCreateResponse CreateOrder(Currency currency, OrderType type, double amount, double? price);
		OrderCancelResponse CancelOrder(Guid oid);
		void StartDataPoller();
		void StopDataPoller();
		bool ValidApiKey { get; }
		int FailoverTimeout { get; set; }
	}

	public interface IMtGoxExchange : IExchange
    {
        Lag GetLag();
        TradeResponse GetTrades(Currency currency);
        TradeResponse GetTrades(Currency currency, DateTime? fromDateTime);
        string APIKey { get; set; }
        string APISecret { get; set; }
        string GetIdKey();
        bool SocketOpen { get; }
		int HTTPApiDelay { get; set; }
		event GoxExceptionHandler GoxExceptionHandlers;
        event GoxOrdersHandler GoxOrdersHandlers;
        event GoxAccountInfoHandler GoxAccountInfoHandlers;
        event GoxTickerHandler GoxTickerHandlers;
        event GoxTradeHandler GoxTradeHandlers;
        event GoxDepthHandler GoxDepthHandlers;
        event GoxDepthStringHandler GoxDepthStringHandlers;
        event GoxLagHandler GoxLagHandlers;
        event GoxWalletHandler GoxWalletHandlers;
        event GoxOrderHandler GoxOrderHandlers;
    }

    public interface IBitfloorExchange : IExchange
    {
        string APIKey { get; set; }
        string APISecret { get; set; }
        string APIPassphrase { get; set; }
    }
}
