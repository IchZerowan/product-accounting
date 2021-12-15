using Newtonsoft.Json;
using product_accounting_frontend.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace product_accounting_frontend.dao
{
    internal class TransactionsDAO
    {
        public async Task<List<Transaction>> executeGetTransaction(){
            List<Transaction> transactions = new List<Transaction>();
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(@"http://product-accounting.us-east-2.elasticbeanstalk.com/transactions");
                string responseJSON = await response.Content.ReadAsStringAsync();
                transactions = JsonConvert.DeserializeObject<List<Transaction>>(responseJSON);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return transactions;
        }
    }
}
