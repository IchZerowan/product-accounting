using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace product_accounting_frontend
{
    public class Product
    {
        [JsonProperty("id")]
        public int id { get; set; }
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
        [JsonIgnore]
        public Visibility isEditingVisible { get; set; } = Visibility.Collapsed;
        [JsonIgnore]
        public Visibility isViewVisible { get; set; } = Visibility.Visible;
        [JsonIgnore]
        public Visibility isAddVisible { get; set; } = Visibility.Collapsed;
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
