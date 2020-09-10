using Kaskeset.Common.RequestInfo;
using Kaskeset.Common.Requests;
using Kaskeset.Common.ResponsesInfo;
using Kaskeset.Common.Extensions;
using Kaskeset.Server.ClientInfo;
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
            ResponseAllClientsInfo allClientsInfo = new ResponseAllClientsInfo();
            allClientsInfo.Clients = _stateInfo.Pbx.Clients.Values.Where(client =>
                client.IsConnected && client.Info.Id != requestInfo.ClientId) // get all connected clients that are not the requesting one.
                .Select(cl => cl.Info).ToList().ConvertListToStrings<IClientInfo>();
            _stateInfo.Pbx.Clients[requestInfo.ClientId].Send(new Request(RequestType.GetAllClients, allClientsInfo.ToDictionary()));
    
        }
    }
}
