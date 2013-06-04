using System;
using System.Collections.Generic;
using System.Linq;
using CCXSharp.HelperClasses;
using Newtonsoft.Json;

namespace CCXSharp.MtGox.Models
{
    [Serializable]
    public class DepthUpdate
    {
        [JsonProperty(PropertyName = "price")]
        public double Price { get; set; }
        [JsonProperty(PropertyName = "type")]
        public OrderType Type { get; set; }
        [JsonProperty(PropertyName = "type_str")]
        public string TypeString { get; set; }
        [JsonProperty(PropertyName = "volume")]
        public double Volume { get; set; }
        [JsonProperty(PropertyName = "price_int")]
        public long PriceInt { get; set; }
        [JsonProperty(PropertyName = "volume_int")]
        public long VolumeInt { get; set; }
        [JsonProperty(PropertyName = "item")]
        public OrderItem Item { get; set; }
        [JsonProperty(PropertyName = "currency")]
        public Currency Currency { get; set; }
        [JsonProperty(PropertyName = "now")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime TimeStamp { get; set; }
        [JsonProperty(PropertyName = "total_volume_int")]
        public long TotalVolume { get; set; }
    }

    public class DepthResponse
    {
        [JsonProperty(PropertyName = "result")]
        public ResponseResult Result { get; set; }
        [JsonProperty(PropertyName = "data")]
        public Depth Depth { get; set; }
    }

    [Serializable]
    public class DepthItem : IComparable<DepthItem>
    {
        [JsonProperty(PropertyName = "price")]
        public double Price { get; set; }
        [JsonProperty(PropertyName = "amount")]
        public double Amount { get; set; }
        [JsonProperty(PropertyName = "price_int")]
        public long PriceInt { get; set; }
        [JsonProperty(PropertyName = "amount_int")]
        public long AmountInt { get; set; }
        [JsonProperty(PropertyName = "stamp")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime TimeStamp { get; set; }
        public OrderType Type { get; set; }

        public int CompareTo(DepthItem that)
        {
            if (this.Price > that.Price) return -1;
            if (this.Price == that.Price) return 0;
            return 1;
        }
    }

    

    public class Depth
    {
        [JsonProperty(PropertyName = "asks")]
        public List<DepthItem> Asks { get; set; }
        [JsonProperty(PropertyName = "bids")]
        public List<DepthItem> Bids { get; set; }
        [JsonProperty(PropertyName = "filter_min_price")]
        public TickerItem FilterMinPrice { get; set; }
        [JsonProperty(PropertyName = "filter_max_price")]
        public TickerItem FilterMaxPrice { get; set; }

        public double WeightedAverageAskPrice
        {
            get
            {
                double weightedOrderPrice = Asks.Sum(od => od.Amount * od.Price);
                double totalVolume = Asks.Sum(od => od.Amount);
                return weightedOrderPrice / totalVolume;
            }
        }

        public double FortyCoinVWAPBuy
        {
            get
            {
                double dblAmountGoal = 40;

                double dblWeightedOrderPrice = 0;
                double dblAmountSoFar = 0;
                double dblAmountLeft = dblAmountGoal;
                double dblAmountForThisFill = 0;
                int cFills = 0;

                foreach (DepthItem di in Asks)
                {
                    if (di.Amount <= dblAmountLeft)
                    {
                        dblAmountForThisFill = di.Amount;
                    }
                    else
                    {
                        dblAmountForThisFill = dblAmountLeft;
                    }
                    ++cFills;
                    dblWeightedOrderPrice += dblAmountForThisFill * di.Price;
                    dblAmountSoFar += dblAmountForThisFill;
                    dblAmountLeft = dblAmountGoal - dblAmountSoFar;
                    
                    if (dblAmountLeft == 0)
                        break;
                }

                return dblWeightedOrderPrice / dblAmountGoal;
            }
        }

        public double FortyCoinVWAPSell
        {
            get
            {
                double dblAmountGoal = 40;

                double dblWeightedOrderPrice = 0;
                double dblAmountSoFar = 0;
                double dblAmountLeft = dblAmountGoal;
                double dblAmountForThisFill = 0;
                int cFills = 0;

                DepthItem bid = null;

                // Traverse bids in reverse
                for (int i = Bids.Count - 1; i >= 0; i--)
                {

                    bid = Bids[i];

                    if (bid.Amount <= dblAmountLeft)
                    {
                        dblAmountForThisFill = bid.Amount;
                    }
                    else
                    {
                        dblAmountForThisFill = dblAmountLeft;
                    }
                    ++cFills;
                    dblWeightedOrderPrice += dblAmountForThisFill * bid.Price;
                    dblAmountSoFar += dblAmountForThisFill;
                    dblAmountLeft = dblAmountGoal - dblAmountSoFar;

                    if (dblAmountLeft == 0)
                        break;
                }

                return dblWeightedOrderPrice / dblAmountGoal;
            }
        }

        public double LowestAsk
        {
            get
            {
                return Asks.Select(od => od.Price).Min();
            }
        }

        public double HighestBid
        {
            get
            {
                return Bids.Select(od => od.Price).Max();
            }
        }
    } 
}
