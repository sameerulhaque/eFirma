using Newtonsoft.Json;

namespace DigitalFirmaClone.Models.Objects
{
    public class Data
    {
        [JsonProperty("document")]
        private Document Document { get; set; }
    }
}