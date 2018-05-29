using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoShark2
{
    public class SummaryInfo
    {
        public long Id { get; set; }
        public string MarketName { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Volume { get; set; }
        public double Last { get; set; }
        public double BaseVolume { get; set; }
        public DateTime TimeStamp { get; set; }
        public double Bid { get; set; }
        public double Ask { get; set; }
        public double OpenBuyOrders { get; set; }
        public double OpenSellOrders { get; set; }
        public double PrevDay { get; set; }
        public DateTime Created { get; set; }

        public SummaryInfo(long id, string marketName, double high, double low, double volume, double last, double baseVolume, DateTime timeStamp,
            double bid, double ask, double openBuyOrders, double openSellOrders, double prevDay, DateTime created)
        {
            Id = id;
            MarketName = marketName;
            High = high;
            Low = low;
            Volume = volume;
            Last = last;
            BaseVolume = baseVolume;
            TimeStamp = timeStamp;
            Bid = bid;
            Ask = ask;
            OpenBuyOrders = openBuyOrders;
            OpenSellOrders = openSellOrders;
            PrevDay = prevDay;
            Created = created;
        }
    }
}
