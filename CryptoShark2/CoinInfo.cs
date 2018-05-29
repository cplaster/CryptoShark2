using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoShark2
{
    public class CoinInfo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Algo { get; set; }
        public string CoinType { get; set; }
        public double WithdrawFee { get; set; }
        public double MinWithdraw { get; set; }
        public int Confirmations { get; set; }
        public string BaseAddress { get; set; }
        public bool EnableDeposit { get; set; }
        public bool EnableWithdraw { get; set; }
        public CoinStatus Status { get; set; }
        public string StatusMessage { get; set; }
        public string InfoUrl { get; set; }
        public string IconUrl { get; set; }

        public CoinInfo() { }

        public CoinInfo(
            long id,
            string name,
            string symbol,
            string algo,
            string coinType,
            double withdrawFee,
            double minWithdraw,
            int confirmations,
            string baseAddress,
            bool enableDeposit,
            bool enableWithdraw,
            CoinStatus status,
            string statusMessage,
            string infoUrl,
            string iconUrl
            )
        {
            Id = id;
            Name = name;
            Symbol = symbol;
            Algo = algo;
            CoinType = coinType;
            WithdrawFee = withdrawFee;
            MinWithdraw = minWithdraw;
            Confirmations = confirmations;
            BaseAddress = baseAddress;
            EnableDeposit = enableDeposit;
            EnableWithdraw = enableWithdraw;
            Status = status;
            StatusMessage = statusMessage;
            InfoUrl = infoUrl;
            IconUrl = iconUrl;

        }

        public enum ListingStatus
        {
            Active,
            Delisting,
            Unknown
        }

        public static ListingStatus ListingStatusConverter(string status)
        {
            switch(status)
            {
                case "Active":
                    return ListingStatus.Active;
                case "Delisting":
                    return ListingStatus.Delisting;
                default:
                    return ListingStatus.Unknown;
            }
        }

        public enum CoinStatus
        {
            OK,
            Maintenence,
            Offline,
            Unknown
        }

        public static CoinStatus CoinStatusConverter(string status)
        {
            switch(status)
            {
                case "OK":
                    return CoinStatus.OK;
                case "Maintenence":
                    return CoinStatus.Maintenence;
                case "Offline":
                    return CoinStatus.Offline;
                default:
                    return CoinStatus.Unknown;
            }
        }

    }
}
