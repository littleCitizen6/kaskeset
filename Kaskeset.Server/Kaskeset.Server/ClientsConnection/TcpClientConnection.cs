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
        private IRequestHandler _requestHandler;
        public bool IsConnected => throw new NotImplementedException();

        public TcpClientConnection(TcpClient client, IRequestHandler requestHandler)
        {
            _client = client;
            _requestHandler = requestHandler;
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
                NetworkStream stream = _client.GetStream();

                int bytesRec;
                while (_requestHandler.Continue && IsConnected)
                {
                    bytesRec = stream.Read(bytes, 0, bytes.Length);
                    if (bytesRec > 0)
                    {
                        stream.Write(_requestHandler.Handle(bytes, bytesRec));
                    }
                }
            });
        }

        public void Send(string response)
        {
            throw new NotImplementedException();
        }
    }
}
