using Newtonsoft.Json;
using System.Windows;

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
        [JsonIgnore]
        public int parentId { get; set; }

        public DeliveryProduct()
        {

        }

        public DeliveryProduct(Product product, int count, double price)
        {
            this.product = product;
            this.count = count;
            this.price = price;
        }

        [JsonIgnore]
        public Visibility productInfoVisibility { get; set; } = Visibility.Visible;
        [JsonIgnore]
        public Visibility productAddByIdVisibility { get; set; } = Visibility.Collapsed;
        [JsonIgnore]
        public Visibility confirmProductVisibility { get; set; } = Visibility.Collapsed;
        [JsonIgnore]
        public Visibility discardProductVisibility { get; set; } = Visibility.Collapsed;
        [JsonIgnore]
        public Visibility removeProductVisibility { get; set; } = Visibility.Visible;
        
    }
}
