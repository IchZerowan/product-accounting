using Newtonsoft.Json;
using product_accounting_frontend.models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace product_accounting_frontend.dao
{
    internal class PurchaseDAO : IGenericDAO<Purchase>
    {
        public async Task<bool> executeDeleteQuery(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.DeleteAsync(@"http://product-accounting.us-east-2.elasticbeanstalk.com/purchases/" + id);
                return await ErrorHandler.handleEror(response);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public async Task<List<Purchase>> executeGetQuery()
        {
             List<Purchase> purchases = new List<Purchase>();
            List<Purchase> notCommitedPurchases = new List<Purchase>();
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(@"http://product-accounting.us-east-2.elasticbeanstalk.com/purchases");
                string responseJSON = await response.Content.ReadAsStringAsync();
                purchases = JsonConvert.DeserializeObject<List<Purchase>>(responseJSON);
                
                foreach (Purchase purchase in purchases)
                {
                    if(!purchase.completed)
                    {
                        if(purchase.coupon == null)
                        {
                            purchase.coupon = new Coupon(0,"",0,0,false);
                        }
                        notCommitedPurchases.Add(purchase);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return notCommitedPurchases;
        }

        public async Task<bool> executePostQuery(Purchase purchase)
        {

            HttpClient client = new HttpClient();
            
            string jsonString = "{"
                + "\"date\":\""
                + purchase.date
                + "\", \"couponCode\":\""
                + purchase.coupon.code
                + "\"}";
            HttpContent httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(@"http://product-accounting.us-east-2.elasticbeanstalk.com/purchases", httpContent);
            return await ErrorHandler.handleEror(response);
        }

        public async Task<bool> executePutQuery(int id, Purchase purchase)
        {
            HttpClient client = new HttpClient();
            string jsonString = "{"
                + "\"date\":\""
                + purchase.date
                + "\", \"couponCode\":\""
                + purchase.coupon.code               
                + "\"}";
            HttpContent httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(@"http://product-accounting.us-east-2.elasticbeanstalk.com/purchases/" + id, httpContent);
            return await ErrorHandler.handleEror(response);
        }

        public async Task<bool> executePostProductQuery(int id, Purchase purchase)
        {
            HttpClient client = new HttpClient();
            string jsonString = "{"
                + "\"productId\":"
                + purchase.products[purchase.products.Count - 1].product.id
                + ", \"count\":"
                + purchase.products[purchase.products.Count - 1].count
                + "}";
            HttpContent httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(@"http://product-accounting.us-east-2.elasticbeanstalk.com/purchases/" + purchase.id + @"/products", httpContent);
            return await ErrorHandler.handleEror(response);
        }

        public async Task<bool> executePutCommit(int id)
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.PutAsync(@"http://product-accounting.us-east-2.elasticbeanstalk.com/purchases/" + id + @"/commit", null);
            return await ErrorHandler.handleEror(response);
        }

        public async Task<bool> executeDeleteProductQuery(int id, int productId)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.DeleteAsync(@"http://product-accounting.us-east-2.elasticbeanstalk.com/purchases/" + id + @"/products/" + productId);
                return await ErrorHandler.handleEror(response);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }            
        }

        public async Task<List<Purchase>> executeCommitedGetQuery()
        {
            List<Purchase> purchases = new List<Purchase>();
            List<Purchase> notCommitedPurchases = new List<Purchase>();
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(@"http://product-accounting.us-east-2.elasticbeanstalk.com/purchases");
                string responseJSON = await response.Content.ReadAsStringAsync();
                purchases = JsonConvert.DeserializeObject<List<Purchase>>(responseJSON);

                foreach (Purchase purchase in purchases)
                {
                    if (purchase.completed)
                    {
                        if (purchase.coupon == null)
                        {
                            purchase.coupon = new Coupon(0, "", 0, 0, false);
                        }
                        notCommitedPurchases.Add(purchase);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return notCommitedPurchases;
        }
    }
}
