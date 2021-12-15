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
    internal class CouponDAO : IGenericDAO<Coupon>
    {
        public async Task<bool> executeDeleteQuery(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.DeleteAsync(@"http://product-accounting.us-east-2.elasticbeanstalk.com/coupons/" + id);
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

        public async Task<List<Coupon>> executeArchivedGetQuery()
        {
            List<Coupon> coupons = new List<Coupon>();
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(@"http://product-accounting.us-east-2.elasticbeanstalk.com/coupons/archived");
                string responseJSON = await response.Content.ReadAsStringAsync();
                coupons = JsonConvert.DeserializeObject<List<Coupon>>(responseJSON);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return coupons;
        }

        public async Task<List<Coupon>> executeGetQuery()
        {
            List<Coupon> coupons = new List<Coupon>();
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(@"http://product-accounting.us-east-2.elasticbeanstalk.com/coupons");
                string responseJSON = await response.Content.ReadAsStringAsync();
                coupons = JsonConvert.DeserializeObject<List<Coupon>>(responseJSON);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return coupons;
        }

        public async Task<bool> executePostQuery(Coupon coupon)
        {
            HttpClient client = new HttpClient();
            JObject jObject = JObject.FromObject(coupon);
            jObject["id"].Parent.Remove();
            jObject["archived"].Parent.Remove();
            string jsonString = jObject.ToString();
            HttpContent httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(@"http://product-accounting.us-east-2.elasticbeanstalk.com/coupons/", httpContent);
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

        public async Task<bool> executePutQuery(int id, Coupon coupon)
        {
            HttpClient client = new HttpClient();
            JObject jObject = JObject.FromObject(coupon);
            jObject["id"].Parent.Remove();
            jObject["archived"].Parent.Remove();
            string jsonString = jObject.ToString();
            HttpContent httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(@"http://product-accounting.us-east-2.elasticbeanstalk.com/coupons/" + id + @"/add", httpContent);
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
