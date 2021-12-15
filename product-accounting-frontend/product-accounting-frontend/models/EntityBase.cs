using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace product_accounting_frontend.models
{
    public abstract class EntityBase
    {
        [JsonProperty("id")]
        public int id { get; set; }
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

        public void setEditMode()
        {

        }

        public void setAddMode()
        {

        }

        public void setViewMode()
        {

        }
    }
}
