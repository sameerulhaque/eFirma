using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DigitalFirmaClone.EF
{
    public partial class inv_ut_unit_of_measure
    {
        public inv_ut_unit_of_measure()
        {
            inv_product = new HashSet<inv_product>();
        }

        public int unit_of_measure_id { get; set; }
        public string unit_of_measure_name { get; set; }
        public int? is_deleted { get; set; }
        public int? created_by { get; set; }
        public int? modified_by { get; set; }
        public DateTime? created_date { get; set; }
        public DateTime? modified_date { get; set; }
        public int? is_active { get; set; }
        public int? is_approved { get; set; }
        public int? is_locked { get; set; }
        public string remarks { get; set; }
        public string import_unit_of_measure_id { get; set; }

        public virtual ICollection<inv_product> inv_product { get; set; }
    }
}
