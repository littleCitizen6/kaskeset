using Kaskeset.Server.ClientsConnection;
using Kaskeset.Server.CommonInfo;
using Kaskeset.Server.RequestHandeling.RequestHandlingManegment;
using Microsoft.Extensions.Logging;

namespace Kaskeset.Server.Runners
{
    public class BasicRunner
    {
        protected TcpBasicListener _listener;
        public BasicRunner(string address, int port, ILogger logger)
        {
            IStateInfo stateInfo = new BasicStateInfo(logger);
            var requesHandler = new BasicRequestHandlingManeger(stateInfo, logger);
            _listener = new TcpBasicListener(address, port, stateInfo, requesHandler, logger);
        }

        public void Run()
        {
            _listener.StartListening();
        }
        
    }
}
