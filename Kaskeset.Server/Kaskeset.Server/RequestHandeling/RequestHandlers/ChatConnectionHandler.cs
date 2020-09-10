using Kaskeset.Common.RequestInfo;
using Kaskeset.Common.ResponsesInfo;
using Kaskeset.Common.Requests;
using Kaskeset.Server.CommonInfo;

namespace Kaskeset.Server.RequestHandeling.RequestHandlers
{
    public class ChatConnectionHandler:IRequestHandler
    {
        private IStateInfo _stateInfo;
        public ChatConnectionHandler(IStateInfo stateInfo)
        {
            _stateInfo = stateInfo;
        }

        public void Handle(Request request)
        {
            ChatConnectionInfo requestInfo = new ChatConnectionInfo();
            requestInfo.LoadFromDictionary(request.Properties);

            if (requestInfo.ToConnect)
            {
                ResponseHistoryInfo historyInfo = new ResponseHistoryInfo();
                historyInfo.Messages = _stateInfo.Chats.ChatById[requestInfo.ChatId].History;
                _stateInfo.Pbx.Clients[requestInfo.ClientId].Send(new Request(RequestType.TextMessage, historyInfo.ToDictionary()));
                _stateInfo.Chats.ChatById[requestInfo.ChatId]
                    .Connect(_stateInfo.Pbx.Clients[requestInfo.ClientId]);
            }
            else
            {
                _stateInfo.Chats.ChatById[requestInfo.ChatId]
                    .Disconnect(_stateInfo.Pbx.Clients[requestInfo.ClientId]);
            }
        }
    }
}
