using System;
using Newtonsoft.Json;

namespace DigitalFirmaClone.Models.Objects
{
   public class CloseDocument
    {
       [JsonProperty("success")]
       public bool Success { get; set; }
    }
}
