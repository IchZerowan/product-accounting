using Newtonsoft.Json;

namespace product_accounting_frontend.models
{
    public class Error
    {
        [JsonProperty("status")]
        public string status { get; set; }
        [JsonProperty("error")]
        public string error { get; set; }
    }
}
