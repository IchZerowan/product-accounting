using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace product_accounting_frontend.models
{
    public class DeliveryProduct
    {
        [JsonProperty("product")]
        public Product product { get; set; }
        [JsonProperty("count")]
        public int count { get; set; }
        [JsonProperty("price")]
        public double price { get; set; }

        public DeliveryProduct()
        {

        }

        public DeliveryProduct(Product product, int count, double price)
        {
            this.product = product;
            this.count = count;
            this.price = price;
        }
    }
}
