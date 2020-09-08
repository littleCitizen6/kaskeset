using Kaskeset.Server.ClientsConnection;
using Kaskeset.Server.CommonInfo;
using Kaskeset.Server.RequestHandeling;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kaskeset.Server.Runners
{
    public class BasicRunner
    {
        protected TcpBasicListener _listener;

        public BasicRunner(string address, int port)
        {
            IStateInfo stateInfo = new BasicStateInfo();
            var requesHandler = new BasicRequestHandlingManeger(stateInfo);
            _listener = new TcpBasicListener(address, port, stateInfo, requesHandler);
        }

        public void Run()
        {
            _listener.StartListening();
        }
        
    }
}
