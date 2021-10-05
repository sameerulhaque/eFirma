using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DigitalFirmaClone.EF
{
    public partial class inv_product_variant
    {
        public inv_product_variant()
        {
            inv_product = new HashSet<inv_product>();
        }

        public int product_variant_id { get; set; }
        public string import_product_variant_id { get; set; }
        public int? created_by { get; set; }
        public int? modified_by { get; set; }
        public DateTime? created_date { get; set; }
        public DateTime? modified_date { get; set; }
        public int? is_active { get; set; }
        public int? is_approved { get; set; }
        public int? is_locked { get; set; }
        public string remarks { get; set; }
        public int? product_id { get; set; }
        public string product_variant_name { get; set; }

        public virtual inv_product product_ { get; set; }
        public virtual ICollection<inv_product> inv_product { get; set; }
    }
}
