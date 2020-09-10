using Kaskeset.Server.ClientsConnection;
using Kaskeset.Server.CommonInfo;
using Kaskeset.Server.RequestHandeling.RequestHandlingManegment;
using Microsoft.Extensions.Logging;
using System;

namespace Kaskeset.Server.Runners
{
    public class BasicRunner : IDisposable
    {
        protected TcpBasicListener _listener;
        protected IStateInfo _stateInfo;
        public BasicRunner(string address, int port, ILogger logger)
        {
            _stateInfo = new BasicStateInfo(logger);
            var requesHandler = new BasicRequestHandlingManeger(_stateInfo, logger);
            _listener = new TcpBasicListener(address, port, _stateInfo, requesHandler, logger);
        }

        public void Run()
        {
            _listener.StartListening();
        }
        public void Dispose()
        {
            _stateInfo.Dispose();
        }
        
    }
}
