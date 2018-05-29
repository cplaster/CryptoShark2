using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoShark2
{
    public class MarketInfo
    {
        public long Id { get; set; }
        public string MarketCurrency { get; set; }
        public string BaseCurrency { get; set; }
        public string MarketName { get; set; }
        public bool IsActive { get; set; }
        public double MinTradeSize { get; set; }
        public double MakerFeeRate { get; set; }
        public double TakerFeeRate { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Volume { get; set; }
        public double Last { get; set; }
        public double Ask { get; set; }
        public double Bid { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
        public double Change { get; set; }
        public double BuyVolume { get; set; }
        public double SellVolume { get; set; }
        public double BaseVolume { get; set; }
        public double BaseBuyVolume { get; set; }
        public double BaseSellVolume { get; set; }

        public MarketInfo() { }

        public MarketInfo(
            string marketCurrency,
            string baseCurrency,
            string marketName,
            bool isActive,
            double minTradeSize,
            double makerFeeRate,
            double takerFeeRate,
            double high,
            double low,
            double volume,
            double last,
            double ask,
            double bid,
            double open,
            double close,
            double change,
            double buyVolume,
            double sellVolume,
            double baseVolume,
            double baseBuyVolume,
            double baseSellVolume
            )
        {
            MarketCurrency = marketCurrency;
            BaseCurrency = baseCurrency;
            MarketName = marketName;
            IsActive = isActive;
            MinTradeSize = minTradeSize;
            MakerFeeRate = makerFeeRate;
            TakerFeeRate = takerFeeRate;
            High = high;
            Low = low;
            Volume = volume;
            Last = last;
            Ask = ask;
            Bid = bid;
            Open = open;
            Close = close;
            Change = change;
            BuyVolume = buyVolume;
            SellVolume = sellVolume;
            BaseBuyVolume = baseBuyVolume;
            BaseSellVolume = baseSellVolume;

        }
    }
}
