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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CryptoShark2
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:CryptoShark2"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:CryptoShark2;assembly=CryptoShark2"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    public class CandleChart : Canvas
    {

        //private ObservableCollection<OhlcvData> _Items = new ObservableCollection<OhlcvData>();
        //public ObservableCollection<OhlcvData> Items { get { return _Items; } }

        #region DependencyProperties

        public ObservableCollection<OhlcvData> Items
        {
            get { return (ObservableCollection<OhlcvData>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register(
            "Items",
             typeof(ObservableCollection<OhlcvData>),
            typeof(CandleChart),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.AffectsRender,
                new PropertyChangedCallback(OnItemsChanged)
             )
        );

        public Brush NullCandleColor
        {
            get { return (Brush)GetValue(NullCandleColorProperty); }
            set { SetValue(NullCandleColorProperty, value); }
        }

        public static readonly DependencyProperty NullCandleColorProperty = DependencyProperty.Register(
            "NullCandleColor",
             typeof(Brush),
            typeof(CandleChart),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.AffectsRender,
                new PropertyChangedCallback(OnColorChanged)
            )
        );

        public Brush NullWickColor
        {
            get { return (Brush)GetValue(NullWickColorProperty); }
            set { SetValue(NullWickColorProperty, value); }
        }

        public static readonly DependencyProperty NullWickColorProperty = DependencyProperty.Register(
            "NullWickColor",
             typeof(Brush),
            typeof(CandleChart),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.AffectsRender,
                new PropertyChangedCallback(OnColorChanged)
            )
        );

        public Brush LossWickColor
        {
            get { return (Brush)GetValue(LossWickColorProperty); }
            set { SetValue(LossWickColorProperty, value); }
        }

        public static readonly DependencyProperty LossWickColorProperty = DependencyProperty.Register(
            "LossWickColor",
             typeof(Brush),
            typeof(CandleChart),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.AffectsRender,
                new PropertyChangedCallback(OnColorChanged)
            )
        );

        public Brush GainWickColor
        {
            get { return (Brush)GetValue(GainWickColorProperty); }
            set { SetValue(GainWickColorProperty, value); }
        }

        public static readonly DependencyProperty GainWickColorProperty = DependencyProperty.Register(
            "GainWickColor",
             typeof(Brush),
            typeof(CandleChart),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.AffectsRender,
                new PropertyChangedCallback(OnColorChanged)
            )
        );

        public Brush LossCandleColor
        {
            get { return (Brush)GetValue(LossCandleColorProperty); }
            set { SetValue(LossCandleColorProperty, value); }
        }

        public static readonly DependencyProperty LossCandleColorProperty = DependencyProperty.Register(
            "LossCandleColor",
             typeof(Brush),
            typeof(CandleChart),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.AffectsRender,
                new PropertyChangedCallback(OnColorChanged)
            )
        );

        public Brush GainCandleColor
        {
            get { return (Brush)GetValue(GainCandleColorProperty); }
            set { SetValue(GainCandleColorProperty, value); }
        }

        public static readonly DependencyProperty GainCandleColorProperty = DependencyProperty.Register(
            "GainCandleColor",
             typeof(Brush),
            typeof(CandleChart),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.AffectsRender,
                new PropertyChangedCallback(OnColorChanged)
            )
        );

        public Brush BorderColor
        {
            get { return (Brush)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        public static readonly DependencyProperty BorderColorProperty = DependencyProperty.Register(
            "BorderColor",
             typeof(Brush),
            typeof(CandleChart),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.AffectsRender,
                new PropertyChangedCallback(OnColorChanged)
            )
        );

        public Brush ForegroundColor
        {
            get { return (Brush)GetValue(ForegroundColorProperty); }
            set { SetValue(ForegroundColorProperty, value); }
        }

        public static readonly DependencyProperty ForegroundColorProperty = DependencyProperty.Register(
            "ForegroundColor",
             typeof(Brush),
            typeof(CandleChart),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.AffectsRender,
                new PropertyChangedCallback(OnColorChanged)
            )
        );

        public Brush BackgroundColor
        {
            get { return (Brush)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }

        public static readonly DependencyProperty BackgroundColorProperty = DependencyProperty.Register(
            "BackgroundColor",
            typeof(Brush),
            typeof(CandleChart),
            new FrameworkPropertyMetadata(
                null, 
                FrameworkPropertyMetadataOptions.AffectsRender, 
                new PropertyChangedCallback(OnColorChanged)
            )
        );

        private static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        private static void OnItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CandleChart control = (CandleChart)d;
            if (control.Items.Count > 0)
            {
                control.Render();
            }
        }

        #endregion

        static CandleChart()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CandleChart), new FrameworkPropertyMetadata(typeof(CandleChart)));
        }

        public CandleChart()
        {
            Items = new ObservableCollection<OhlcvData>(); ;
            Items.CollectionChanged += _Items_CollectionChanged;
        }

        private void _Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.Render();
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

        private double GetScaledVolume(double d)
        {
            string s = d.ToString("F8");
            int i = 0;
            double ret = d;
            if (d != 0)
            {
                if (s.Substring(0, 3).Contains("0.0"))
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
                else
                {
                    string[] temp = s.Split(".".ToCharArray());
                    string t = temp[0];
                    i = t.Count();
                    int j = 0;
                    if (i > 2) { 
                        while (i > 2)
                        {
                            j++;
                            i--;
                        }
                        ret = (10 ^ -j) * d;
                    }
                }
            }

            return ret;
        }

        public double ScaleVolume(double d)
        {

            double ret = d;

            if (d >= 1)
            {
                //check how many lefthand significant digits we have
                string s = d.ToString();
                string[] temp = s.Split(".".ToCharArray());
                string a = temp[0];
                int j = 0;
                int i = a.Count();
                if (i > 1)
                {
                    while (i > 1)
                    {
                        j++;
                        i--;
                    }

                    ret = Math.Pow(10, -(j - 2));
                }
                else
                {
                    ret = Math.Pow(10, 1);
                }
            }

            if (d < 1)
            {
                //check how many righthand significant digits we have
                string s = d.ToString();
                string[] temp = s.Split(".".ToCharArray());
                string a = temp[1];
                int i = 0;
                int j = 0;
                if (a.Substring(0, 1) == "0")
                {
                    while (a.Substring(0, 1) == "0")
                    {
                        a = a.Substring(1, a.Count() - 1);
                        i++;
                    }
                    ret = Math.Pow(10, i);
                } else
                {
                    ret = Math.Pow(10, 1);
                }
            }

            return ret;
        }

        public void Render()
        {
            Canvas canvas = this;
            List<OhlcvData> data = new List<OhlcvData>();

            foreach(OhlcvData d in Items)
            {
                data.Add(d);
            }

            double s = 100000000;
            Rectangle rect = new Rectangle();
            rect.Stroke = BorderColor;
            rect.StrokeThickness = 2;
            rect.Fill = BackgroundColor;
            rect.Width = canvas.ActualWidth;
            rect.Height = canvas.ActualHeight;
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
            double scale = (canvas.ActualHeight - horizontalMargin) / height;
            double volscale = (canvas.ActualHeight - horizontalMargin) / volheight;
            double linespace = Math.Floor((double)height / 10);
            if (linespace == 0) { linespace = 1; }

            double left = 0;
            int space = 5;
            double candlewidth = Math.Floor(canvas.ActualWidth / data.Count) - 5;
            int wickwidth = 1;
            List<Label> labels = new List<Label>();

            double chartHeight = canvas.ActualHeight - horizontalMargin;

            for (double i = bounds.High; i >= bounds.Low; i -= linespace)
            {
                double top = (bounds.High - i) / height * chartHeight;
                top += horizontalMargin;
                Rectangle l = new Rectangle();
                l.Stroke = ForegroundColor;
                l.StrokeThickness = 1;
                l.Fill = ForegroundColor;
                l.Width = canvas.ActualWidth;
                l.Height = 1;
                Canvas.SetLeft(l, 0);
                Canvas.SetTop(l, top);
                canvas.Children.Add(l);
                Label label = new Label();
                label.Content = (i / s).ToString("F8");
                label.Foreground = ForegroundColor;
                Canvas.SetLeft(label, canvas.ActualWidth - 75);
                Canvas.SetTop(label, top - 10 - 10);
                //Canvas.SetTop(label, (bounds.High - i + horizontalMargin - linespace + (linespace * 0.3)) / height * chartHeight);
                labels.Add(label);
            }

            double test = ScaleVolume(bounds.VolHigh - bounds.VolLow);
            while(test * bounds.VolHigh * 10 < (canvas.ActualHeight / 2) + 50)
            {
                test = test * 10;
            }
            test = test / 2;

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
                vol.Stroke = ForegroundColor;
                vol.Fill = ForegroundColor;
                vol.Width = candlewidth;
                double scaledVolume = GetScaledVolume(d.VolumeTo);
                

                //vol.Height = volscale * scaledVolume;
                vol.Height = test * d.VolumeTo;

                Canvas.SetLeft(vol, left);
                //Canvas.SetTop(vol, canvas.ActualHeight - (volscale * scaledVolume));
                Canvas.SetTop(vol, canvas.ActualHeight - (test * d.VolumeTo));
                canvas.Children.Add(vol);

                //candles and wicks
                if (d.Close != d.Open)
                {
                    if (d.Close > d.Open)
                    {
                        r.Stroke = GainCandleColor;
                        r.Fill = GainCandleColor;
                        wick.Stroke = GainWickColor;
                        wick.Fill = GainWickColor;
                        top = (bounds.High - d.Close) / height * chartHeight;
                        top += horizontalMargin;
                        Canvas.SetTop(r, top);
                    }
                    else
                    {
                        r.Stroke = LossCandleColor;
                        r.Fill = LossCandleColor;
                        wick.Stroke = LossWickColor;
                        wick.Fill = LossWickColor;
                        top = (bounds.High - d.Open) / height * chartHeight;
                        top += horizontalMargin;
                        Canvas.SetTop(r, top);
                    }

                    r.Width = candlewidth;
                    r.Height = scale * Math.Abs(d.Open - d.Close);

                }
                else
                {
                    r.Stroke = NullCandleColor;
                    r.Fill = NullCandleColor;
                    wick.Stroke = NullWickColor;
                    wick.Fill = NullWickColor;

                    if (height != 0 && bounds.High - d.Close != 0)
                    {
                        top = (bounds.High - d.Close) / height * chartHeight;
                        top += horizontalMargin;
                    }
                    else
                    {
                        top = 1 / height * chartHeight;
                        top += horizontalMargin;
                    }
                    Canvas.SetTop(r, top);

                    if (height != 0 && bounds.High - d.High != 0)
                    {
                        top = (bounds.High - d.High) / height * chartHeight;
                        top += horizontalMargin;
                    }
                    else
                    {
                        top = 1 / height * chartHeight;
                        top += horizontalMargin;
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
                    top = canvas.ActualHeight / 2;
                }

                Canvas.SetTop(wick, top);

                Canvas.SetLeft(r, left);
                Canvas.SetLeft(wick, left + (candlewidth / 2));

                canvas.Children.Add(r);
                canvas.Children.Add(wick);

            }

            foreach (Label l in labels)
            {
                canvas.Children.Add(l);
            }
        }

        public double RoundDown(double i)
        {
            return Math.Floor(i / 10.0d) * 10;
        }

        public double RoundUp(double i)
        {
            return Math.Ceiling(i / 10.0d) * 10;
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

    }
}
