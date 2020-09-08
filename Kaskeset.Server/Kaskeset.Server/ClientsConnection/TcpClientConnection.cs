using Kaskeset.Server.ClientInfo;
using Kaskeset.Server.RequestHandeling;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Kaskeset.Server.ClientsConnection
{
    public class TcpClientConnection : IClientConnection
    {
        private TcpClient _client;
        private IRequestHandlingManeger _requestHandler;
        public bool IsConnected => _client.Connected;

        public IClientInfo Info { get; set; }

        public TcpClientConnection(TcpClient client, IRequestHandlingManeger requestHandler)
        {
            Info = new BasicClientInfo(); // todo: get as param
            _client = client;
            _requestHandler = requestHandler;
            Send(Info.Id.ToString());
        }
        public void Dispose()
        {
            _client.Dispose();
        }

        public Task Run()
        {
            return new Task(() =>
            {
                Byte[] bytes = new Byte[256];

                int bytesRec;
                while (_requestHandler.Continue && IsConnected)
                {
                    bytesRec = _client.GetStream().Read(bytes, 0, bytes.Length);
                    _requestHandler.Handle(bytes, bytesRec); // change requestHandler
                }
            });
        }

        public void Send(string response)
        {
            _client.GetStream().Write(Encoding.ASCII.GetBytes(response));
        }
    }
}
