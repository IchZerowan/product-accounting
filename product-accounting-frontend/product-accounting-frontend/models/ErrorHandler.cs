using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace product_accounting_frontend.models
{
    public abstract class ErrorHandler
    {
        public async static Task<bool> handleEror(HttpResponseMessage response)
        {
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
