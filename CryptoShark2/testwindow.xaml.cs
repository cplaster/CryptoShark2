using System;
using System.Collections.Generic;
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
    /// Interaction logic for testwindow.xaml
    /// </summary>
    public partial class testwindow : Window
    {
        Dictionary<string, List<Info.CryptoCompare.HistoMinuteData>> cache = new Dictionary<string, List<Info.CryptoCompare.HistoMinuteData>>();

        public testwindow()
        {
            InitializeComponent();
        }
        private void GetData2(Info.CryptoCompare.HistoType type)
        {
            string exchange = "Cryptopia";
            canvas.Children.Clear();
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

            List<OhlcvData> data = new List<OhlcvData>();
            OhlcvData tempOhlcv = new OhlcvData();
            int periodCounter = 0;
            double s = 100000000;

            foreach (Info.CryptoCompare.HistoMinuteData d in dat)
            {
                DateTime time = Info.CryptoCompare.Dates.FromUnixTime(d.Time);
                if (period == 1)
                {
                    data.Add(new OhlcvData(d.Open * s, d.High * s, d.Low * s, d.Close * s, d.VolumeFrom, d.VolumeTo, time));
                } else
                {
                    if(periodCounter == 0)
                    {
                        tempOhlcv = new OhlcvData(d.Open * s, d.High * s, d.Low * s, d.Close * s, d.VolumeFrom, d.VolumeTo, time);
                        periodCounter++;
                    } else
                    {
                        if(periodCounter == period - 1)
                        {
                            OhlcvData dd = new OhlcvData(d.Open * s, d.High * s, d.Low * s, d.Close * s, d.VolumeFrom, d.VolumeTo, time);
                            if (tempOhlcv.High < dd.High) { tempOhlcv.High = dd.High; }
                            if(tempOhlcv.Low > dd.Low) { tempOhlcv.Low = dd.Low; }
                            tempOhlcv.Close = dd.Close;
                            tempOhlcv.VolumeTo = dd.VolumeTo;
                            tempOhlcv.VolumeFrom = dd.VolumeFrom;
                            data.Add(tempOhlcv);
                            periodCounter = 0;
                        } else
                        {
                            periodCounter++;
                        }
                    }
                }
            }


            Rectangle rect = new Rectangle();
            rect.Stroke = new SolidColorBrush(Colors.Gray);
            rect.StrokeThickness = 2;
            rect.Fill = new SolidColorBrush(Colors.GhostWhite);
            rect.Width = canvas.Width;
            rect.Height = canvas.Height;
            Canvas.SetLeft(rect, 0);
            Canvas.SetTop(rect, 0);
            canvas.Children.Add(rect);
            Bounds bounds = GetBounds(data);

            int height = Convert.ToInt32(Math.Ceiling(RoundUp(bounds.High) - RoundDown(bounds.Low)));
            int volheight = Convert.ToInt32(Math.Ceiling(RoundUp(bounds.VolHigh) - RoundDown(bounds.VolLow)));
            if (volheight == 0) { volheight = 1; }
            if (volheight > 10) { volheight = (volheight % 10) * 1000; }
            if (volheight == 0) { volheight = 1000; }
            if (volheight == 10) { volheight = 10; }
            if (height == 0) { height = 1; }
            double horizontalMargin = 30;
            double scale = (canvas.Height - horizontalMargin) / height;
            double volscale = (canvas.Height - horizontalMargin) / volheight;
            double linespace = Math.Floor((double)height / 10);
            if (linespace == 0) { linespace = 1; }

            double left = 0;
            int space = 5;
            double candlewidth = Math.Floor(canvas.Width / data.Count) - 5;
            int wickwidth = 1;
            List<Label> labels = new List<Label>();

            double chartHeight = canvas.Height - horizontalMargin;

            for (double i = bounds.High; i >= bounds.Low; i -= linespace)
            {
                double top = (bounds.High - i) / height * chartHeight;
                top += horizontalMargin;
                Rectangle l = new Rectangle();
                l.Stroke = new SolidColorBrush(Color.FromArgb(200, 128, 128, 128));
                l.StrokeThickness = 1;
                l.Fill = new SolidColorBrush(Color.FromArgb(200, 128, 128, 128));
                l.Width = canvas.Width;
                l.Height = 1;
                Canvas.SetLeft(l, 0);
                Canvas.SetTop(l, top);
                canvas.Children.Add(l);
                Label label = new Label();
                label.Content = (i / s).ToString("F8");
                label.Foreground = new SolidColorBrush(Colors.Gray);
                Canvas.SetLeft(label, canvas.Width - 75);
                Canvas.SetTop(label, top - 10 - 10 );
                //Canvas.SetTop(label, (bounds.High - i + horizontalMargin - linespace + (linespace * 0.3)) / height * chartHeight);
                labels.Add(label);
            }

            foreach (OhlcvData d in data)
            {
                left = left + space + candlewidth;
                Rectangle r = new Rectangle();
                Rectangle wick = new Rectangle();
                r.StrokeThickness = 1;
                wick.StrokeThickness = 1;
                double top;

                //volume stuff...
                Rectangle vol = new Rectangle();
                vol.StrokeThickness = 1;
                vol.Stroke = new SolidColorBrush(Color.FromArgb(128, 128, 128, 128));
                vol.Fill = new SolidColorBrush(Color.FromArgb(128, 128, 128, 128));
                vol.Width = candlewidth;
                double scaledVolume = GetScaledVolume(d.VolumeTo);
                vol.Height = volscale * scaledVolume;

                Canvas.SetLeft(vol, left);
                Canvas.SetTop(vol, canvas.Height - (volscale * scaledVolume));
                canvas.Children.Add(vol);

                //candles and wicks
                if (d.Close != d.Open)
                {
                    if (d.Close > d.Open)
                    {
                        r.Stroke = new SolidColorBrush(Colors.Green);
                        r.Fill = new SolidColorBrush(Colors.Green);
                        wick.Stroke = new SolidColorBrush(Colors.Green);
                        wick.Fill = new SolidColorBrush(Colors.Green);
                        top = (bounds.High - d.Close) / height * chartHeight;
                        top += horizontalMargin;
                        Canvas.SetTop(r, top);
                    }
                    else
                    {
                        r.Stroke = new SolidColorBrush(Colors.Red);
                        r.Fill = new SolidColorBrush(Colors.Red);
                        wick.Stroke = new SolidColorBrush(Colors.Red);
                        wick.Fill = new SolidColorBrush(Colors.Red);
                        top = (bounds.High - d.Open) / height * chartHeight;
                        top += horizontalMargin;
                        Canvas.SetTop(r, top);
                    }

                    r.Width = candlewidth;
                    r.Height = scale * Math.Abs(d.Open - d.Close);

                }
                else
                {
                    r.Stroke = new SolidColorBrush(Colors.CornflowerBlue);
                    r.Fill = new SolidColorBrush(Colors.CornflowerBlue);
                    wick.Stroke = new SolidColorBrush(Colors.CornflowerBlue);
                    wick.Fill = new SolidColorBrush(Colors.CornflowerBlue);

                    if (height != 0 && bounds.High - d.Close != 0)
                    {
                        top = (bounds.High - d.Close) / height * chartHeight;
                        top += horizontalMargin;
                    }
                    else
                    {
                        top = canvas.Height / 2;
                    }
                    Canvas.SetTop(r, top);

                    if (height != 0 && bounds.High - d.High != 0)
                    {
                        top = (bounds.High - d.High) / height * chartHeight;
                        top += horizontalMargin;
                    }
                    else
                    {
                        top = canvas.Height / 2;
                    }

                    Canvas.SetTop(wick, top);
                    r.Width = candlewidth;
                    r.Height = 1;

                }

                wick.Width = wickwidth;

                if (d.High != d.Low)
                {
                    wick.Height = scale * Math.Abs(d.High - d.Low);
                }
                else
                {
                    wick.Height = 1;
                }

                if (height != 0)
                {
                    top = (bounds.High - d.High) / height * chartHeight;
                    top += horizontalMargin;
                }
                else
                {
                    top = canvas.Height / 2;
                }

                Canvas.SetTop(wick, top);

                Canvas.SetLeft(r, left);
                Canvas.SetLeft(wick, left + (candlewidth /2 ));

                canvas.Children.Add(r);
                canvas.Children.Add(wick);

            }

            foreach (Label l in labels)
            {
                canvas.Children.Add(l);
            }

        }

        private double GetScaledVolume(double d)
        {
            string s = d.ToString("F8");
            int i = 0;
            double ret = d;
            if (d != 0)
            {
                if (s.Contains("0.0"))
                {
                    string[] temp = s.Split(".".ToCharArray());
                    string t = temp[1];
                    while (t[0].ToString() == "0")
                    {
                        t = t.Substring(1, t.Length - 1);
                        i++;
                    }
                    t = "0." + t;
                    ret = (10 ^ i) * Convert.ToDouble(t);
                }
            }

            return ret;
        }

        private void GetData(Info.CryptoCompare.HistoType type) 
        {
            string exchange = "Cryptopia";
            canvas.Children.Clear();
            MarketPair mp = GetMarketPair();
            List<Info.CryptoCompare.HistoMinuteData> dat;
            string histoLimit = txtlimit.Text;

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

            List<OhlcvData> data = new List<OhlcvData>();
            double s = 100000000;

            foreach(Info.CryptoCompare.HistoMinuteData d in dat)
            {
                DateTime time = Info.CryptoCompare.Dates.FromUnixTime(d.Time);
                data.Add(new OhlcvData(d.Open*s, d.High*s, d.Low*s, d.Close*s, d.VolumeFrom, d.VolumeTo, time));
            }


            Rectangle rect = new Rectangle();
            rect.Stroke = new SolidColorBrush(Colors.Gray);
            rect.StrokeThickness = 2;
            rect.Fill = new SolidColorBrush(Colors.Black);
            rect.Width = canvas.Width;
            rect.Height = canvas.Height;
            Canvas.SetLeft(rect, 0);
            Canvas.SetTop(rect, 0);
            canvas.Children.Add(rect);
            Bounds bounds = GetBounds(data);

            int height = Convert.ToInt32(Math.Ceiling(RoundUp(bounds.High) - RoundDown(bounds.Low)));
            if(height == 0) { height = 1; }
            double scale = canvas.Height / height;
            double linespace = Math.Floor((double)height / 10);
            if(linespace == 0) { linespace = 1; }

            for (double i = bounds.High; i >= bounds.Low; i -= linespace)
            {
                double top = (bounds.High - i) / height * canvas.Height;
                Rectangle l = new Rectangle();
                l.Stroke = new SolidColorBrush(Colors.Gray);
                l.StrokeThickness = 1;
                l.Fill = new SolidColorBrush(Colors.Gray);
                l.Width = canvas.Width;
                l.Height = 1;
                Canvas.SetLeft(l, 0);
                Canvas.SetTop(l, top);
                canvas.Children.Add(l);
                Label label = new Label();
                label.Content = (i / s).ToString("F8");
                label.Foreground = new SolidColorBrush(Colors.Gray);
                Canvas.SetLeft(label, canvas.Width - 75);
                Canvas.SetTop(label, (bounds.High - i - linespace) / height * canvas.Height);
                canvas.Children.Add(label);
            }

            int left = 0;
            int space = 5;
            int candlewidth = 5;
            int wickwidth = 1;

            foreach(OhlcvData d in data)
            {
                left = left + space + candlewidth;
                Rectangle r = new Rectangle();
                Rectangle wick = new Rectangle();
                r.StrokeThickness = 1;
                wick.StrokeThickness = 1;
                double top;

                if(d.Close != d.Open)
                {
                    if(d.Close > d.Open)
                    {
                        r.Stroke = new SolidColorBrush(Colors.Green);
                        r.Fill = new SolidColorBrush(Colors.Green);
                        wick.Stroke = new SolidColorBrush(Colors.Green);
                        wick.Fill = new SolidColorBrush(Colors.Green);
                        top = top = (bounds.High - d.Close) / height * canvas.Height;
                        Canvas.SetTop(r, top);
                    } else
                    {
                        r.Stroke = new SolidColorBrush(Colors.Red);
                        r.Fill = new SolidColorBrush(Colors.Red);
                        wick.Stroke = new SolidColorBrush(Colors.Red);
                        wick.Fill = new SolidColorBrush(Colors.Red);
                        top = (bounds.High - d.Open) / height * canvas.Height;
                        Canvas.SetTop(r, top);
                    }

                    r.Width = candlewidth;
                    r.Height = scale * Math.Abs(d.Open - d.Close);

                } else
                {
                    r.Stroke = new SolidColorBrush(Colors.CornflowerBlue);
                    r.Fill = new SolidColorBrush(Colors.CornflowerBlue);
                    wick.Stroke = new SolidColorBrush(Colors.CornflowerBlue);
                    wick.Fill = new SolidColorBrush(Colors.CornflowerBlue);

                    if (height != 0 && bounds.High - d.Close != 0)
                    {
                        top = (bounds.High - d.Close) / height * canvas.Height;
                    } else
                    {
                        top = canvas.Height / 2;
                    }
                    Canvas.SetTop(r, top);

                    if(height != 0 && bounds.High - d.High != 0)
                    {
                        top = (bounds.High - d.High) / height * canvas.Height;
                    } else
                    {
                        top = canvas.Height / 2;
                    }

                    Canvas.SetTop(wick, top); 
                    r.Width = candlewidth;
                    r.Height = 1;

                }

                wick.Width = wickwidth;

                if(d.High != d.Low)
                {
                    wick.Height = scale * Math.Abs(d.High - d.Low);
                } else
                {
                    wick.Height = 1;
                }

                if (height != 0)
                {
                    top = (bounds.High - d.High) / height * canvas.Height;
                }
                else
                {
                    top = canvas.Height / 2;
                }

                Canvas.SetTop(wick, top);

                Canvas.SetLeft(r, left);
                Canvas.SetLeft(wick, left + 2);

                canvas.Children.Add(r);
                canvas.Children.Add(wick);
            }

        }

        public double RoundUp(double i)
        {
            return Math.Ceiling(i / 10.0d) * 10;
        }

        public double RoundDown(double i)
        {
            return Math.Floor(i / 10.0d) * 10;
        }

        public Bounds GetBounds(List<OhlcvData> data)
        {
            double high = 0;
            double low = int.MaxValue;
            double volhigh = 0;
            double vollow = int.MaxValue;

            foreach (OhlcvData d in data)
            {
                if (d.High > high) { high = d.High; }
                if (d.Open > high) { high = d.Open; }
                if (d.Close > high) { high = d.Close; }
                if (d.Low > high) { high = d.Low; }
                if (d.VolumeTo > volhigh) { volhigh = d.VolumeTo; }


                if (d.High < low) { low = d.High; }
                if (d.Open < low) { low = d.Open; }
                if (d.Close < low) { low = d.Close; }
                if (d.Low < low) { low = d.Low; }
                if (d.VolumeTo < vollow) { vollow = d.VolumeTo; }
            }

            Bounds b = new Bounds();
            b.High = high;
            b.Low = low;
            b.VolHigh = volhigh;
            b.VolLow = vollow;

            return b;
        }

        public struct Bounds
        {
            public double High;
            public double Low;
            public double VolHigh;
            public double VolLow;
        }

        public class OhlcvData
        {
            public double Open { get; set; }
            public double High { get; set; }
            public double Low { get; set; }
            public double Close { get; set; }
            public double VolumeFrom { get; set; }
            public double VolumeTo { get; set; }
            public DateTime TimeStamp { get; set; }

            public OhlcvData() { }

            public OhlcvData(double open, double high, double low, double close, double volumeFrom, double volumeTo, DateTime timeStamp)
            {
                Open = open;
                High = high;
                Low = low;
                Close = close;
                VolumeFrom = volumeFrom;
                VolumeTo = volumeTo;
                TimeStamp = timeStamp;
            }
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

        private void btnHistoMinute_Click(object sender, RoutedEventArgs e)
        {
            GetData2(Info.CryptoCompare.HistoType.Minute);
        }

        private void btnHistoHour_Click(object sender, RoutedEventArgs e)
        {
            GetData2(Info.CryptoCompare.HistoType.Hour);
        }

        private void btnHistoDay_Click(object sender, RoutedEventArgs e)
        {
            GetData2(Info.CryptoCompare.HistoType.Day);
        }
    }
}
