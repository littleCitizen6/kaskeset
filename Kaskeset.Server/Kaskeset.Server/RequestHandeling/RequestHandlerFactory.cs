using Kaskeset.Common.Requests;
using Kaskeset.Server.CommonInfo;
using Kaskeset.Server.RequestHandeling.RequestHandlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kaskeset.Server.RequestHandeling
{
    public class RequestHandlerFactory
    {
        private IStateInfo _stateInfo;
        public RequestHandlerFactory(IStateInfo stateInfo)
        {
            _stateInfo = stateInfo;
        }
        public IRequestHandler Generate(Request request)
        {
            switch (request.Type)
            {
                case RequestType.TextMessage:
                    return new MessageHandler(_stateInfo);
                case RequestType.ChatConnection:
                    return new ChatConnectionHandler(_stateInfo);
                default:
                    throw new NotImplementedException("request type does not supported");
            }
        }
    }
}
