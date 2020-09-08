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
        public BasicRunner(string address, int port, ILogger logger)
        {
            IStateInfo stateInfo = new BasicStateInfo();
            var requesHandler = new BasicRequestHandlingManeger(stateInfo, logger);
            _listener = new TcpBasicListener(address, port, stateInfo, requesHandler, logger);
        }

        public void Run()
        {
            _listener.StartListening();
        }
        
    }
}
