using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    internal class DeliveryDAO : IGenericDAO<Delivery>
    {
        public async Task<bool> executeDeleteQuery(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.DeleteAsync(@"http://product-accounting.us-east-2.elasticbeanstalk.com/deliveries/" + id);
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(response.StatusCode.ToString());
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

        public async Task<bool> executeDeleteProductQuery(int id, int productId)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.DeleteAsync(@"http://product-accounting.us-east-2.elasticbeanstalk.com/deliveries/" + id + @"/products/" + productId);
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(response.StatusCode.ToString());
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

        public async Task<List<Delivery>> executeGetQuery()
        {
            List<Delivery> deliveries = new List<Delivery>();
            List<Delivery> notCommitedDeliveries = new List<Delivery>();
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(@"http://product-accounting.us-east-2.elasticbeanstalk.com/deliveries");
                string responseJSON = await response.Content.ReadAsStringAsync();
                deliveries = JsonConvert.DeserializeObject<List<Delivery>>(responseJSON);
                
                foreach (Delivery delivery in deliveries)
                {
                    if(!delivery.completed)
                    {
                        notCommitedDeliveries.Add(delivery);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return notCommitedDeliveries;
        }

        public async Task<List<Delivery>> executeCommitedGetQuery()
        {
            List<Delivery> deliveries = new List<Delivery>();
            List<Delivery> CommitedDeliveries = new List<Delivery>();
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(@"http://product-accounting.us-east-2.elasticbeanstalk.com/deliveries");
                string responseJSON = await response.Content.ReadAsStringAsync();
                deliveries = JsonConvert.DeserializeObject<List<Delivery>>(responseJSON);

                foreach (Delivery delivery in deliveries)
                {
                    if (delivery.completed)
                    {
                        CommitedDeliveries.Add(delivery);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return CommitedDeliveries;
        }


        public async Task<bool> executePostQuery(Delivery delivery)
        {
            HttpClient client = new HttpClient();
            JObject jObject = JObject.FromObject(delivery);
            jObject["id"].Parent.Remove();
            jObject["completed"].Parent.Remove();
            string jsonString = "{"
                + "\"date\":\""
                + delivery.date
                + "\", \"shippingCost\":"
                + delivery.shippingCost
                + ", \"supplierId\":"
                + delivery.supplier.id
                + "}";
            HttpContent httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(@"http://product-accounting.us-east-2.elasticbeanstalk.com/deliveries", httpContent);
            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show(response.StatusCode.ToString());
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<bool> executePutQuery(int id, Delivery delivery)
        {
            HttpClient client = new HttpClient();
            string jsonString = "{"
                + "\"date\":\""
                + delivery.date
                + "\", \"shippingCost\":"
                + delivery.shippingCost
                + ", \"supplierId\":"
                + delivery.supplier.id
                + "}";
            HttpContent httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(@"http://product-accounting.us-east-2.elasticbeanstalk.com/deliveries/" + id, httpContent);
            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show(response.StatusCode.ToString());
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<bool> executePutCommit(int id)
        {
            HttpClient client = new HttpClient();            
            
            HttpResponseMessage response = await client.PutAsync(@"http://product-accounting.us-east-2.elasticbeanstalk.com/deliveries/" + id + @"/commit", null);
            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show(response.StatusCode.ToString());
                return false;
            }
            else
            {
                return true;
            }
        }



        public async Task<bool> executePostProductQuery(int id, Delivery delivery)
        {
            HttpClient client = new HttpClient();
            string jsonString = "{"
                + "\"productId\":"
                + delivery.products[delivery.products.Count - 1].product.id
                + ", \"count\":"
                + delivery.products[delivery.products.Count - 1].count
                + "}";
            HttpContent httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(@"http://product-accounting.us-east-2.elasticbeanstalk.com/deliveries/" + delivery.id + @"/products", httpContent);
            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show(response.StatusCode.ToString());
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
