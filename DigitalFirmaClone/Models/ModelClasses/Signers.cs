using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalFirmaClone.Models.ModelClasses
{
    public class Signers
    {
        public string SignatureName { get; set; }
        public string SignatureEmail { get; set; }
        public string RFC { get; internal set; }
    }
}
