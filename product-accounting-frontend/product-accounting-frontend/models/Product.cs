using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using product_accounting_frontend.models;

namespace product_accounting_frontend
{
    public class Product : EntityBase
    {
       
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("description")]
        public string description { get; set; }
        [JsonProperty("priceRetail")]
        public float priceRetail { get; set; }
        [JsonProperty("priceWholesale")]
        public float priceWholesale { get; set; }
        [JsonProperty("archived")]
        private bool archived { get; set; }
        [JsonProperty("count")]
        public int count { get; set; }
        
        Product()
        {

        }
        public Product(int id, string name, string description, float priceRetail, float priceWholesale, bool archived, int count)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.priceRetail = priceRetail;
            this.priceWholesale = priceWholesale;
            this.count = count;
            this.archived = archived;
        }

        public int getId()
        {
            return id;
        }
    }
}
