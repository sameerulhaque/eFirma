using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DigitalFirmaClone.EF
{
    public partial class sig_signature_details
    {
        public int signature_details_id { get; set; }
        public int? is_deleted { get; set; }
        public int? created_by { get; set; }
        public int? modified_by { get; set; }
        public DateTime? created_date { get; set; }
        public DateTime? modified_date { get; set; }
        public int? is_active { get; set; }
        public int? is_approved { get; set; }
        public int? is_locked { get; set; }
        public string remarks { get; set; }
        public DateTime? approved_date { get; set; }
        public int? approved_by { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string rfc { get; set; }
        public int? signature_id { get; set; }

        public virtual sig_signature signature_ { get; set; }
    }
}
