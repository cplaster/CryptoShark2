using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;

namespace CryptoShark2.Exchanges.Bittrex
{
    #region JSON Translators

    public partial class Coin : IListResponse<Coin, CoinData>
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("result")]
        public List<CoinData> Data { get; set; }
    }

    public partial class CoinData
    {
        [JsonProperty("Currency")]
        public string Currency { get; set; }

        [JsonProperty("CurrencyLong")]
        public string CurrencyLong { get; set; }

        [JsonProperty("MinConfirmation")]
        public long MinConfirmation { get; set; }

        [JsonProperty("TxFee")]
        public double TxFee { get; set; }

        [JsonProperty("IsActive")]
        public bool IsActive { get; set; }

        [JsonProperty("CoinType")]
        public string CoinType { get; set; }

        [JsonProperty("BaseAddress")]
        public string BaseAddress { get; set; }
    }

    public partial class Coin
    {
        public Coin FromJson(string json) => JsonConvert.DeserializeObject<Coin>(json, new JsonCustomConverter());
        public string ToJson() => JsonConvert.SerializeObject(this, JsonDefaultConverter.Settings);
    }

    public static class CoinSerialize
    {
        public static string ToJson(this Coin self) => JsonConvert.SerializeObject(self, JsonDefaultConverter.Settings);
    }

    public static class CoinDeserialize
    {
        public static Coin FromJson(string json) => JsonConvert.DeserializeObject<Coin>(json, new JsonCustomConverter());
    }

    public partial class Markets : IListResponse<Markets, MarketData>
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("result")]
        public List<MarketData> Data { get; set; }
    }

    public partial class MarketData
    {
        [JsonProperty("MarketCurrency")]
        public string MarketCurrency { get; set; }

        [JsonProperty("BaseCurrency")]
        public string BaseCurrency { get; set; }

        [JsonProperty("MarketCurrencyLong")]
        public string MarketCurrencyLong { get; set; }

        [JsonProperty("BaseCurrencyLong")]
        public string BaseCurrencyLong { get; set; }

        [JsonProperty("MinTradeSize")]
        public double MinTradeSize { get; set; }

        [JsonProperty("MarketName")]
        public string MarketName { get; set; }

        [JsonProperty("IsActive")]
        public bool IsActive { get; set; }

        [JsonProperty("Created")]
        public DateTime Created { get; set; }

        [JsonProperty("TradeFee")]
        public double TradeFee { get; set; }
    }

    public partial class Markets
    {
        public Markets FromJson(string json) => JsonConvert.DeserializeObject<Markets>(json, new JsonCustomConverter());
        public string ToJson() => JsonConvert.SerializeObject(this, JsonDefaultConverter.Settings);
    }

    public static class MarketsSerialize
    {
        public static string ToJson(this Markets self) => JsonConvert.SerializeObject(self, JsonDefaultConverter.Settings);
    }

    public static class MarketsDeserialize
    {
        public static Markets FromJson(string json) => JsonConvert.DeserializeObject<Markets>(json, new JsonCustomConverter());
    }

    public partial class MyOrderBook : IResponse<MyOrderBook, OrderBookData>
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("result")]
        public OrderBookData Data { get; set; }
    }

    public partial class OrderBookData
    {
        [JsonProperty("buy")]
        public List<Order> Buy { get; set; }

        [JsonProperty("sell")]
        public List<Order> Sell { get; set; }
    }

    public partial class Order
    {
        [JsonProperty("Quantity")]
        public double Quantity { get; set; }

        [JsonProperty("Rate")]
        public double Rate { get; set; }
    }

    public partial class MyOrderBook
    {
        public MyOrderBook FromJson(string json) => JsonConvert.DeserializeObject<MyOrderBook>(json, new JsonCustomConverter());
        public string ToJson() => JsonConvert.SerializeObject(this, JsonDefaultConverter.Settings);
    }

    public static class GetOrderBookSerialize
    {
        public static string ToJson(this MyOrderBook self) => JsonConvert.SerializeObject(self, JsonDefaultConverter.Settings);
    }

    public static class GetOrderBookDeserialize
    {
        public static MyOrderBook FromJson(string json) => JsonConvert.DeserializeObject<MyOrderBook>(json, new JsonCustomConverter());
    }

    public partial class Summary : IListResponse<Summary, SummaryData>
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("result")]
        public List<SummaryData> Data { get; set; }


    }

    public partial class SummaryData
    {
        [JsonProperty("MarketName")]
        public string MarketName { get; set; }

        [JsonProperty("High")]
        public double High { get; set; }

        [JsonProperty("Low")]
        public double Low { get; set; }

        [JsonProperty("Volume")]
        public double Volume { get; set; }

        [JsonProperty("Last")]
        public double Last { get; set; }

        [JsonProperty("BaseVolume")]
        public double BaseVolume { get; set; }

        [JsonProperty("TimeStamp")]
        public DateTime TimeStamp { get; set; }

        [JsonProperty("Bid")]
        public double Bid { get; set; }

        [JsonProperty("Ask")]
        public double Ask { get; set; }

        [JsonProperty("OpenBuyOrders")]
        public int OpenBuyOrders { get; set; }

        [JsonProperty("OpenSellOrders")]
        public int OpenSellOrders { get; set; }

        [JsonProperty("PrevDay")]
        public double PrevDay { get; set; }

        [JsonProperty("Created")]
        public DateTime Created { get; set; }

        [JsonProperty("DisplayMarketName")]
        public string DisplayMarketName { get; set; }
    }

    public partial class Summary
    {
        public Summary FromJson(string json) => JsonConvert.DeserializeObject<Summary>(json, new JsonCustomConverter());
        public string ToJson() => JsonConvert.SerializeObject(this, JsonDefaultConverter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Summary self) => JsonConvert.SerializeObject(self, JsonDefaultConverter.Settings);
    }

    public static class Deserialize
    {
        public static Summary FromJson(string json) => JsonConvert.DeserializeObject<Summary>(json, new JsonCustomConverter());
    }

    #endregion

    #region API Implementation

    public class Constants
    {
        public static double TradeFee = 0.0025;
        public static double MinTradeSize = 0.00100000;
        public static string MarketsUrl = "https://bittrex.com/api/v1.1/public/getmarkets";
        public static string CoinInfoUrl = "https://bittrex.com/api/v1.1/public/getcurrencies";
        public static string SummaryUrl = "https://bittrex.com/api/v1.1/public/getmarketsummaries";
        public static string GetOrderBookUrl = "https://bittrex.com/api/v1.1/public/getorderbook?market=[0]&type=[1]";
    }

    public class Api : Exchange<Markets, Coin, Summary, MarketData, CoinData, SummaryData>
    {

        public override bool Initialize()
        {
            _Markets = new MarketsMessage(Constants.MarketsUrl, null);
            _CoinInfo = new CoinInfoMessage(Constants.CoinInfoUrl, null);
            _Summary = new SummaryMessage(Constants.SummaryUrl, null);

            return true;
        }

        public override void PostRefresh()
        {
            foreach (string key in _Markets.Data.Keys)
            {
                if (_Summary.Data.ContainsKey(key))
                {
                    MarketInfo m = _Markets.Data[key];
                    SummaryInfo s = _Summary.Data[key];
                    m.Ask = s.Ask;
                    m.BaseVolume = s.BaseVolume;
                    m.Bid = s.Bid;
                    m.High = s.High;
                    m.Last = s.Last;
                    m.Low = s.Low;
                    m.Volume = s.Volume;
                    m.BuyVolume = s.OpenBuyOrders;
                    m.SellVolume = s.OpenSellOrders;
                    if (s.PrevDay > s.Last)
                    {
                        m.Change = -1 * (1 - (s.Last / s.PrevDay)) * 100;
                    }
                    else
                    {
                        m.Change = ((s.Last / s.PrevDay) - 1) * 100;
                    }
                }
            }
        }

        public override BookInfo GetOrderBook(string pair, string type = "both")
        {
            string[] args = { pair, type };
            OrderBookMessage mt = new OrderBookMessage(Constants.GetOrderBookUrl, args);
            mt.Initialize();
            return mt.Data;
        }

    }

    public class OrderBookMessage : RequestMessage<MyOrderBook, OrderBookData, BookInfo>
    {
        public OrderBookMessage(string url, string[] args) : base(url, args) { }

        public override void Process(IResponse<MyOrderBook, OrderBookData> mr)
        {
            BookInfo book = new BookInfo();

            foreach (Order o in mr.Data.Buy)
            {
                book.Buys.Add(new BookInfo.Order(o.Rate, o.Quantity, BookInfo.Order.OrderType.Buy));
            }

            foreach (Order o in mr.Data.Sell)
            {
                book.Sells.Add(new BookInfo.Order(o.Rate, o.Quantity, BookInfo.Order.OrderType.Sell));
            }

            this._Data = book;
        }
    }

    public class MarketsMessage : RequestListMessage<Markets, MarketData, Dictionary<string, MarketInfo>>
    {
        public MarketsMessage(string url, string[] args) : base(url, args) { _Data = new Dictionary<string, MarketInfo>(); }

        public override void Process(IListResponse<Markets, MarketData> mr)
        {
            foreach (MarketData data in mr.Data)
            {
                MarketInfo mi = new MarketInfo();
                mi.MarketCurrency = data.MarketCurrency;
                mi.BaseCurrency = data.BaseCurrency;
                mi.Id = -1;
                mi.MarketName = data.MarketName;
                mi.MinTradeSize = Constants.MinTradeSize;
                mi.MakerFeeRate = Constants.TradeFee;
                mi.TakerFeeRate = Constants.TradeFee;
                mi.IsActive = data.IsActive;
                this._Data.Add(data.MarketName, mi);
            }
        }
    }

    public class CoinInfoMessage : RequestListMessage<Coin, CoinData, Dictionary<string, CoinInfo>>
    {
        public CoinInfoMessage(string url, string[] args) : base(url, args) { _Data = new Dictionary<string, CoinInfo>(); }

        public override void Process(IListResponse<Coin, CoinData> mr)
        {
            foreach (CoinData data in mr.Data)
            {
                CoinInfo ci = new CoinInfo(-1, data.CurrencyLong, data.Currency, null, data.CoinType, data.TxFee, 0, Convert.ToInt32(data.MinConfirmation), data.BaseAddress, data.IsActive, data.IsActive, (data.IsActive) ? CoinInfo.CoinStatus.OK : CoinInfo.CoinStatus.Unknown, null, null, null);
                this._Data.Add(data.Currency, ci);
            }
        }
    }

    public class SummaryMessage : RequestListMessage<Summary, SummaryData, Dictionary<string, SummaryInfo>>
    {
        public SummaryMessage(string url, string[] args) : base(url, args) { _Data = new Dictionary<string, SummaryInfo>(); }

        public override void Process(IListResponse<Summary, SummaryData> mr)
        {
            foreach (SummaryData data in mr.Data)
            {
                this._Data.Add(data.MarketName, new SummaryInfo(-1, data.MarketName, data.High, data.Low, data.Volume, data.Last, data.BaseVolume, data.TimeStamp, data.Bid,
                    data.Ask, data.OpenBuyOrders, data.OpenSellOrders, data.PrevDay, data.Created));
            }
        }
    }

    #endregion

}
