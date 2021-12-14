using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace product_accounting_frontend.models
{
    public class Supplier
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("phone")]
        public string phone { get; set; }
        [JsonProperty("archived")]
        public bool archived { get; set; }
        [JsonIgnore]
        public Visibility isEditingButtonsVisible { get; set; } = Visibility.Collapsed;
        [JsonIgnore]
        public Visibility isViewButtonsVisible { get; set; } = Visibility.Visible;
        [JsonIgnore]
        public Visibility isAddButtonsVisible { get; set; } = Visibility.Collapsed;
        [JsonIgnore]
        public Visibility isEditingFieldsVisible { get; set; } = Visibility.Collapsed;
        [JsonIgnore]
        public Visibility isViewFieldsVisible { get; set; } = Visibility.Visible;
        [JsonIgnore]
        public Visibility isAddFieldsVisible { get; set; } = Visibility.Collapsed;

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
