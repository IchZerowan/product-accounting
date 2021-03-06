using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows;

namespace product_accounting_frontend.models
{
    public class Delivery : EntityBase
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

        public Delivery(int id, string date, double shippingCost, Supplier supplier, List<DeliveryProduct> products, double total, bool completed)
        {
            this.id = id;
            this.date = date;
            this.shippingCost = shippingCost;
            this.supplier = supplier;
            this.products = products;
            this.total = total;
            this.completed = completed;
        }
        
        [JsonIgnore]
        public Visibility isEditSuppliersButtonsVisible { get; set; } = Visibility.Visible;
        [JsonIgnore]
        public Visibility isAddSuppliersFieldsVisible { get; set; } = Visibility.Collapsed;
        [JsonIgnore]
        public Visibility isEditConfirmationSuppliersButtonsVisible { get; set; } = Visibility.Collapsed;
        [JsonIgnore]
        public Visibility isAddSuppliersButtonsVisible { get; set; } = Visibility.Collapsed;
        [JsonIgnore]
        public Visibility isViewSuppliersFieldsVisible { get; set; } = Visibility.Visible;
        [JsonIgnore]
        public Visibility addProductButtonVisibility { get; set; } = Visibility.Visible;
    }
}
