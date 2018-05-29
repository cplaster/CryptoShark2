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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System.IO;

namespace CryptoShark2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Private Variables

        private const string defaultFileName = "default.cshark";

        // these are never used directly, but are necessary for type inspection.
        //private Bittrex.Api Bittrex;
        private Exchanges.Cryptopia.Api Cryptopia;
        //private HitBTC.Api HitBTC;
        //private Poloniex.Api Poloniex;
        //private KuCoin.Api KuCoin;
        private Dictionary<string, IExchange> ExchangeList;

        //Configuration
        private Configuration configuration;
        private string pairBase;
        private string pairCurrency;

        #endregion

        public MainWindow()
        {
            InitializeComponent();

            ExchangeList = new Dictionary<string, IExchange>();

            if (File.Exists(defaultFileName))
            {
                configuration = Configuration.Load(defaultFileName);
            }

            if (configuration == null)
            {
                configuration = new Configuration();
            }

            foreach (KeyValuePair<string, Configuration.ExchangeConfig> kv in configuration.ExchangeSettings)
            {
                // could just do a Type inspection to get it exact item we are looking for, but this is probably quick enough
                foreach (object o in menuExchanges.Items)
                {
                    if (o.GetType() == typeof(MenuItem))
                    {
                        MenuItem mi = (MenuItem)o;

                        if (mi.Name.Contains(kv.Key))
                        {
                            mi.IsChecked = kv.Value.isActive;
                        }
                    }
                }
            }

            foreach (KeyValuePair<string, object> kv in configuration.UiSettings)
            {
                foreach (object o in menuOptions.Items)
                {
                    if (o.GetType() == typeof(MenuItem))
                    {
                        MenuItem mi = (MenuItem)o;

                        if (mi.Name.Contains(kv.Key))
                        {
                            if (mi.IsCheckable)
                            {
                                mi.IsChecked = (bool)kv.Value;
                            }
                        }
                    }
                }

                FieldInfo f = this.GetType().GetField(kv.Key, BindingFlags.Instance | BindingFlags.NonPublic);

                if (f.FieldType == typeof(TextBox))
                {
                    ((TextBox)f.GetValue(this)).Text = (string)kv.Value;
                }
            }

            SynchOptionsMenuItems();

        }

        #region Configuration Class 

        [Serializable]
        public class Configuration
        {
            public Dictionary<string, ExchangeConfig> ExchangeSettings { get; }
            public Dictionary<string, object> UiSettings { get; }
            public string FileName;

            public Configuration()
            {
                ExchangeSettings = new Dictionary<string, ExchangeConfig>();
                UiSettings = new Dictionary<string, object>();
            }

            public static Configuration Load(string fileName)
            {
                Stream stream = File.Open(fileName, FileMode.Open);
                BinaryFormatter bformatter = new BinaryFormatter();
                Configuration conf = null;
                try
                {
                    conf = (Configuration)bformatter.Deserialize(stream);
                }
                catch (Exception e)
                {
                    // failed to deserialize the config file. Just return null.
                }
                return conf;
            }

            public void Save(string fileName)
            {
                FileName = fileName;
                Stream stream = File.Open(fileName, FileMode.Create);
                BinaryFormatter bformatter = new BinaryFormatter();
                bformatter.Serialize(stream, this);
                stream.Close();
            }

            public static void Save(string fileName, Configuration Configuration)
            {
                Configuration.FileName = fileName;
                Stream stream = File.Open(fileName, FileMode.Create);
                BinaryFormatter bformatter = new BinaryFormatter();
                bformatter.Serialize(stream, Configuration);
                stream.Close();
            }

            [Serializable]
            public class ExchangeConfig
            {
                public string Name { get; set; }
                public bool isActive { get; set; }
                public string ApiPublicKey { get; set; }
                public string ApiPrivateKey { get; set; }
                public string ApiSecret { get; set; }

                public ExchangeConfig()
                {

                }
            }

        }

        #endregion

        #region Helper Methods

        private string InitializeSim(string pair)
        {
            if (ExchangeList.Count == 0)
            {
                foreach (KeyValuePair<string, Configuration.ExchangeConfig> kv in configuration.ExchangeSettings)
                {
                    if (kv.Value.isActive)
                    {
                        Type type = Type.GetType("CryptoShark2.Exchanges." + kv.Value.Name + ".Api", true);
                        IExchange exchange = (IExchange)Activator.CreateInstance(type);
                        exchange.Refresh();
                        ExchangeList.Add(kv.Key, exchange);
                    }
                }
            }

            string[] a = pair.Split("-".ToCharArray());

            if (a.Count() == 2)
            {
                return a[1];
            }
            else
            {
                return null;
            }
        }

        private void SetCurrencyPair()
        {
            string[] a = txtTradePair.Text.Split("-".ToCharArray());
            if (a.Length == 2)
            {
                pairBase = a[0];
                lblBasePair.Content = pairBase + " Amount:";
                pairCurrency = a[1];
                lblCurrencyPair.Content = pairCurrency + " Amount:";
            }
        }

        #endregion

        #region Helper Classes / Structs

        private struct BuyPower
        {
            public int BuyDepth;
            public int SellDepth;
            public double Volume;
            public double Balance;
        }

        #endregion

        #region MenuBar Handlers

        private void menuFileSave_Click(object sender, RoutedEventArgs e)
        {
            SynchTextBoxes();

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.DefaultExt = ".cshark";
            dlg.Filter = "CryptoShark Config (*.cshark)|*.cshark";
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                configuration.FileName = dlg.FileName;
                configuration.Save(dlg.FileName);
            }
        }

        private void SynchTextBoxes()
        {
            FieldInfo[] fi = this.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (FieldInfo f in fi)
            {
                if (f.FieldType == typeof(TextBox))
                {
                    if (!configuration.UiSettings.ContainsKey(f.Name))
                    {
                        configuration.UiSettings.Add(f.Name, null);
                    }

                    var mm = this.GetType().GetField(f.Name, BindingFlags.Instance | BindingFlags.NonPublic);
                    configuration.UiSettings[f.Name] = ((TextBox)mm.GetValue(this)).Text;
                }
            }
        }

        private void menuFileOpen_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".cshark";
            dlg.Filter = "CryptoShark Config (*.cshark)|*.cshark";
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                configuration = Configuration.Load(dlg.FileName);
            }
        }

        private void menuFileExit_Click(object sender, RoutedEventArgs e)
        {
            if (configuration.FileName == null || configuration.FileName == "")
            {
                configuration.FileName = "default.cshark";
            }

            SynchTextBoxes();

            configuration.Save(configuration.FileName);

            Close();
        }

        private void menuExchanges_SubmenuClosed(object sender, RoutedEventArgs e)
        {
            if (menuExchangesAll.IsChecked)
            {
                if (configuration.ExchangeSettings.Count == 0)
                {
                    SynchExchangeMenuItems();
                }

                /*
                foreach (KeyValuePair<string, Configuration.ExchangeConfig> kv in configuration.ExchangeSettings)
                {
                    kv.Value.isActive = true;
                }
                */
            }
            else
            {
                SynchExchangeMenuItems();
            }
        }

        private void menuOptions_SubmenuClosed(object sender, RoutedEventArgs e)
        {
            SynchOptionsMenuItems();
        }

        private void SynchOptionsMenuItems()
        {
            foreach (object o in menuOptions.Items)
            {
                if (o.GetType() == typeof(MenuItem))
                {
                    MenuItem mi = (MenuItem)o;
                    string name = mi.Name;

                    if (!configuration.UiSettings.Keys.Contains(name))
                    {
                        configuration.UiSettings.Add(name, null);
                    }

                    if (mi.IsCheckable)
                    {
                        configuration.UiSettings[name] = mi.IsChecked;
                    }
                }
            }
        }

        private void SynchExchangeMenuItems()
        {
            foreach (object o in menuExchanges.Items)
            {
                if (o.GetType() == typeof(MenuItem))
                {
                    MenuItem mi = (MenuItem)o;
                    string name = mi.Name.Replace("menuExchanges", "");

                    if (!configuration.ExchangeSettings.Keys.Contains(name))
                    {
                        configuration.ExchangeSettings.Add(name, new Configuration.ExchangeConfig());
                    }

                    configuration.ExchangeSettings[name].isActive = mi.IsChecked;
                    configuration.ExchangeSettings[name].Name = name;
                }
            }
        }

        #endregion

        #region Input Handlers

        private void txtTradePair_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SetCurrencyPair();
            }
        }

        private void txtTradePair_LostFocus(object sender, RoutedEventArgs e)
        {
            SetCurrencyPair();
        }

        #endregion

        #region Button Handlers

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtResults.Clear();
            ExchangeList.Clear();
        }

        private void btnSimBuy_Click(object sender, RoutedEventArgs e)
        {
            string pair = txtTradePair.Text;
            string pairCoin = InitializeSim(pair);
            double baseamount = Convert.ToDouble(txtBaseAmount.Text);

            foreach (KeyValuePair<string, IExchange> kv in ExchangeList)
            {
                IExchange ex = kv.Value;

                bool ignoreInactive = (bool)configuration.UiSettings["menuOptionsIgnoreInactive"];
                CoinInfo.CoinStatus coinStatus = CoinInfo.CoinStatus.OK;

                // If the ignoreInactive option is set, pretend that the pair is active even if it isn't.
                // Otherwise, use the Unknown value.
                if(!ignoreInactive)
                {
                    coinStatus = ex.CoinInfo[pairCoin].Status;
                    if (!(ex.CoinInfo.ContainsKey(pairCoin) && coinStatus == CoinInfo.CoinStatus.OK))
                    {
                        coinStatus = CoinInfo.CoinStatus.Unknown;
                    }
                }

                if (ex.Summary.ContainsKey(pair) && coinStatus == CoinInfo.CoinStatus.OK)
                {
                    BuyPower buy = new BuyPower();
                    buy.SellDepth = 0;
                    buy.Volume = 0;
                    buy.Balance = baseamount;

                    double fee = ex.Markets[pair].TakerFeeRate;
                    BookInfo book = ex.GetOrderBook(pair);

                    foreach (BookInfo.Order order in book.Sells)
                    {
                        double totalPrice = order.Price * order.Volume;
                        double pricePlusFee = totalPrice + (totalPrice * fee);
                        double minPricePlusFee = order.Price + (order.Price * fee);
                        double maxShares = buy.Balance / order.Price;
                        double maxPricePlusFee = (maxShares * order.Price) + (maxShares * order.Price * fee);
                        double minTradeAmount = ex.Markets[pair].MinTradeSize;

                        if (buy.Balance > minPricePlusFee)
                        {
                            buy.BuyDepth++;
                            double total = order.Price * order.Volume;
                            double priceplusfee = total + (total * fee);

                            if (buy.Balance > priceplusfee)
                            {
                                buy.Volume += order.Volume;
                                buy.Balance -= priceplusfee;
                            }
                            else
                            {
                                while (buy.Balance < maxPricePlusFee)
                                {
                                    maxShares -= 1;
                                    maxPricePlusFee = (maxShares * order.Price) + (maxShares * order.Price * fee);
                                }

                                buy.Volume += maxShares;
                                buy.Balance -= maxPricePlusFee;
                                break;
                            }
                        }
                    }

                    double spent = baseamount - buy.Balance;

                    txtResults.Text += "\n" + kv.Key + " (" + pair + ")\n";
                    txtResults.Text += "Bought " + buy.Volume.ToString() + " " + pairCoin + " for " + spent.ToString() + " " + pairBase + " (" + (spent / buy.Volume).ToString("F8") + ")\n";

                    txtResults.ScrollToEnd();
                }
            }
        }

        private void btnSimSell_Click(object sender, RoutedEventArgs e)
        {
            string pair = txtTradePair.Text;
            string pairCoin = InitializeSim(pair);
            double currencyAmount = Convert.ToDouble(txtCurrencyAmount.Text);

            foreach (KeyValuePair<string, IExchange> kv in ExchangeList)
            {
                IExchange ex = kv.Value;

                bool ignoreInactive = (bool)configuration.UiSettings["menuOptionsIgnoreInactive"];
                CoinInfo.CoinStatus coinStatus = CoinInfo.CoinStatus.OK;

                // If the ignoreInactive option is set, pretend that the pair is active even if it isn't.
                // Otherwise, use the actual IsActive value
                if (!ignoreInactive)
                {
                    if (!(ex.CoinInfo.ContainsKey(pairCoin) && coinStatus == CoinInfo.CoinStatus.OK))
                    {
                        coinStatus = CoinInfo.CoinStatus.Unknown;
                    }
                }

                bool haskey = ex.Markets.ContainsKey(pair);

                if (ex.Summary.ContainsKey(pair) && coinStatus == CoinInfo.CoinStatus.OK)
                {
                    BuyPower sell = new BuyPower();
                    sell.SellDepth = 0;
                    sell.Volume = currencyAmount;
                    sell.Balance = 0;

                    double minTradeAmount = ex.Markets[pair].MinTradeSize;
                    double fee = ex.Markets[pair].MakerFeeRate;
                    BookInfo book = ex.GetOrderBook(pair);



                    foreach (BookInfo.Order order in book.Buys)
                    {
                        if (sell.Volume * order.Price > minTradeAmount)
                        {
                            double priceMinusFee;
                            double total = order.Price * order.Volume;
                            sell.SellDepth++;

                            if (sell.Volume >= order.Volume)
                            {
                                priceMinusFee = total - (total * fee);
                                sell.Volume -= order.Volume;
                                sell.Balance += priceMinusFee;

                            }
                            else
                            {
                                priceMinusFee = (sell.Volume * order.Price) - (sell.Volume * order.Price * fee);
                                sell.Volume = 0;
                                sell.Balance += priceMinusFee;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    double sold = currencyAmount - sell.Volume;

                    txtResults.Text += "\n" + kv.Key + " (" + pair + ")\n";
                    txtResults.Text += "Sold " + sold.ToString() + " " + pairCoin + " for " + sell.Balance.ToString() + " " + pairBase + " (" + (sell.Balance / sold).ToString("F8") + ")\n";

                    txtResults.ScrollToEnd();
                }
            }
        }

        private void btnGetInfo_Click(object sender, RoutedEventArgs e)
        {

            Info.CryptoCompare.Api api = new Info.CryptoCompare.Api();
            api.Refresh();
            int count = api.CoinList.Count();
            Info.CryptoCompare.CoinFullInfoData dat = api.GetFullInfo(1182);
            int i = 0;
        }

        #endregion

    }
}
