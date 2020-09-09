using Kaskeset.Server.ClientInfo;
using Kaskeset.Server.RequestHandeling.RequestHandlingManegment;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Sockets;
using System.Text;

namespace Kaskeset.Server.ClientsConnection
{
    public class TcpClientConnection : IClientConnection
    {
        private ILogger _logger;
        private TcpClient _client;
        private IRequestHandlingManeger _requestHandler;
        public bool IsConnected => _client.Connected;

        public IClientInfo Info { get; set; }

        public TcpClientConnection(TcpClient client, IRequestHandlingManeger requestHandler, ILogger logger)
        {
            Info = new BasicClientInfo(); // todo: get as param
            _client = client;
            _requestHandler = requestHandler;
            _logger = logger;
            _logger.LogInformation($"Sending Id {Info.Id}");
            Send(Info.Id.ToString());
        }
        public void Dispose()
        {
            _client.Dispose();
        }

        public void Run()
        {
            Byte[] bytes = new Byte[_client.ReceiveBufferSize];
            int bytesRec;
            while (_requestHandler.Continue && IsConnected)
            {
                bytesRec = _client.GetStream().Read(bytes, 0, bytes.Length);
                _requestHandler.Handle(bytes, bytesRec); // change requestHandler
            }
            _logger.LogInformation($"client left : {Info.Name}::{Info.Id}");
        }

        public void Send(string response)
        {
            _client.GetStream().Write(Encoding.ASCII.GetBytes(response));
        }
    }
}
