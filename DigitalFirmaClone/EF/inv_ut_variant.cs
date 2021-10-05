using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DigitalFirmaClone.EF
{
    public partial class inv_ut_variant
    {
        public inv_ut_variant()
        {
            inv_product_variant_details = new HashSet<inv_product_variant_details>();
        }

        public int variant_id { get; set; }
        public string import_variant_id { get; set; }
        public string variant_name { get; set; }
        public int? created_by { get; set; }
        public int? modified_by { get; set; }
        public DateTime? created_date { get; set; }
        public DateTime? modified_date { get; set; }
        public int? is_active { get; set; }
        public int? is_approved { get; set; }
        public int? is_locked { get; set; }
        public string remarks { get; set; }
        public string short_code { get; set; }
        public int? option_id { get; set; }
        public int? company_id { get; set; }

        public virtual ut_company company_ { get; set; }
        public virtual inv_ut_option option_ { get; set; }
        public virtual ICollection<inv_product_variant_details> inv_product_variant_details { get; set; }
    }
}
