using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using ApiSkeletonPoc.Core.Common;
using ApiSkeletonPoc.Core.Interfaces;
using ApiSkeletonPoc.Core.Models;
using ApiSkeletonPoc.Core.RequestModels;
using ApiSkeletonPoc.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TextMagic.Service;
using TextMagicClient.Model;

namespace ApiSkeletonPoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MessageController : ControllerBase
    {
        private readonly ILogger<MessageController> _logger;
        private readonly ITextMagicClient _textMagicClient;
        private readonly IMessageService _messageService;
        private readonly IClientService _clientService;
        private readonly IConfiguration _configuration;
        public MessageController(ILogger<MessageController> logger, ITextMagicClient textMagicClient, IMessageService messageService, IClientService clientService,IConfiguration configuration)
        {
            _logger = logger;
            _textMagicClient = textMagicClient;
            _messageService = messageService;
            _clientService = clientService;
            _configuration = configuration;
        }
        [AllowAnonymous]
        [HttpPost("DeliveryCallBack")]
        public IActionResult DeliveryStatus([FromBody]DeliveryCallbackModel deliveryCallbackModel)
        {
            _logger.LogInformation($"Payload recieved :{JsonConvert.SerializeObject(deliveryCallbackModel)}");
            var message = _messageService.GetMessageByReferenceIdAndReceiver(deliveryCallbackModel.referenceId, deliveryCallbackModel.receiver);
            if (message != null)
            {
                _logger.LogInformation($"message found :{JsonConvert.SerializeObject(message)}");
                if (message.DeliveryStatusTypeId != Convert.ToInt32(Enums.DeliveryStatus.d) && message.DeliveryStatusTypeId != Convert.ToInt32(Enums.DeliveryStatus.e) && message.DeliveryStatusTypeId != Convert.ToInt32(Enums.DeliveryStatus.f) && message.DeliveryStatusTypeId != Convert.ToInt32(Enums.DeliveryStatus.j) && message.DeliveryStatusTypeId != Convert.ToInt32(Enums.DeliveryStatus.u))
                {
                    message.DeliveryStatusTypeId = Constants.DeliveryStatus[deliveryCallbackModel.status];
                    message.Price = Convert.ToDecimal(deliveryCallbackModel.price);
                    message.Receiver = deliveryCallbackModel.receiver;
                    message.DeliveredTime = deliveryCallbackModel.status == Convert.ToString(Enums.DeliveryStatus.d) ? DateTime.Now : (DateTime?)null;
                    _messageService.UpdateDeliveryStatus(message);
                }
                int clientId = Convert.ToInt32(message.ReferenceId.Split('_')[1]);
                int deliveredMessagesCount = _messageService.GetDeliveredMessagesByReferenceId(message.ReferenceId).Count;
                _clientService.UpdateRemainingTexts(deliveredMessagesCount, clientId);

            }
            return Ok();
        }
        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] MessageRequestModel messageRequestModel)
        {
            var client = (ClientModel)_clientService.Get(Convert.ToInt32(User.GetClientId())).Response;
            int currentTextRemainingCount = client.RemainingTextsCount;
            if (currentTextRemainingCount <= 0)
            {
                return BadRequest(new BaseResponseModel { IsSuccess = false, Message = "You don't have enough messages remaining. Please purchase more texts.", Response = null });
            }
            if (currentTextRemainingCount < messageRequestModel.MessageModels.Count())
            {
                return BadRequest(new BaseResponseModel { IsSuccess = false, Message = "Your remaining messages count is not enough to send the messages to list.", Response = null });
            }
            var formattedMessages = messageRequestModel.MessageModels.Select(a => a.Receiver.Replace(" ", "")).ToList();
            string messages = string.Join(",", formattedMessages.Where(a=>a.StartsWith(_configuration["CountryCode"])).ToList());
            if (!string.IsNullOrEmpty(messages))
            {
                //"hello tejinder", "447860021131,447860021130"`
                var response = await _textMagicClient.SendMessage(messageRequestModel.TextToSend, messages);
                messageRequestModel.MessageModels.ForEach(m => { m.Text = messageRequestModel.TextToSend; m.TextMagicMessageId = response.Item2.Id.Value.ToString(); m.ReferenceId = response.Item1.ToString() + "_" + Convert.ToString(User.GetClientId()); m.Sender = "447860021262"; });
                if (response != null)
                {
                    int sentMessagesCount = _messageService.SendMessages(messageRequestModel.MessageModels);
                    //_clientService.UpdateRemainingTexts(sentMessagesCount, Convert.ToInt32(User.GetClientId()));
                }
                return Ok(response);
            }
            return BadRequest();
        }
    }
}
