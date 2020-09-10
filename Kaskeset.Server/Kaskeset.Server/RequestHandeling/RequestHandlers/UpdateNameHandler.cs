using Kaskeset.Common.RequestInfo;
using Kaskeset.Common.Requests;
using Kaskeset.Server.CommonInfo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kaskeset.Server.RequestHandeling.RequestHandlers
{
    public class UpdateNameHandler : IRequestHandler
    {
        private IStateInfo _stateInfo;
        public UpdateNameHandler(IStateInfo stateInfo)
        {
            _stateInfo = stateInfo;
        }
        public void Handle(Request request)
        {
            UpdateNameInfo requestInfo = new UpdateNameInfo();
            requestInfo.LoadFromDictionary(request.Properties);
            _stateInfo.Pbx.Clients[requestInfo.ClientId].Info.Name = requestInfo.Name;
        }
    }
}
