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
using System.Net;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json;

namespace product_accounting_frontend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            startupCheckProducts();
        }

        private async void startupCheckProducts()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(@"http://product-accounting.us-east-2.elasticbeanstalk.com/products");  
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://product-accounting.us-east-2.elasticbeanstalk.com/products");           
            string responseJSON = await response.Content.ReadAsStringAsync();
            textBlock1.Text = "";
            textBlock1.Text = responseJSON;
            List<Product> products = new List<Product>();
            try
            {
                products = JsonConvert.DeserializeObject<List<Product>>(responseJSON);
            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            for(int i = 0; i < products.Count; i++)
            {
                Console.WriteLine(products[i].ToString());
            }
        }
    }
}
