using Newtonsoft.Json;

namespace DigitalFirmaClone.Models.Objects
{
    public class Viewer
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}