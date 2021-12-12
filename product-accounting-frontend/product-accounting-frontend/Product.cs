using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace product_accounting_frontend
{
    internal class Product
    {
        private int id;
        private string name;
        private string description;
        private float priceRetail;
        private float priceWholesale;
        private int count;
        private bool archived;

        Product()
        {

        }
        Product(int id, string name, string description, float priceRetail, float priceWholesale, int count, bool archived)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.priceRetail = priceRetail;
            this.priceWholesale = priceWholesale;
            this.count = count;
            this.archived = archived;
        }
    }
}
