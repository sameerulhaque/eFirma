using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DigitalFirmaClone.EF
{
    public partial class ut_currency
    {
        public ut_currency()
        {
            str_gin_detail = new HashSet<str_gin_detail>();
            str_grn_detail = new HashSet<str_grn_detail>();
        }

        public int currency_id { get; set; }
        public string import_currency_id { get; set; }
        public string currency_name { get; set; }
        public int? created_by { get; set; }
        public int? modified_by { get; set; }
        public DateTime? created_date { get; set; }
        public DateTime? modified_date { get; set; }
        public decimal? exchange_rate { get; set; }
        public int? is_active { get; set; }
        public int? is_approved { get; set; }
        public int? is_locked { get; set; }
        public string remarks { get; set; }

        public virtual ICollection<str_gin_detail> str_gin_detail { get; set; }
        public virtual ICollection<str_grn_detail> str_grn_detail { get; set; }
    }
}
