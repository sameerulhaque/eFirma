using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalFirmaClone.Models.APIClasses
{
    public class GetSignature
    {
        public string type { get; set; }
        public string id { get; set; }
        //public object links { get; set; }
        //public object relationships { get; set; }
        public attributes attributes { get; set; }
    }
}
