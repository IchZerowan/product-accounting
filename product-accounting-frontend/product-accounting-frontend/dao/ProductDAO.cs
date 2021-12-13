using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace product_accounting_frontend.dao
{
    internal class ProductDAO : IGenericDAO<Product>
    {     
        public async Task<bool> executePutQuery(int id, Product product)
        {
            HttpClient client = new HttpClient();
            JObject jObject = JObject.FromObject(product);
            jObject["id"].Parent.Remove();
            jObject["archived"].Parent.Remove();
            string jsonString = jObject.ToString();
            HttpContent httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(@"http://product-accounting.us-east-2.elasticbeanstalk.com/products/" + id, httpContent);
            return true;
        }

        public async Task<bool> executeDeleteQuery(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.DeleteAsync(@"http://product-accounting.us-east-2.elasticbeanstalk.com/products/" + id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }

        public async Task<List<Product>> executeGetQuery()
        {
            List<Product> products = new List<Product>();
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(@"http://product-accounting.us-east-2.elasticbeanstalk.com/products");
                string responseJSON = await response.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<List<Product>>(responseJSON);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return products;
        }

        public async Task<bool> executePostQuery(Product product)
        {
            HttpClient client = new HttpClient();
            JObject jObject = JObject.FromObject(product);
            jObject["id"].Parent.Remove();
            jObject["archived"].Parent.Remove();
            string jsonString = jObject.ToString();
            HttpContent httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(@"http://product-accounting.us-east-2.elasticbeanstalk.com/products/", httpContent);
            return true;
        }
    }
}
