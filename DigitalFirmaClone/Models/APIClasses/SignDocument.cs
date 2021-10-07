using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalFirmaClone.Models.APIClasses
{
    public class SignDocument
    {
        public string key { get; set; }
        public string cer { get; set; }
        public string password { get; set; }
        public bool reject { get; set; }
        public string commentary { get; set; }
        public string SignatureId { get; set; }
    }
}
