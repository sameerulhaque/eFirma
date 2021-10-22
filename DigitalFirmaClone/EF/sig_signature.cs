using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DigitalFirmaClone.EF
{
    public partial class sig_signature
    {
        public sig_signature()
        {
            sig_signature_details = new HashSet<sig_signature_details>();
            sig_spectators_details = new HashSet<sig_spectators_details>();
        }

        public int signature_id { get; set; }
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
        public string document_name { get; set; }
        public int user_id { get; set; }
        public string mifiel_id { get; set; }

        public virtual ICollection<sig_signature_details> sig_signature_details { get; set; }
        public virtual ICollection<sig_spectators_details> sig_spectators_details { get; set; }
    }
}
