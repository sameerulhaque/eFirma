using Newtonsoft.Json;

namespace DigitalFirmaClone.Models.Objects
{
    public class SignatureResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("data")]
        public Data Data { get; set; }
    }
}
