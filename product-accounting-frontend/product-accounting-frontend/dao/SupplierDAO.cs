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
    internal class SupplierDAO : IGenericDAO<Supplier>
    {
        public async Task<bool> executeDeleteQuery(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.DeleteAsync(@"http://product-accounting.us-east-2.elasticbeanstalk.com/suppliers/" + id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }

        public async Task<List<Supplier>> executeArchivedGetQuery()
        {
            List<Supplier> suppliers = new List<Supplier>();
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(@"http://product-accounting.us-east-2.elasticbeanstalk.com/suppliers/archived");
                string responseJSON = await response.Content.ReadAsStringAsync();
                suppliers = JsonConvert.DeserializeObject<List<Supplier>>(responseJSON);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return suppliers;
        }

        public async Task<List<Supplier>> executeGetQuery()
        {
            List<Supplier> suppliers = new List<Supplier>();
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(@"http://product-accounting.us-east-2.elasticbeanstalk.com/suppliers");
                string responseJSON = await response.Content.ReadAsStringAsync();
                suppliers = JsonConvert.DeserializeObject<List<Supplier>>(responseJSON);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return suppliers;
        }

        public async Task<bool> executePostQuery(Supplier supplier)
        {
            HttpClient client = new HttpClient();
            JObject jObject = JObject.FromObject(supplier);
            jObject["id"].Parent.Remove();
            jObject["archived"].Parent.Remove();
            string jsonString = jObject.ToString();
            HttpContent httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(@"http://product-accounting.us-east-2.elasticbeanstalk.com/suppliers/", httpContent);
            return true;
        }

        public async Task<bool> executePutQuery(int id, Supplier supplier)
        {
            HttpClient client = new HttpClient();
            JObject jObject = JObject.FromObject(supplier);
            jObject["id"].Parent.Remove();
            jObject["archived"].Parent.Remove();
            string jsonString = jObject.ToString();
            HttpContent httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(@"http://product-accounting.us-east-2.elasticbeanstalk.com/suppliers/" + id, httpContent);
            return true;
        }
    }
}
