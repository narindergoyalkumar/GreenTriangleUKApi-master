using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EmailService
{
    public class EmailModel
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Host { get; set; }
        public string Password { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        public int Port { get; set; }
        public Stream Attachment { get; set; }
        public string ContentType { get; set; }
    }
}
