using Kaskeset.Server.ClientsConnection;
using Kaskeset.Server.CommonInfo;
using Kaskeset.Server.RequestHandeling;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kaskeset.Server.Runners
{
    public class BasicRunner
    {
        protected TcpBasicListener _listener;
        protected ILogger _logger;
        public BasicRunner(string address, int port, ILogger logger)
        {
            IStateInfo stateInfo = new BasicStateInfo();
            var requesHandler = new BasicRequestHandlingManeger(stateInfo, _logger);
            _listener = new TcpBasicListener(address, port, stateInfo, requesHandler, _logger);
        }

        public void Run()
        {
            _listener.StartListening();
        }
        
    }
}
