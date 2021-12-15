using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace product_accounting_frontend.models
{
    public class Transaction
    {
        [JsonProperty("type")]
        public string type { get; set; }
        [JsonProperty("relId")]
        public int relId { get; set; }
        [JsonProperty("amount")]
        public double amount { get; set; }
        [JsonProperty("date")]
        public string date { get; set; }

        public Transaction()
        {

        }

        public Transaction(string type, int relId, double amount, string date)
        {
            this.type = type;
            this.relId = relId;
            this.amount = amount;
            this.date = date;
        }
    }
}
