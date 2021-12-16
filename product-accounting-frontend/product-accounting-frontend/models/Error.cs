using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace product_accounting_frontend.models
{
    public class Error
    {
        [JsonProperty("status")]
        public string status { get; set; }
        [JsonProperty("error")]
        public string error { get; set; }
    }
}
