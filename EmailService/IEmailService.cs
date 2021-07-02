using System;
using System.Collections.Generic;
using System.Text;

namespace EmailService
{
    public interface IEmailService
    {
        void SendEmail(EmailModel emailModel);
    }
}
