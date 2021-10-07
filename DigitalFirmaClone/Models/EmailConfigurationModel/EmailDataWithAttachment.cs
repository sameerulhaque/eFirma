using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalFirmaClone.Models.EmailConfigurationModel
{
    public class EmailDataWithAttachment : EmailData
    {
        public IFormFileCollection EmailAttachments { get; set; }
    }
}
