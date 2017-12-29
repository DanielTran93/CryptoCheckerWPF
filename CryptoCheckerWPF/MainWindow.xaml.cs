using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace CoinMarketCapWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public class CoinValues
        {

            public string id { get; set; }
            public string name { get; set; }
            public string symbol { get; set; }
            public string rank { get; set; }
            public string price_usd { get; set; }
            public string price_btc { get; set; }
            public string __invalid_name__24h_volume_usd { get; set; }
            public string market_cap_usd { get; set; }
            public string available_supply { get; set; }
            public string total_supply { get; set; }
            public string max_supply { get; set; }
            public string percent_change_1h { get; set; }
            public string percent_change_24h { get; set; }
            public string percent_change_7d { get; set; }
            public string last_updated { get; set; }

        }

        async void getPrice_Click(object sender, RoutedEventArgs e)
        {
            string crypto = cryptoName.Text;
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync("https://api.coinmarketcap.com/v1/ticker/" + crypto))
                {
                    using (HttpContent content = response.Content)
                    {
                        string myContent = await content.ReadAsStringAsync();
                        List<CoinValues> json = JsonConvert.DeserializeObject<List<CoinValues>>(myContent);
                        cryptoID.Text = json[0].id;
                        cryptoPrice.Text = "$"+json[0].price_usd;
                        cryptoMarketCap.Text = "$"+json[0].market_cap_usd;
                        crypto1HrChange.Text = json[0].percent_change_1h + "%";
                        crypto24HrChange.Text = json[0].percent_change_24h + "%";
                        crypto7DayChange.Text = json[0].percent_change_7d + "%";

                        //need to catch exception when coin is not valid.

                    }

                }
            }

        }

    }
}
