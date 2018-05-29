using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CryptoShark2.Exchanges.KuCoin
{

    #region JSON Translators

    public partial class Coin : IListResponse<Coin, CoinData>
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("msg")]
        public string Message { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("data")]
        public List<CoinData> Data { get; set; }
    }

    public partial class CoinData
    {
        [JsonProperty("withdrawMinFee")]
        public double WithdrawMinFee { get; set; }

        [JsonProperty("withdrawMinAmount")]
        public double WithdrawMinAmount { get; set; }

        [JsonProperty("withdrawFeeRate")]
        public double WithdrawFeeRate { get; set; }

        [JsonProperty("confirmationCount")]
        public long ConfirmationCount { get; set; }

        [JsonProperty("withdrawRemark")]
        public string WithdrawRemark { get; set; }

        [JsonProperty("infoUrl")]
        public object InfoUrl { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("tradePrecision")]
        public long TradePrecision { get; set; }

        [JsonProperty("depositRemark")]
        public string DepositRemark { get; set; }

        [JsonProperty("enableWithdraw")]
        public bool EnableWithdraw { get; set; }

        [JsonProperty("enableDeposit")]
        public bool EnableDeposit { get; set; }

        [JsonProperty("coin")]
        public string Coin { get; set; }
    }

    public partial class Coin
    {
        public Coin FromJson(string json) => JsonConvert.DeserializeObject<Coin>(json, new JsonCustomConverter());
        public string ToJson() => JsonConvert.SerializeObject(this, JsonDefaultConverter.Settings);
    }

    public static class CoinSerialize
    {
        public static string ToJson(this Markets self) => JsonConvert.SerializeObject(self, JsonDefaultConverter.Settings);
    }

    public static class CoinDeserialize
    {
        public static Markets FromJson(string json) => JsonConvert.DeserializeObject<Markets>(json, new JsonCustomConverter());
    }

    internal class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    public partial class Markets : IListResponse<Markets, MarketData>
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("msg")]
        public string Message { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("data")]
        public List<MarketData> Data { get; set; }
    }

    public partial class MarketData
    {
        [JsonProperty("coinType")]
        public string CoinType { get; set; }

        [JsonProperty("trading")]
        public bool Trading { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("lastDealPrice")]
        public double LastDealPrice { get; set; }

        [JsonProperty("buy")]
        public double? Buy { get; set; }

        [JsonProperty("sell")]
        public double Sell { get; set; }

        [JsonProperty("change")]
        public double? Change { get; set; }

        [JsonProperty("coinTypePair")]
        public string CoinTypePair { get; set; }

        [JsonProperty("sort")]
        public long Sort { get; set; }

        [JsonProperty("feeRate")]
        public double FeeRate { get; set; }

        [JsonProperty("volValue")]
        public double VolValue { get; set; }

        [JsonProperty("high")]
        public double? High { get; set; }

        [JsonProperty("datetime")]
        public long Datetime { get; set; }

        [JsonProperty("vol")]
        public double Vol { get; set; }

        [JsonProperty("low")]
        public double? Low { get; set; }

        [JsonProperty("changeRate")]
        public double? ChangeRate { get; set; }
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

    internal class MarketsConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    public partial class MyOrderBook : IResponse<MyOrderBook, OrderBookData>
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("msg")]
        public string Message { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("data")]
        public OrderBookData Data { get; set; }
    }

    public partial class OrderBookData
    {
        [JsonProperty("SELL")]
        public List<List<double>> Sell { get; set; }

        [JsonProperty("BUY")]
        public List<List<double>> Buy { get; set; }
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

    internal class MyOrderBookConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    public partial class Summary : IListResponse<Summary, SummaryData>
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("msg")]
        public string Message { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("data")]
        public List<SummaryData> Data { get; set; }
    }

    public partial class SummaryData
    {
        [JsonProperty("coinType")]
        public string CoinType { get; set; }

        [JsonProperty("trading")]
        public bool Trading { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("lastDealPrice")]
        public double LastDealPrice { get; set; }

        [JsonProperty("buy")]
        public double Buy { get; set; }

        [JsonProperty("sell")]
        public double Sell { get; set; }

        [JsonProperty("change")]
        public double? Change { get; set; }

        [JsonProperty("coinTypePair")]
        public string CoinTypePair { get; set; }

        [JsonProperty("sort")]
        public long Sort { get; set; }

        [JsonProperty("feeRate")]
        public double FeeRate { get; set; }

        [JsonProperty("volValue")]
        public double VolValue { get; set; }

        [JsonProperty("high")]
        public double High { get; set; }

        [JsonProperty("datetime")]
        public long Datetime { get; set; }

        [JsonProperty("vol")]
        public double Vol { get; set; }

        [JsonProperty("low")]
        public double Low { get; set; }

        [JsonProperty("changeRate")]
        public double ChangeRate { get; set; }
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

    internal class SummaryConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    #endregion

    #region API Implementation

    public class Constants
    {
        public static string MarketsUrl = "https://api.kucoin.com/v1/market/open/symbols";
        public static string CoinInfoUrl = "https://api.kucoin.com/v1/market/open/coins";
        public static string SummaryUrl = "https://api.kucoin.com/v1/market/open/symbols";
        public static string GetOrderBookUrl = "https://api.kucoin.com/v1/[0]/open/orders/";
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
            
        }

        public override BookInfo GetOrderBook(string pair, string type = "both")
        {
            string[] p = pair.Split("-".ToCharArray());
            string repair = p[1] + "-" + p[0];

            string[] args = { repair, type };
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


            foreach (List<double> list in mr.Data.Buy)
            {
                book.Buys.Add(new BookInfo.Order(list[0], list[1], BookInfo.Order.OrderType.Buy));
            }

            foreach (List<double> list in mr.Data.Sell)
            {
                book.Sells.Add(new BookInfo.Order(list[0], list[1], BookInfo.Order.OrderType.Sell));
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
                string symbol;
                string[] s = data.Symbol.Split("-".ToCharArray());
                symbol = s[1] + "-" + s[0];
                this._Data.Add(symbol, new MarketInfo(data.CoinType, data.CoinTypePair, data.CoinType, data.Trading, 0, data.FeeRate, data.FeeRate, Convert.ToDouble(data.High), Convert.ToDouble(data.Low), data.Vol, data.LastDealPrice, data.Sell, Convert.ToDouble(data.Buy), 0, 0, Convert.ToDouble(data.ChangeRate), 0,0,0,0,0));
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
                string infourl = null;
                if(data.InfoUrl != null)
                {
                    infourl = data.InfoUrl.ToString();
                }
                this._Data.Add(data.Coin, new CoinInfo(-1, data.Name, data.Coin, null, null, data.WithdrawMinFee, data.WithdrawMinAmount, Convert.ToInt32(data.ConfirmationCount), null, data.EnableDeposit, data.EnableWithdraw, (data.EnableDeposit && data.EnableWithdraw) ? CoinInfo.CoinStatus.OK : CoinInfo.CoinStatus.Unknown, null, infourl, null));
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
                string symbol;
                string[] s = data.Symbol.Split("-".ToCharArray());
                symbol = s[1] + "-" + s[0];

                this._Data.Add(symbol, new SummaryInfo(-1, symbol, data.High, data.Low, data.Vol, data.LastDealPrice, data.VolValue, new DateTime(data.Datetime), data.Buy,
                    data.Sell, -1, -1, -1, new DateTime(data.Datetime)));
            }
        }
    }

    #endregion
}
