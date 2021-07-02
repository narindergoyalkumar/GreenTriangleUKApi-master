using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;


namespace EmailService
{
    public class EmailService : IEmailService
    {
        public void SendEmail(EmailModel emailModel)
        {
            // create message
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(emailModel.From),
            };
            email.To.Add(MailboxAddress.Parse(emailModel.To));
            if (!string.IsNullOrEmpty(emailModel.Cc))
            {
                email.Cc.Add(MailboxAddress.Parse(emailModel.Cc));
            }
            email.Subject = emailModel.Subject;
            var mailBuilder = new BodyBuilder
            {
                HtmlBody = emailModel.Body
            };
            if (emailModel.Attachment != null)
            {
                mailBuilder.Attachments.Add("Signature.pdf", emailModel.Attachment, ContentType.Parse(emailModel.ContentType));
            }
            email.Body = mailBuilder.ToMessageBody();
            // send email
            using var smtp = new SmtpClient();
            smtp.Connect(emailModel.Host, emailModel.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(emailModel.From, emailModel.Password);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
