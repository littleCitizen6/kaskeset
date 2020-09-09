using Kaskeset.Common.RequestInfo;
using Kaskeset.Common.Requests;
using Kaskeset.Server.CommonInfo;
using System.Linq;

namespace Kaskeset.Server.RequestHandeling.RequestHandlers
{
    public class GetAllClientsHandler : IRequestHandler
    {
        private object _locker;
        private IStateInfo _stateInfo;
        public GetAllClientsHandler(IStateInfo stateInfo)
        {
            _locker = new object();
            _stateInfo = stateInfo;
        }
        public void Handle(Request request)
        {
            GetAllClientsInfo requestInfo = new GetAllClientsInfo();
            requestInfo.LoadFromDictionary(request.Properties);
            lock (_locker) // because client can be add, and if he will be added at the middle, them the read count wont be true 
            {
                int count = _stateInfo.Pbx.Clients.Values.Count(cl => cl.IsConnected && cl.Info.Id != requestInfo.ClientId);
                _stateInfo.Pbx.Clients[requestInfo.ClientId].Send(count.ToString()); // first send how many there is to read
                _stateInfo.Pbx.Clients.Keys.Where(clientId => _stateInfo.Pbx.Clients[clientId].IsConnected && clientId != requestInfo.ClientId)
                    .ToList().ForEach(id => _stateInfo.Pbx.Clients[requestInfo.ClientId].Send(id.ToString()));   // send all active clients except the client sent the request
            }
        }
    }
}
