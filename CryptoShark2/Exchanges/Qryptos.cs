using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CryptoShark2.Exchanges.Qryptos
{

    #region JSON Translators

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
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("product_type")]
        public ProductType ProductType { get; set; }

        [JsonProperty("code")]
        public Code Code { get; set; }

        [JsonProperty("name")]
        public object Name { get; set; }

        [JsonProperty("market_ask")]
        public double? MarketAsk { get; set; }

        [JsonProperty("market_bid")]
        public double? MarketBid { get; set; }

        [JsonProperty("indicator")]
        public long? Indicator { get; set; }

        [JsonProperty("currency")]
        public Currency Currency { get; set; }

        [JsonProperty("currency_pair_code")]
        public string CurrencyPairCode { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("btc_minimum_withdraw")]
        public object BtcMinimumWithdraw { get; set; }

        [JsonProperty("fiat_minimum_withdraw")]
        public object FiatMinimumWithdraw { get; set; }

        [JsonProperty("pusher_channel")]
        public string PusherChannel { get; set; }

        [JsonProperty("taker_fee")]
        public double TakerFee { get; set; }

        [JsonProperty("maker_fee")]
        public double MakerFee { get; set; }

        [JsonProperty("low_market_bid")]
        public string LowMarketBid { get; set; }

        [JsonProperty("high_market_ask")]
        public string HighMarketAsk { get; set; }

        [JsonProperty("volume_24h")]
        public string Volume24H { get; set; }

        [JsonProperty("last_price_24h")]
        public string LastPrice24H { get; set; }

        [JsonProperty("last_traded_price")]
        public string LastTradedPrice { get; set; }

        [JsonProperty("last_traded_quantity")]
        public string LastTradedQuantity { get; set; }

        [JsonProperty("quoted_currency")]
        public Currency QuotedCurrency { get; set; }

        [JsonProperty("base_currency")]
        public string BaseCurrency { get; set; }

        [JsonProperty("internal_token_sale")]
        public bool InternalTokenSale { get; set; }

        [JsonProperty("disabled")]
        public bool Disabled { get; set; }
    }

    public enum Code { Cash };

    public enum Currency { Btc, Eth, Qash };

    public enum ProductType { CurrencyPair };

    public partial class Markets
    {
        public Markets FromJson(string json) => JsonConvert.DeserializeObject<Markets>(json, Converter.Settings);
        public string ToJson() => JsonConvert.SerializeObject(this, Converter.Settings);
    }

    public static class MarketsSerialize
    {
        public static string ToJson(this Markets self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new CodeConverter(),
                new CurrencyConverter(),
                new ProductTypeConverter(),
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class CodeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Code) || t == typeof(Code?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "CASH")
            {
                return Code.Cash;
            }
            throw new Exception("Cannot unmarshal type Code");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (Code)untypedValue;
            if (value == Code.Cash)
            {
                serializer.Serialize(writer, "CASH"); return;
            }
            throw new Exception("Cannot marshal type Code");
        }
    }

    internal class CurrencyConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Currency) || t == typeof(Currency?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "BTC":
                    return Currency.Btc;
                case "ETH":
                    return Currency.Eth;
                case "QASH":
                    return Currency.Qash;
            }
            throw new Exception("Cannot unmarshal type Currency");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (Currency)untypedValue;
            switch (value)
            {
                case Currency.Btc:
                    serializer.Serialize(writer, "BTC"); return;
                case Currency.Eth:
                    serializer.Serialize(writer, "ETH"); return;
                case Currency.Qash:
                    serializer.Serialize(writer, "QASH"); return;
            }
            throw new Exception("Cannot marshal type Currency");
        }
    }

    internal class ProductTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ProductType) || t == typeof(ProductType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "CurrencyPair")
            {
                return ProductType.CurrencyPair;
            }
            throw new Exception("Cannot unmarshal type ProductType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (ProductType)untypedValue;
            if (value == ProductType.CurrencyPair)
            {
                serializer.Serialize(writer, "CurrencyPair"); return;
            }
            throw new Exception("Cannot marshal type ProductType");
        }
    }

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
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("product_type")]
        public ProductType ProductType { get; set; }

        [JsonProperty("code")]
        public Code Code { get; set; }

        [JsonProperty("name")]
        public object Name { get; set; }

        [JsonProperty("market_ask")]
        public double? MarketAsk { get; set; }

        [JsonProperty("market_bid")]
        public double? MarketBid { get; set; }

        [JsonProperty("indicator")]
        public long? Indicator { get; set; }

        [JsonProperty("currency")]
        public Currency Currency { get; set; }

        [JsonProperty("currency_pair_code")]
        public string CurrencyPairCode { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("btc_minimum_withdraw")]
        public object BtcMinimumWithdraw { get; set; }

        [JsonProperty("fiat_minimum_withdraw")]
        public object FiatMinimumWithdraw { get; set; }

        [JsonProperty("pusher_channel")]
        public string PusherChannel { get; set; }

        [JsonProperty("taker_fee")]
        public double TakerFee { get; set; }

        [JsonProperty("maker_fee")]
        public double MakerFee { get; set; }

        [JsonProperty("low_market_bid")]
        public string LowMarketBid { get; set; }

        [JsonProperty("high_market_ask")]
        public string HighMarketAsk { get; set; }

        [JsonProperty("volume_24h")]
        public string Volume24H { get; set; }

        [JsonProperty("last_price_24h")]
        public string LastPrice24H { get; set; }

        [JsonProperty("last_traded_price")]
        public string LastTradedPrice { get; set; }

        [JsonProperty("last_traded_quantity")]
        public string LastTradedQuantity { get; set; }

        [JsonProperty("quoted_currency")]
        public Currency QuotedCurrency { get; set; }

        [JsonProperty("base_currency")]
        public string BaseCurrency { get; set; }

        [JsonProperty("internal_token_sale")]
        public bool InternalTokenSale { get; set; }

        [JsonProperty("disabled")]
        public bool Disabled { get; set; }
    }

    public partial class Coin
    {
        public Coin FromJson(string json) => JsonConvert.DeserializeObject<Coin>(json, Converter.Settings);
        public string ToJson() => JsonConvert.SerializeObject(this, Converter.Settings);
    }

    public static class CoinSerialize
    {
        public static string ToJson(this Coin self) => JsonConvert.SerializeObject(self, Converter.Settings);
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
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("product_type")]
        public ProductType ProductType { get; set; }

        [JsonProperty("code")]
        public Code Code { get; set; }

        [JsonProperty("name")]
        public object Name { get; set; }

        [JsonProperty("market_ask")]
        public double? MarketAsk { get; set; }

        [JsonProperty("market_bid")]
        public double? MarketBid { get; set; }

        [JsonProperty("indicator")]
        public long? Indicator { get; set; }

        [JsonProperty("currency")]
        public Currency Currency { get; set; }

        [JsonProperty("currency_pair_code")]
        public string CurrencyPairCode { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("btc_minimum_withdraw")]
        public object BtcMinimumWithdraw { get; set; }

        [JsonProperty("fiat_minimum_withdraw")]
        public object FiatMinimumWithdraw { get; set; }

        [JsonProperty("pusher_channel")]
        public string PusherChannel { get; set; }

        [JsonProperty("taker_fee")]
        public double TakerFee { get; set; }

        [JsonProperty("maker_fee")]
        public double MakerFee { get; set; }

        [JsonProperty("low_market_bid")]
        public string LowMarketBid { get; set; }

        [JsonProperty("high_market_ask")]
        public string HighMarketAsk { get; set; }

        [JsonProperty("volume_24h")]
        public string Volume24H { get; set; }

        [JsonProperty("last_price_24h")]
        public string LastPrice24H { get; set; }

        [JsonProperty("last_traded_price")]
        public string LastTradedPrice { get; set; }

        [JsonProperty("last_traded_quantity")]
        public string LastTradedQuantity { get; set; }

        [JsonProperty("quoted_currency")]
        public Currency QuotedCurrency { get; set; }

        [JsonProperty("base_currency")]
        public string BaseCurrency { get; set; }

        [JsonProperty("internal_token_sale")]
        public bool InternalTokenSale { get; set; }

        [JsonProperty("disabled")]
        public bool Disabled { get; set; }
    }

    public partial class Summary
    {
        public Summary FromJson(string json) => JsonConvert.DeserializeObject<Summary>(json, Converter.Settings);
        public string ToJson() => JsonConvert.SerializeObject(this, Converter.Settings);
    }

    public static class SummarySerialize
    {
        public static string ToJson(this Summary self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    public partial class MyOrderBook : IResponse<MyOrderBook, OrderBookData>
    {
        [JsonProperty("Success")]
        public bool Success { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("Data")]
        public OrderBookData Data { get; set; }
    }

    public partial class OrderBookData
    {
        [JsonProperty("buy_price_levels")]
        public List<List<string>> BuyPriceLevels { get; set; }

        [JsonProperty("sell_price_levels")]
        public List<List<string>> SellPriceLevels { get; set; }
    }

    public partial class MyOrderBook
    {
        public MyOrderBook FromJson(string json) => JsonConvert.DeserializeObject<MyOrderBook>(json, MyOrderBookConverter.Settings);
        public string ToJson() => JsonConvert.SerializeObject(this, MyOrderBookConverter.Settings);
    }

    public static class MyOrderBookSerialize
    {
        public static string ToJson(this MyOrderBook self) => JsonConvert.SerializeObject(self, MyOrderBookConverter.Settings);
    }

    internal static class MyOrderBookConverter
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
        public static double MinTradeSize = 0;
        public static string MarketsUrl = "https://api.qryptos.com/products";
        public static string CoinInfoUrl = "https://api.qryptos.com/products";
        public static string SummaryUrl = "https://api.qryptos.com/products";
        public static string GetOrderBookUrl = "https://api.qryptos.com/products/[0]/price_levels?full=1";
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
            if(this.Markets.ContainsKey(pair))
            {
                string id = this.Markets[pair].Id.ToString();
                string[] args = { id, type };
                OrderBookMessage mt = new OrderBookMessage(Constants.GetOrderBookUrl, args);
                mt.Initialize();
                return mt.Data;
            } else
            {
                return null;
            }
        }
    }

    public class OrderBookMessage : RequestMessage<MyOrderBook, OrderBookData, BookInfo>
    {
        public OrderBookMessage(string url, string[] args) : base(url, args)
        {
            _needShim = true;
            _shimStart = "{\"Success\":true,\"Message\":null,\"Data\":";
            _shimEnd = "}";
        }

        public override void Process(IResponse<MyOrderBook, OrderBookData> mr)
        {
            BookInfo book = new BookInfo();

            foreach (List<string> o in mr.Data.BuyPriceLevels)
            {
                book.Buys.Add(new BookInfo.Order(Convert.ToDouble(o[0]), Convert.ToDouble(o[1]), BookInfo.Order.OrderType.Buy));
            }

            foreach (List<string> o in mr.Data.SellPriceLevels)
            {
                book.Sells.Add(new BookInfo.Order(Convert.ToDouble(o[0]), Convert.ToDouble(o[1]), BookInfo.Order.OrderType.Sell));
            }

            this._Data = book;
        }
    }

    public class MarketsMessage : RequestListMessage<Markets, MarketData, Dictionary<string, MarketInfo>>
    {
        public MarketsMessage(string url, string[] args) : base(url, args)
        {
            _Data = new Dictionary<string, MarketInfo>();
            _needShim = true;
            _shimStart = "{\"Success\":true,\"Message\":null,\"Data\":";
            _shimEnd = "}";
        }

        public override void Process(IListResponse<Markets, MarketData> mr)
        {
            foreach (MarketData data in mr.Data)
            {
                MarketInfo mi = new MarketInfo();
                mi.MarketCurrency = data.BaseCurrency;
                mi.BaseCurrency = data.QuotedCurrency.ToString().ToUpper();
                mi.Id = Convert.ToInt64(data.Id);
                mi.MarketName = mi.BaseCurrency + "-" + mi.MarketCurrency;
                mi.MinTradeSize = Constants.MinTradeSize;
                mi.MakerFeeRate = data.MakerFee * -1;
                mi.TakerFeeRate = data.TakerFee;
                mi.IsActive = !data.Disabled;
                mi.Ask = Convert.ToDouble(data.MarketAsk);
                mi.Bid = Convert.ToDouble(data.MarketBid);
                mi.Last = Convert.ToDouble(data.LastTradedPrice);
                this._Data.Add(mi.MarketName, mi);
            }
        }
    }

    public class CoinInfoMessage : RequestListMessage<Coin, CoinData, Dictionary<string, CoinInfo>>
    {
        public CoinInfoMessage(string url, string[] args) : base(url, args)
        {
            _Data = new Dictionary<string, CoinInfo>();
            _needShim = true;
            _shimStart = "{\"Success\":true,\"Message\":null,\"Data\":";
            _shimEnd = "}";
        }

        public override void Process(IListResponse<Coin, CoinData> mr)
        {
            foreach (CoinData data in mr.Data)
            {
                string name = data.QuotedCurrency.ToString().ToUpper() + "-" + data.BaseCurrency;
                CoinInfo ci = new CoinInfo(Convert.ToInt64(data.Id), name, data.BaseCurrency, null, null, 0, 0, 0, null, !data.Disabled, !data.Disabled, (!data.Disabled) ? CoinInfo.CoinStatus.OK : CoinInfo.CoinStatus.Unknown, null, null, null);
                this._Data.Add(name, ci);
            }
        }
    }

    public class SummaryMessage : RequestListMessage<Summary, SummaryData, Dictionary<string, SummaryInfo>>
    {
        public SummaryMessage(string url, string[] args) : base(url, args)
        {
            _Data = new Dictionary<string, SummaryInfo>();
            _needShim = true;
            _shimStart = "{\"Success\":true,\"Message\":null,\"Data\":";
            _shimEnd = "}";
        }

        public override void Process(IListResponse<Summary, SummaryData> mr)
        {
            foreach (SummaryData data in mr.Data)
            {
                string name = data.QuotedCurrency.ToString().ToUpper() + "-" + data.BaseCurrency;
                SummaryInfo si = new SummaryInfo(Convert.ToInt64(data.Id), name, Convert.ToDouble(data.HighMarketAsk), Convert.ToDouble(data.LowMarketBid), Convert.ToDouble(data.Volume24H), Convert.ToDouble(data.LastTradedPrice), 0, DateTime.UtcNow, Convert.ToDouble(data.MarketBid), Convert.ToDouble(data.MarketAsk), 0, 0, Convert.ToDouble(data.LastPrice24H), DateTime.UtcNow);
                this._Data.Add(name, si);
            }
        }
    }

    #endregion

}