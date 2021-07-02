using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApiSkeletonPoc.Core.Insfrastucture;
using ApiSkeletonPoc.Core.Interfaces;
using ApiSkeletonPoc.Core.Models;
using ApiSkeletonPoc.DAL.DataContracts;

namespace ApiSkeletonPoc.Core.Services
{
    public class ClientService : IClientService
    {
        private readonly IBaseService<Client> _clientBaseService;
        private readonly ILogService _logService;

        public ClientService(IBaseService<Client> clientBaseService, ILogService logService)
        {
            _clientBaseService = clientBaseService;
            _logService = logService;
        }

        public BaseResponseModel Add(ClientModel clientModel)
        {
            BaseResponseModel baseResponseModel = null;
            var createdClient = _clientBaseService.AddOrUpdate(Mapper.MapClientModelWithClient(clientModel), 0);

            if (createdClient != null)
            {
                int clientId = createdClient.ClientId;
                _logService.Add(new LogEntryModel { LoggedDateTime = DateTime.Now.ToString(), LogText = $"A new Client with Client Id {createdClient.ClientId} added" });
                baseResponseModel = new BaseResponseModel { IsSuccess = true, Response = clientId, Message = "Client added successfuly." };
            }

            return baseResponseModel;
        }

        public BaseResponseModel Update(int id, ClientModel clientModel)
        {
            int clientId = clientModel.ClientId;
            BaseResponseModel baseResponseModel = null;
            var clientEntity = _clientBaseService.GetById(id);
            if (clientEntity != null)
            {
                clientEntity.Name = clientModel.Name;
                clientEntity.RecordUpdatedDate = DateTime.Now;
                clientEntity.RemainingTextsCount += clientModel.RemainingTextsCount;
                clientEntity.TotalTextsCount = clientEntity.TotalTextsCount == 0 ? clientModel.RemainingTextsCount : clientEntity.TotalTextsCount;
                _clientBaseService.AddOrUpdate(clientEntity, id);
                _logService.Add(new LogEntryModel { LoggedDateTime = DateTime.Now.ToString(), LogText = $"A Client with Client Id {clientEntity.ClientId} updated" });
                baseResponseModel = new BaseResponseModel { IsSuccess = true, Response = clientId, Message = "Client updated successfuly." };
            }

            return baseResponseModel;
        }

        public BaseResponseModel Remove(int clintId)
        {
            BaseResponseModel baseResponseModel = null;
            var client = _clientBaseService.GetById(clintId);
            if (client != null)
            {
                _clientBaseService.Remove(client.ClientId);
                _logService.Add(new LogEntryModel
                {
                    LoggedDateTime = DateTime.Now.ToString(),
                    LogText = $"A new Client with Id {client.ClientId} deleted"
                });
                baseResponseModel = new BaseResponseModel { IsSuccess = true, Message = "Client deleted successfuly.", Response = true };
            }

            return baseResponseModel;
        }

        public bool IsClientExists(int clientId)
        {
            if (_clientBaseService.GetById(clientId) != null)
            {
                return true;
            }
            return false;
        }

        public void UpdateRemainingTexts(int count, int clientId)
        {
            var client = _clientBaseService.GetById(clientId);
            if (client != null)
            {
                client.RemainingTextsCount -= count;
                client.RecordUpdatedDate = DateTime.Now;
                _clientBaseService.AddOrUpdate(client, clientId);
            }
        }
        public void UpdateRemainingTextsWithDeleiveredMessage(int count, int clientId)
        {
            var client = _clientBaseService.GetById(clientId);
            if (client != null)
            {
                client.RemainingTextsCount += count;
                client.RecordUpdatedDate = DateTime.Now;
                _clientBaseService.AddOrUpdate(client, clientId);
            }
        }

        public BaseResponseModel Get(int id)
        {
            return new BaseResponseModel { IsSuccess = true, Message = "", Response = Mapper.MapClientWithClientModel(_clientBaseService.GetById(id)) };
        }

        public BaseResponseModel GetAll()
        {
            return new BaseResponseModel { IsSuccess = true, Message = "", Response = _clientBaseService.GetAll().Select(c => Mapper.MapClientWithClientModel(c)).ToList() };
        }
    }
}
