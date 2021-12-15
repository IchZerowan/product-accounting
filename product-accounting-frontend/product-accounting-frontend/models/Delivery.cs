using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace product_accounting_frontend.models
{
    public class Delivery
    {
        [JsonProperty("date")]
        public string date { get; set; }
        [JsonProperty("shippingCost")]
        public double shippingCost { get; set; }
        [JsonProperty("supplier")]
        public Supplier supplier { get; set; }
        [JsonProperty("products")]
        public List<DeliveryProduct> products { get; set; }
        [JsonProperty("total")]
        public double total { get; set; }
        [JsonProperty("completed")]
        public bool completed { get; set; }

        public Delivery()
        {

        }

        public Delivery(string date, double shippingCost, Supplier supplier, List<DeliveryProduct> products, double total, bool completed)
        {
            this.date = date;
            this.shippingCost = shippingCost;
            this.supplier = supplier;
            this.products = products;
            this.total = total;
            this.completed = completed;
        }
    }
}
