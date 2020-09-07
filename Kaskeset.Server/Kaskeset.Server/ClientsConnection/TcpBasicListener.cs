using Kaskeset.Server.RequestHandeling;
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
        private List<IClientConnection> _clients;
        private IPAddress _ipAddress;
        private int _port;
        private IRequestHandler _requestHandler;
        private object _locker;
        public TcpBasicListener(string address, int port, IRequestHandler requestHandler)
        {
            _ipAddress = IPAddress.Parse(address);
            _port = port;
            _clients= new List<IClientConnection>();
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
                    //Console.Write("Waiting for a connection... ");

                    TcpClient client = server.AcceptTcpClient();
                    //Console.WriteLine("Connected!");
                    Task Connect = new Task(() =>
                    {
                        int index;
                        lock (_locker)
                        {
                            _clients.Add(new TcpClientConnection(client, _requestHandler));
                            index = _clients.Count - 1;
                        }
                        _clients[index].Run();
                        _clients[index].Dispose();
                    });
                    Connect.Start();
                }
            }
            catch(Exception ex)
            {
                
            }
        }
    }
}
