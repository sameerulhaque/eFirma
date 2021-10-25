using DigitalFirmaClone.Models.Payment;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalFirmaClone.Models.ModelClasses
{
    public class Signature
    {
        public Signature()
        {
            ViewersList = new List<Viewers>();
            SignersList = new List<Signers>();
        }
        public string FileName { get; set; }

        public List<Viewers> ViewersList { get; set; }
        public List<Signers> SignersList { get; set; }
        public IFormFile File { get; set; }
        public double Total { get; set; }

        public string DocumentName { get;  set; }
        public string DocumentString { get;  set; }
        public string Auth { get;  set; }
        public string PaypalId { get;  set; }
        public string CompanyId { get;  set; }
        public string SignOrdered { get;  set; }
        public string ModeLogo { get;  set; }
        public string RememberAt { get;  set; }
        public int? RememberEvery { get;  set; }
        public string SignMode { get;  set; }
        public string SignPosition { get;  set; }
        public string Tries { get;  set; }
        public int Id { get;  set; }
        public string SignStatus { get;  set; }
        public string CreateDateString { get;  set; }

        public PayModel PayModel { get; set; }
        public int UserId { get;  set; }
        public string MifielId { get;  set; }
        public string MessageForSigners { get;  set; }

        //public string DocumentString { get;  set; }
        //public string Auth { get;  set; }
        //public string PaypalId { get;  set; }
        //public string companyId { get;  set; }
        //public string signOrdered { get;  set; }
        //public string modeLogo { get;  set; }
        //public string rememberAt { get;  set; }
        //public int rememberEvery { get;  set; }
        //public string signMode { get;  set; }
        //public string signPosition { get;  set; }
        //public string tries { get;  set; }

    }
}
