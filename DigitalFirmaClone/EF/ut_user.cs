using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DigitalFirmaClone.EF
{
    public partial class ut_user
    {
        public ut_user()
        {
            ut_user_role = new HashSet<ut_user_role>();
        }

        public int user_id { get; set; }
        public string import_user_id { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public string show_password { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public int? is_deleted { get; set; }
        public int? created_by { get; set; }
        public int? modified_by { get; set; }
        public DateTime? created_date { get; set; }
        public DateTime? modified_date { get; set; }
        public int? is_active { get; set; }
        public int? is_approved { get; set; }
        public int? is_locked { get; set; }
        public string remarks { get; set; }

        public virtual ICollection<ut_user_role> ut_user_role { get; set; }
    }
}
