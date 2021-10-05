using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalFirmaClone.Models.APIClasses
{
    public class attributes
    {
        public string file_name { get; set; }
        public string sign_status { get; set; }
        public DateTime signed_at { get; set; }
        public string signed_at_String { get; set; }
    }
}
