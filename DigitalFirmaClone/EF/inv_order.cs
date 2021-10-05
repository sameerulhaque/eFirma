using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DigitalFirmaClone.EF
{
    public partial class inv_order
    {
        public inv_order()
        {
            str_gin_detail = new HashSet<str_gin_detail>();
        }

        public int order_id { get; set; }
        public string transaction_number { get; set; }
        public int? created_by { get; set; }
        public int? modified_by { get; set; }
        public DateTime? created_date { get; set; }
        public DateTime? modified_date { get; set; }
        public int? is_active { get; set; }
        public int? is_approved { get; set; }
        public int? is_locked { get; set; }
        public string remarks { get; set; }
        public int? company_id { get; set; }
        public string import_order_id { get; set; }
        public decimal? net_amount { get; set; }
        public DateTime? date { get; set; }
        public int? customer_id { get; set; }

        public virtual ut_company company_ { get; set; }
        public virtual inv_ut_party customer_ { get; set; }
        public virtual ICollection<str_gin_detail> str_gin_detail { get; set; }
    }
}
