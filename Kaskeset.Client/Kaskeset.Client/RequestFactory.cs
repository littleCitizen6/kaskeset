using Kaskeset.Common.RequestInfo;
using Kaskeset.Common.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kaskeset.Client
{
    public class RequestFactory
    {
        private ClientInfo _info;
        public RequestFactory(ClientInfo clientInfo)
        {
            _info = clientInfo;
        }
        public Request CreateUpdateNameRequest(string name)
        {
            UpdateNameInfo updateName = new UpdateNameInfo();
            updateName.ClientId = _info.ClientId;
            updateName.Name = name;
            return new Request(RequestType.UpdateName, updateName.ToDictionary());
        }
        public Request CreateMessageRequest(string value, int ChatId)
        {
            MessageInfo message = new MessageInfo();
            message.ClientId = _info.ClientId;
            message.ChatId = ChatId;
            message.Value = value;
            return new Request(RequestType.TextMessage, message.ToDictionary());
        }
        public Request CreateChatConnectionRequest(int chatId, bool toConnect)
        {
            ChatConnectionInfo chatConnection = new ChatConnectionInfo();
            chatConnection.ClientId = _info.ClientId;
            chatConnection.ChatId = chatId;
            chatConnection.ToConnect = toConnect;
            return new Request(RequestType.ChatConnection, chatConnection.ToDictionary());
        }
        public Request CreateCreateChatRequest(string name, List<Guid> participentsId)
        {
            CreateChatInfo createChatInfo = new CreateChatInfo();
            createChatInfo.ClientId = _info.ClientId;
            createChatInfo.Name = name;
            createChatInfo.ParticipentsId = participentsId;
            return new Request(RequestType.CreateChat, createChatInfo.ToDictionary());
        }
        public Request CreateGetAllClientsRequest()
        {
            GetAllClientsInfo getAllClientsInfo = new GetAllClientsInfo();
            getAllClientsInfo.ClientId = _info.ClientId;
            return new Request(RequestType.GetAllClients, getAllClientsInfo.ToDictionary());
        }
        public Request CreateGetRealatedChatGroupsRequest()
        {
            GetRelatedChatGroupInfo getRelatedChatGroupInfo = new GetRelatedChatGroupInfo();
            getRelatedChatGroupInfo.ClientId = _info.ClientId;
            return new Request(RequestType.GetRelatedChatGroups, getRelatedChatGroupInfo.ToDictionary());
        }
    }
}
