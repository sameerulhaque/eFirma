using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalFirmaClone.Models.APIClasses
{
    public class attributes
    {
        public string original_hash { get; set; }
        public string file_name { get; set; }
        public string signStatus { get; set; }
        public int sign_status { get; set; }
        public bool signed { get; set; }
        public string signature_mode { get; set; }
        public string signature_ordered { get; set; }
        public string owner { get; set; }
        public bool isOwner { get; set; }
        public string rejected_message { get; set; }
        public int tries { get; set; }
        //public object remember_at { get; set; }
        public int remember_every { get; set; }
        public string signed_at { get; set; }
        public string created_at { get; set; }
        public string signStatusColor { get; internal set; }
    }
}
