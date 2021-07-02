using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Models
{
    public class SendDocAsMailModel
    {
        public string PdfBase64String { get; set; }
        public string MailSubject { get; set; }
        public string Cc { get; set; }
        public string ReceiverName { get; set; }
        public string OwnerName { get; set; }
        public string ReceiverEmail { get; set; }
    }
}
