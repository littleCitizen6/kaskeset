using Kaskeset.Common.Extensions;
using Kaskeset.Common.RequestInfo;
using Kaskeset.Common.Requests;
using Kaskeset.Common.ResponsesInfo;
using Kaskeset.Server.CommonInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kaskeset.Server.RequestHandeling.RequestHandlers
{
    public class GetRelatedChatGroupsHandler : IRequestHandler
    {
        private IStateInfo _stateInfo;
        public GetRelatedChatGroupsHandler(IStateInfo stateInfo)
        {
            _stateInfo = stateInfo;
        }
        public void Handle(Request request)
        {
            GetRelatedChatGroupInfo requestInfo = new GetRelatedChatGroupInfo();
            requestInfo.LoadFromDictionary(request.Properties);
            ResponseRelatedChatGroups responseInfo = new ResponseRelatedChatGroups();
            responseInfo.Chats = _stateInfo.Chats.ChatById.Values.Where(chat =>
              chat.Name != "private" && chat.Name != "global" && chat.Clients.Contains(_stateInfo.Pbx.Clients[requestInfo.ClientId]))
              .ToList().ConvertListToStrings();
            _stateInfo.Pbx.Clients[requestInfo.ClientId].Send(new Request(RequestType.GetAllClients, responseInfo.ToDictionary()));

        }
    }
}
