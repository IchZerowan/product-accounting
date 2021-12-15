using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace product_accounting_frontend.models
{
    public class Purchase : EntityBase
    {
        [JsonProperty("date")]
        public string date { get; set; }
            
        [JsonProperty("products")]
        public List<DeliveryProduct> products { get; set; }
        [JsonProperty("total")]
        public double total { get; set; }
        [JsonProperty("coupon")]
        public Coupon coupon { get; set; }
        [JsonProperty("completed")]
        public bool completed { get; set; }

        public Purchase()
        {

        }

        public Purchase(int id, string date, List<DeliveryProduct> deliveryProducts, Coupon coupon, bool completed)
        {
            this.id = id;
            this.date = date;
            this.products = deliveryProducts;
            this.coupon = coupon;
            this.completed = completed;
        }
        [JsonIgnore]
        public Visibility addProductButtonVisibility { get; set; } = Visibility.Visible;
    }
}
