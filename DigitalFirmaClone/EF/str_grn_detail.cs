using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DigitalFirmaClone.EF
{
    public partial class str_grn_detail
    {
        public string import_grn_detail_id { get; set; }
        public int? created_by { get; set; }
        public int? modified_by { get; set; }
        public DateTime? created_date { get; set; }
        public DateTime? modified_date { get; set; }
        public int? is_active { get; set; }
        public int? is_approved { get; set; }
        public int? is_locked { get; set; }
        public string remarks { get; set; }
        public int? product_id { get; set; }
        public int? unit_of_measure_id { get; set; }
        public decimal? amount { get; set; }
        public decimal? quantity { get; set; }
        public int? currency_id { get; set; }
        public decimal? exchange_rate { get; set; }
        public int grn_detail_id { get; set; }
        public DateTime date { get; set; }

        public virtual ut_currency currency_ { get; set; }
        public virtual inv_product product_ { get; set; }
        public virtual inv_ut_unit_of_measure unit_of_measure_ { get; set; }
    }
}
