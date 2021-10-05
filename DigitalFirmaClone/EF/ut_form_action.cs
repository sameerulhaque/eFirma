using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DigitalFirmaClone.EF
{
    public partial class ut_form_action
    {
        public ut_form_action()
        {
            ut_user_role = new HashSet<ut_user_role>();
        }

        public int action_id { get; set; }
        public string import_action_id { get; set; }
        public string action_name { get; set; }
        public string display_name { get; set; }
        public int? is_active { get; set; }
        public int? is_approved { get; set; }
        public int? is_locked { get; set; }
        public string remarks { get; set; }
        public int form_code_id { get; set; }

        public virtual sdfghjk form_code_ { get; set; }
        public virtual ICollection<ut_user_role> ut_user_role { get; set; }
    }
}
