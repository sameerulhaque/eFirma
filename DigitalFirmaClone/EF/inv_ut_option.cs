using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DigitalFirmaClone.EF
{
    public partial class inv_ut_option
    {
        public inv_ut_option()
        {
            inv_ut_variant = new HashSet<inv_ut_variant>();
        }

        public int option_id { get; set; }
        public string import_option_id { get; set; }
        public string option_name { get; set; }
        public int? created_by { get; set; }
        public int? modified_by { get; set; }
        public DateTime? created_date { get; set; }
        public DateTime? modified_date { get; set; }
        public int? is_active { get; set; }
        public int? is_approved { get; set; }
        public int? is_locked { get; set; }
        public string remarks { get; set; }
        public string short_code { get; set; }
        public int? company_id { get; set; }

        public virtual ut_company company_ { get; set; }
        public virtual ICollection<inv_ut_variant> inv_ut_variant { get; set; }
    }
}
