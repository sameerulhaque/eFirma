using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DigitalFirmaClone.EF
{
    public partial class view_inv_stock_management
    {
        public int product_id { get; set; }
        public string product_name { get; set; }
        public decimal? quantity { get; set; }
    }
}
