using Kaskeset.Common.RequestInfo;
using Kaskeset.Common.Requests;
using Kaskeset.Server.CommonInfo;

namespace Kaskeset.Server.RequestHandeling.RequestHandlers
{
    public class MessageHandler : IRequestHandler
    {
        private IStateInfo _stateInfo;
        public MessageHandler(IStateInfo stateInfo)
        {
            _stateInfo = stateInfo;
        }
        public void Handle(Request request)
        {
            MessageInfo message = new MessageInfo();
            message.LoadFromDictionary(request.Properties);
            if (message.Value != "exit")
            {
                _stateInfo.Chats.ChatById[message.ChatId].Write(message.Value, _stateInfo.Pbx.Clients[message.ClientId]);
            }
            else
            {
                _stateInfo.Pbx.Clients[message.ClientId].Send(message.Value); // send back the client exit
            }
        }
    }
}
