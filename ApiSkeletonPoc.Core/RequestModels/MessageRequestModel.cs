using ApiSkeletonPoc.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.RequestModels
{
    public class MessageRequestModel
    {
        public string TextToSend { get; set; }
        public List<MessageModel> MessageModels { get; set; }
        //public int ClientId { get; set; }
    }
}
