using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalFirmaClone.Models.Payment
{
    public class PayModel
    {
        public string CustomerName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string CardNumder { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public string CVC { get; set; }

        public int Amount { get; set; }
    }
}
