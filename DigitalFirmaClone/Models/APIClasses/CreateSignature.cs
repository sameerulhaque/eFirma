using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalFirmaClone.Models.APIClasses
{
    public class CreateSignature
    {
        public CreateSignature()
        {
            signatures = new List<signatures>();
            spectators = new List<spectators>();
        }
        public List<signatures> signatures { get; set; }
        public List<spectators> spectators { get; set; }
        public string document { get; set; }

        public int tries { get; set; }
        public string companyId { get; set; }
        public string signOrdered { get; set; }
        public string modeLogo { get; set; }
        public string rememberAt { get; set; }
        public int rememberEvery { get; set; }
        public string signMode { get; set; }
        public string signPosition { get; set; }
    }
}
