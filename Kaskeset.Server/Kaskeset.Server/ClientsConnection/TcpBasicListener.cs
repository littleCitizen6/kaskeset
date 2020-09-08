using Kaskeset.Server.CommonInfo;
using Kaskeset.Server.RequestHandeling;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Kaskeset.Server.ClientsConnection
{
    public class TcpBasicListener
    {
        private ILogger _logger;
        private IStateInfo _stateInfo;
        private IPAddress _ipAddress;
        private int _port;
        private IRequestHandlingManeger _requestHandler;
        private object _locker;
        public TcpBasicListener(string address, int port, IStateInfo stateInfo,IRequestHandlingManeger requestHandler, ILogger logger)
        {
            _logger = logger;
            _ipAddress = IPAddress.Parse(address);
            _port = port;
            _stateInfo = stateInfo;
            _requestHandler = requestHandler;
            _locker = new object();
        }
        public void StartListening()
        {
            try
            {
                var server = new TcpListener(_ipAddress, _port);
                server.Start();
                while (true)
                {
                    _logger.LogInformation("wait for new connection");
                    TcpClient client = server.AcceptTcpClient();
                    _logger.LogInformation("a connection created");
                    Task Connect = new Task(() =>
                    {
                        Guid index;
                        lock (_locker)
                        {
                            var cl = new TcpClientConnection(client, _requestHandler, _logger);
                            index = _stateInfo.Pbx.AddClient(cl);
                            _stateInfo.Chats.ChatById[0].AppendClient(cl); // add for global chat
                        }
                        _stateInfo.Pbx.Clients[index].Run();
                        _stateInfo.Pbx.Clients[index].Dispose();
                        _stateInfo.Pbx.Clients.Remove(index);
                    });
                    Connect.Start();
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
        }
    }
}
