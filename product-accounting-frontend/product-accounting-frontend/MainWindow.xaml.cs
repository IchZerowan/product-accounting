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

        private void startupCheckProducts()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://product-accounting.us-east-2.elasticbeanstalk.com/products");
            List<Product> products = new List<Product>();
            string responseJSON;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                responseJSON = reader.ReadToEnd();
            }
            textBlock1.Text = "";
            textBlock1.Text = responseJSON;
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
