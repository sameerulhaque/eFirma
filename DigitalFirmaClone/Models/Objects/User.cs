using Newtonsoft.Json;

namespace DigitalFirmaClone.Models.Objects
{
    public class User
    {
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
