using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DigitalFirmaClone.EF
{
    public partial class inv_ut_party
    {
        public inv_ut_party()
        {
            inv_order = new HashSet<inv_order>();
        }

        public int party_id { get; set; }
        public string import_party_id { get; set; }
        public string name { get; set; }
        public int? created_by { get; set; }
        public int? modified_by { get; set; }
        public DateTime? created_date { get; set; }
        public DateTime? modified_date { get; set; }
        public int? is_active { get; set; }
        public int? is_approved { get; set; }
        public int? is_locked { get; set; }
        public string remarks { get; set; }
        public string short_name { get; set; }
        public string mobile { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string fax { get; set; }
        public string website { get; set; }
        public string cnic { get; set; }
        public string contact_person { get; set; }
        public int? company_id { get; set; }

        public virtual ut_company company_ { get; set; }
        public virtual ICollection<inv_order> inv_order { get; set; }
    }
}
