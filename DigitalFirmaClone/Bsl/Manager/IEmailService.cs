using DigitalFirmaClone.Models.EmailConfigurationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalFirmaClone.Bsl.Manager
{
    public interface IEmailService
    {
        public bool SendEmail(EmailData emailData);
        public bool SendEmailWithAttachment(EmailDataWithAttachment emailData);
    }
}
