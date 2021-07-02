using ApiSkeletonPoc.Core.Common;
using ApiSkeletonPoc.Core.Insfrastucture;
using ApiSkeletonPoc.Core.Interfaces;
using ApiSkeletonPoc.Core.Models;
using ApiSkeletonPoc.Core.RequestModels;
using ApiSkeletonPoc.DAL.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApiSkeletonPoc.Core.Services
{
    public class MessageService : IMessageService
    {
        private readonly IBaseService<Message> _messageBaseService;
        public MessageService(IBaseService<Message> messageBaseService)
        {
            _messageBaseService = messageBaseService;
        }
        public int SendMessages(List<MessageModel> messageModels)
        {
            int sentMessagesCount = 0;
            IEnumerable<Message> messages = messageModels.Select(m => Mapper.MapMessageModelWithMessage(m));
            _messageBaseService.InsertBulk(messages);
            sentMessagesCount = messageModels.Count();
            return sentMessagesCount;
        }
        public Message GetMessageByReferenceIdAndReceiver(string referenceId, string receiver)
        {
            var message = _messageBaseService.Where(a => a.ReferenceId.Contains(referenceId) && a.Receiver == receiver).FirstOrDefault();
            return message;
        }

        public List<Message> GetDeliveredMessagesByReferenceId(string refId)
        {
            return _messageBaseService.Where(a => a.ReferenceId == refId && a.DeliveryStatusTypeId == Convert.ToInt32(Enums.DeliveryStatus.d)).ToList();
        }
        public void UpdateDeliveryStatus(Message message)
        {
            _messageBaseService.AddOrUpdate(message, message.MessageId);
        }
    }
}
