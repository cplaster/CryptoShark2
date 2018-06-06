using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CryptoShark2
{
    /// <summary>
    /// Interaction logic for testwindow2.xaml
    /// </summary>
    public partial class testwindow2 : Window
    {
        Dictionary<string, List<Info.CryptoCompare.HistoMinuteData>> cache = new Dictionary<string, List<Info.CryptoCompare.HistoMinuteData>>();

        public testwindow2()
        {
            InitializeComponent();
        }

        private MarketPair GetMarketPair()
        {
            string[] a = txtPair.Text.Split("-".ToCharArray());
            MarketPair mp = new MarketPair();
            mp.MarketCurrency = a[0];
            mp.BaseCurrency = a[1];

            return mp;
        }

        public struct MarketPair
        {
            public string MarketCurrency;
            public string BaseCurrency;
        }

        private void GetData(Info.CryptoCompare.HistoType type)
        {
            string exchange = txtExchange.Text;
            candlechart.Children.Clear();
            MarketPair mp = GetMarketPair();
            List<Info.CryptoCompare.HistoMinuteData> dat;
            string histoLimit = txtlimit.Text;
            int period = Convert.ToInt32(txtPeriod.Text);

            string cacheKey = exchange + "-" + type.ToString() + "-" + histoLimit + ":" + mp.MarketCurrency + "-" + mp.BaseCurrency;

            if (cache.ContainsKey(cacheKey))
            {
                dat = cache[cacheKey];
            }
            else
            {
                Info.CryptoCompare.Api api = new Info.CryptoCompare.Api();
                dat = api.GetHistoData(mp.MarketCurrency, mp.BaseCurrency, exchange, type, histoLimit);
                cache.Add(cacheKey, dat);
            }

            int periodCounter = 0;
            double s = 100000000;
            ObservableCollection<CandleChart.OhlcvData> data = new ObservableCollection<CandleChart.OhlcvData>();
            CandleChart.OhlcvData tempOhlcv = new CandleChart.OhlcvData();

            foreach (Info.CryptoCompare.HistoMinuteData d in dat)
            {
                DateTime time = Info.CryptoCompare.Dates.FromUnixTime(d.Time);
                if (period == 1)
                {
                    data.Add(new CandleChart.OhlcvData(d.Open * s, d.High * s, d.Low * s, d.Close * s, d.VolumeFrom, d.VolumeTo, time));
                }
                else
                {
                    if (periodCounter == 0)
                    {
                        tempOhlcv = new CandleChart.OhlcvData(d.Open * s, d.High * s, d.Low * s, d.Close * s, d.VolumeFrom, d.VolumeTo, time);
                        periodCounter++;
                    }
                    else
                    {
                        if (periodCounter == period - 1)
                        {
                            CandleChart.OhlcvData dd = new CandleChart.OhlcvData(d.Open * s, d.High * s, d.Low * s, d.Close * s, d.VolumeFrom, d.VolumeTo, time);
                            if (tempOhlcv.High < dd.High) { tempOhlcv.High = dd.High; }
                            if (tempOhlcv.Low > dd.Low) { tempOhlcv.Low = dd.Low; }
                            tempOhlcv.Close = dd.Close;
                            tempOhlcv.VolumeTo = dd.VolumeTo;
                            tempOhlcv.VolumeFrom = dd.VolumeFrom;
                            data.Add(tempOhlcv);
                            periodCounter = 0;
                        }
                        else
                        {
                            periodCounter++;
                        }
                    }
                }
            }

            candlechart.Items = data;
        }

        private void btnHistoMinute_Click(object sender, RoutedEventArgs e)
        {
            GetData(Info.CryptoCompare.HistoType.Minute);
        }

        private void btnHistoHour_Click(object sender, RoutedEventArgs e)
        {
            GetData(Info.CryptoCompare.HistoType.Hour);
        }

        private void btnHistoDay_Click(object sender, RoutedEventArgs e)
        {
            GetData(Info.CryptoCompare.HistoType.Day);
        }
    }
}
