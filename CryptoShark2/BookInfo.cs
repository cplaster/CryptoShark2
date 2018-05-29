using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoShark2
{
    public class BookInfo
    {
        public List<Order> Buys { get; } = new List<Order>();
        public List<Order> Sells { get; } = new List<Order>();

        public class Order
        {
            public double Price { get; }
            public double Volume { get; }
            public OrderType Type { get; }

            public Order(double price, double volume, OrderType type)
            {
                Price = price;
                Volume = volume;
                Type = type;
            }

            public enum OrderType
            {
                Buy,
                Sell
            }
        }
    }
}
