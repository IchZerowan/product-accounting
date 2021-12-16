using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using product_accounting_frontend.models;

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
            if (!response.IsSuccessStatusCode)
            {
                string responseMessage = await response.Content.ReadAsStringAsync();
                Error error = JsonConvert.DeserializeObject<Error>(responseMessage);
                MessageBox.Show(error.error, error.status);
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<bool> executeDeleteQuery(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.DeleteAsync(@"http://product-accounting.us-east-2.elasticbeanstalk.com/products/" + id);
                if (!response.IsSuccessStatusCode)
                {
                    string responseMessage = await response.Content.ReadAsStringAsync();
                    Error error = JsonConvert.DeserializeObject<Error>(responseMessage);
                    MessageBox.Show(error.error, error.status);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }            
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

        public async Task<List<Product>> executeArchivedGetQuery()
        {
            List<Product> products = new List<Product>();
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(@"http://product-accounting.us-east-2.elasticbeanstalk.com/products/archived");
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
            if (!response.IsSuccessStatusCode)
            {
                string responseMessage = await response.Content.ReadAsStringAsync();
                Error error = JsonConvert.DeserializeObject<Error>(responseMessage);
                MessageBox.Show(error.error, error.status);
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
