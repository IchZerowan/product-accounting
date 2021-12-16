using Newtonsoft.Json;

namespace product_accounting_frontend.models
{
    public class Coupon : EntityBase
    {
        [JsonProperty("code")]
        public string code { get; set; }
        [JsonProperty("amount")]
        public double amount { get; set; }
        [JsonProperty("count")]
        public int count { get; set; }
        [JsonProperty("archived")]
        public bool archived { get; set; }

        public Coupon()
        {

        }

        public Coupon(int id, string code, double amount, int count, bool archived)
        {
            this.id = id;
            this.code = code;   
            this.amount = amount;
            this.count = count;
            this.archived = archived;
        }
    }
}
