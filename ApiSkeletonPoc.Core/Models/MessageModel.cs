using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Models
{
    public class MessageModel
    {
        public int MessageId { get; set; }
        public string Text { get; set; }
        public string Receiver { get; set; }
        public string Sender { get; set; }
        public DateTime? SentDateTime { get; set; }
        public string ReferenceId { get; set; }
        public int? ContactId { get; set; }
        public decimal? Price { get; set; }
        public int? DeliveryStatusTypeId { get; set; }
        public string TextMagicMessageId { get; set; }
    }
}
