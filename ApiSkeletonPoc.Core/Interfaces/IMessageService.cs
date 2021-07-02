using ApiSkeletonPoc.Core.Models;
using ApiSkeletonPoc.Core.RequestModels;
using ApiSkeletonPoc.DAL.DataContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Interfaces
{
    public interface IMessageService
    {
        int SendMessages(List<MessageModel> messageModels);
        Message GetMessageByReferenceIdAndReceiver(string referenceId, string receiver);
        void UpdateDeliveryStatus(Message message);

        List<Message> GetDeliveredMessagesByReferenceId(string refId);
    }
}
