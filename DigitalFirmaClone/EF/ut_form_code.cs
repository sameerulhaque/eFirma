using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DigitalFirmaClone.EF
{
    public partial class ut_form_code
    {
        public ut_form_code()
        {
            ut_form_action = new HashSet<ut_form_action>();
            ut_form_layout = new HashSet<ut_form_layout>();
        }

        public int form_code_id { get; set; }
        public string import_form_code_id { get; set; }
        public string form_code_name { get; set; }
        public string form_code_action { get; set; }
        public string label_text { get; set; }
        public string view_role { get; set; }
        public string href { get; set; }
        public string view_module { get; set; }
        public string main_module { get; set; }
        public int? is_deleted { get; set; }
        public int? created_by { get; set; }
        public int? modified_by { get; set; }
        public DateTime? created_date { get; set; }
        public DateTime? modified_date { get; set; }
        public int? is_active { get; set; }
        public int? is_approved { get; set; }
        public int? is_locked { get; set; }
        public int language_id { get; set; }

        public virtual ICollection<ut_form_action> ut_form_action { get; set; }
        public virtual ICollection<ut_form_layout> ut_form_layout { get; set; }
    }
}
