using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CryptoShark2.Exchanges.Cryptopia
{

    #region JSON Translators

    public partial class Coin : IListResponse<Coin, CoinData>
    {
        [JsonProperty("Success")]
        public bool Success { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("Data")]
        public List<CoinData> Data { get; set; }
    }

    public partial class CoinData
    {
        [JsonProperty("Id")]
        public long Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Symbol")]
        public string Symbol { get; set; }

        [JsonProperty("Algorithm")]
        public string Algorithm { get; set; }

        [JsonProperty("WithdrawFee")]
        public double WithdrawFee { get; set; }

        [JsonProperty("MinWithdraw")]
        public double MinWithdraw { get; set; }

        [JsonProperty("MinBaseTrade")]
        public long MinBaseTrade { get; set; }

        [JsonProperty("IsTipEnabled")]
        public bool IsTipEnabled { get; set; }

        [JsonProperty("MinTip")]
        public long MinTip { get; set; }

        [JsonProperty("DepositConfirmations")]
        public long DepositConfirmations { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("StatusMessage")]
        public string StatusMessage { get; set; }

        [JsonProperty("ListingStatus")]
        public string ListingStatus { get; set; }
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
        [JsonProperty("Success")]
        public bool Success { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("Data")]
        public List<MarketData> Data { get; set; }
    }

    public partial class MarketData
    {
        [JsonProperty("Id")]
        public long Id { get; set; }

        [JsonProperty("Label")]
        public string Label { get; set; }

        [JsonProperty("Currency")]
        public string Currency { get; set; }

        [JsonProperty("Symbol")]
        public string Symbol { get; set; }

        [JsonProperty("BaseCurrency")]
        public string BaseCurrency { get; set; }

        [JsonProperty("BaseSymbol")]
        public string BaseSymbol { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("StatusMessage")]
        public string StatusMessage { get; set; }

        [JsonProperty("TradeFee")]
        public double TradeFee { get; set; }

        [JsonProperty("MinimumTrade")]
        public double MinimumTrade { get; set; }

        [JsonProperty("MaximumTrade")]
        public double MaximumTrade { get; set; }

        [JsonProperty("MinimumBaseTrade")]
        public double MinimumBaseTrade { get; set; }

        [JsonProperty("MaximumBaseTrade")]
        public double MaximumBaseTrade { get; set; }

        [JsonProperty("MinimumPrice")]
        public double MinimumPrice { get; set; }

        [JsonProperty("MaximumPrice")]
        public double MaximumPrice { get; set; }
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
        [JsonProperty("Success")]
        public bool Success { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("Data")]
        public OrderBookData Data { get; set; }

        [JsonProperty("Error")]
        public object Error { get; set; }
    }

    public partial class OrderBookData
    {
        [JsonProperty("Buy")]
        public List<Order> Buy { get; set; }

        [JsonProperty("Sell")]
        public List<Order> Sell { get; set; }
    }

    public partial class Order
    {
        [JsonProperty("TradePairId")]
        public long TradePairId { get; set; }

        [JsonProperty("Label")]
        public string Label { get; set; }

        [JsonProperty("Price")]
        public double Price { get; set; }

        [JsonProperty("Volume")]
        public double Volume { get; set; }

        [JsonProperty("Total")]
        public double Total { get; set; }
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
        [JsonProperty("Success")]
        public bool Success { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("Data")]
        public List<SummaryData> Data { get; set; }
    }

    public partial class SummaryData
    {
        [JsonProperty("TradePairId")]
        public long TradePairId { get; set; }

        [JsonProperty("Label")]
        public string Label { get; set; }

        [JsonProperty("AskPrice")]
        public double AskPrice { get; set; }

        [JsonProperty("BidPrice")]
        public double BidPrice { get; set; }

        [JsonProperty("Low")]
        public double Low { get; set; }

        [JsonProperty("High")]
        public double High { get; set; }

        [JsonProperty("Volume")]
        public double Volume { get; set; }

        [JsonProperty("LastPrice")]
        public double LastPrice { get; set; }

        [JsonProperty("BuyVolume")]
        public double BuyVolume { get; set; }

        [JsonProperty("SellVolume")]
        public double SellVolume { get; set; }

        [JsonProperty("Change")]
        public double Change { get; set; }

        [JsonProperty("Open")]
        public double Open { get; set; }

        [JsonProperty("Close")]
        public double Close { get; set; }

        [JsonProperty("BaseVolume")]
        public double BaseVolume { get; set; }

        [JsonProperty("BaseBuyVolume")]
        public double BaseBuyVolume { get; set; }

        [JsonProperty("BaseSellVolume")]
        public double BaseSellVolume { get; set; }
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
        public static string MarketsUrl = "https://www.cryptopia.co.nz/api/GetTradePairs";
        public static string CoinInfoUrl = "https://www.cryptopia.co.nz/api/GetCurrencies";
        public static string SummaryUrl = "https://www.cryptopia.co.nz/api/GetMarkets";
        public static string GetOrderBookUrl = "https://www.cryptopia.co.nz/api/GetMarketOrders/[0]";
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
                    if(s.PrevDay > s.Last)
                    {
                        m.Change = -1 * (1 - (s.Last / s.PrevDay)) * 100;
                    } else
                    {
                        m.Change = ((s.Last / s.PrevDay) - 1) * 100;
                    }
                }
            }
        }

        public override BookInfo GetOrderBook(string pair, string type = "both")
        {
            string[] p = pair.Split("-".ToCharArray());
            string a = p[1] + "_" + p[0];
            string[] args = { a, type };
            OrderBookMessage mt = new OrderBookMessage(Constants.GetOrderBookUrl, args);
            mt.Initialize();
            return mt.Data;
        }

        public class OrderBookMessage : RequestMessage<MyOrderBook, OrderBookData, BookInfo>
        {
            public OrderBookMessage(string url, string[] args) : base(url, args) { }

            public override void Process(IResponse<MyOrderBook, OrderBookData> mr)
            {
                BookInfo book = new BookInfo();

                foreach (Order o in mr.Data.Buy)
                {
                    book.Buys.Add(new BookInfo.Order(o.Price, o.Volume, BookInfo.Order.OrderType.Buy));
                }

                foreach (Order o in mr.Data.Sell)
                {
                    book.Sells.Add(new BookInfo.Order(o.Price, o.Volume, BookInfo.Order.OrderType.Sell));
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
                    string label = data.Label.Replace("/", "-");
                    string[] temp = label.Split("-".ToCharArray());
                    label = temp[1] + "-" + temp[0];
                    // for some reason, some exchanges report the same label more than once...
                    if (!this._Data.ContainsKey(label))
                    {
                        MarketInfo mi = new MarketInfo();
                        mi.MarketCurrency = data.Currency;
                        mi.BaseCurrency = data.BaseCurrency;
                        mi.Id = data.Id;
                        mi.MarketName = label;
                        mi.MinTradeSize = data.MinimumBaseTrade;
                        mi.MakerFeeRate = data.TradeFee / 100;
                        mi.TakerFeeRate = data.TradeFee / 100;
                        mi.IsActive = ValidateStatus(data.Status);

                        this._Data.Add(label, mi);
                    }
                }
            }

            private bool ValidateStatus(string status)
            {
                return CryptoShark2.CoinInfo.CoinStatusConverter(status) == CryptoShark2.CoinInfo.CoinStatus.OK || CryptoShark2.CoinInfo.ListingStatusConverter(status) == CryptoShark2.CoinInfo.ListingStatus.Active;
            }
        }

        public class CoinInfoMessage : RequestListMessage<Coin, CoinData, Dictionary<string, CoinInfo>>
        {
            public CoinInfoMessage(string url, string[] args) : base(url, args) { _Data = new Dictionary<string, CoinInfo>(); }

            public override void Process(IListResponse<Coin, CoinData> mr)
            {
                foreach (CoinData data in mr.Data)
                {
                    CoinInfo.ListingStatus listingstatus = CryptoShark2.CoinInfo.ListingStatusConverter(data.ListingStatus);
                    CoinInfo.CoinStatus coinstatus = CryptoShark2.CoinInfo.CoinStatusConverter(data.Status);
                    bool enabled = ValidateStatus(data.Status);
                    CoinInfo ci = new CoinInfo(data.Id, data.Name, data.Symbol, data.Algorithm, null, data.WithdrawFee, data.MinWithdraw, Convert.ToInt32(data.DepositConfirmations), null, enabled, enabled, coinstatus, data.StatusMessage, null, null);
                    this._Data.Add(data.Symbol, ci);
                }
            }

            private bool ValidateStatus(string status)
            {
                return CryptoShark2.CoinInfo.CoinStatusConverter(status) == CryptoShark2.CoinInfo.CoinStatus.OK || CryptoShark2.CoinInfo.ListingStatusConverter(status) == CryptoShark2.CoinInfo.ListingStatus.Active;
            }
        }

        public class SummaryMessage : RequestListMessage<Summary, SummaryData, Dictionary<string, SummaryInfo>>
        {
            public SummaryMessage(string url, string[] args) : base(url, args) { _Data = new Dictionary<string, SummaryInfo>(); }

            public override void Process(IListResponse<Summary, SummaryData> mr)
            {
                foreach (SummaryData data in mr.Data)
                {
                    string label = data.Label.Replace("/", "-");
                    string[] temp = label.Split("-".ToCharArray());
                    label = temp[1] + "-" + temp[0];

                    // for some reason, some exchanges report the same label more than once.
                    if (!this._Data.ContainsKey(label))
                    {
                        this._Data.Add(label, new SummaryInfo(data.TradePairId, label, data.High, data.Low, data.Volume, data.LastPrice, data.BaseVolume, DateTime.UtcNow, data.BidPrice,
                            data.AskPrice, data.BuyVolume, data.SellVolume, data.Open, DateTime.UtcNow));
                    }
                }
            }
        }
    }

    #endregion

}
