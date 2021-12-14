using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace product_accounting_frontend.models
{
    public class Supplier : EntityBase
    {
        
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("phone")]
        public string phone { get; set; }
        [JsonProperty("archived")]
        public bool archived { get; set; }       

        Supplier()
        {

        }

        public Supplier(int id, string name, string phone, bool archived)
        {
            this.id = id;
            this.name = name;
            this.phone = phone;
            this.archived = archived;
        }
    }
}
