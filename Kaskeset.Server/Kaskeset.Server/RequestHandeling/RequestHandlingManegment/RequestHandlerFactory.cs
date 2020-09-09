using Kaskeset.Common.Requests;
using Kaskeset.Server.CommonInfo;
using Kaskeset.Server.RequestHandeling.RequestHandlers;
using Microsoft.Extensions.Logging;
using System;

namespace Kaskeset.Server.RequestHandeling.RequestHandlingManegment
{
    public class RequestHandlerFactory
    {
        private ILogger _logger;
        private IStateInfo _stateInfo;
        public RequestHandlerFactory(IStateInfo stateInfo, ILogger logger)
        {
            _stateInfo = stateInfo;
            _logger = logger;
        }
        public IRequestHandler Generate(Request request)
        {
            switch (request.Type)
            {
                case RequestType.TextMessage:
                    return new MessageHandler(_stateInfo);
                case RequestType.ChatConnection:
                    return new ChatConnectionHandler(_stateInfo);
                case RequestType.UpdateName:
                    return new UpdateNameHandler(_stateInfo);
                default:
                    throw new NotImplementedException("request type does not supported");
            }
        }
    }
}
