using Kaskeset.Client.MenuHandling;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Kaskeset.Client
{
    public class Server
    {
        private ClientInfo _info;
        private TcpClientHandler _tcpServer;
        private RequestFactory _factory;
        public Server(TcpClientHandler tcpServer, ClientInfo info)
        {
            _info = info;
            _tcpServer = tcpServer;
            _factory = new RequestFactory(_info);
            _info.ClientId = Guid.Parse(_tcpServer.Recive()); // the server send the client id when start connection
        }
        public void UpdateName(string name)
        {
            _tcpServer.Send(_factory.CreateUpdateNameRequest(name));
        }

        public void ConnectChat(int chatId, bool toConnect)
        {
            _tcpServer.Send(_factory.CreateChatConnectionRequest(chatId, toConnect));
        }
        public void SendMessage(string msg, int chatId)
        {
            _tcpServer.Send(_factory.CreateMessageRequest(msg, chatId));
        }
        public Task DisplayMessagesAsync(IDisplayer displayer)
        {
            while(true)
            {
                displayer.Display(_tcpServer.Recive());
            }
        }
        public int CreateChat(string name, List<Guid> participentsId)
        {
            _tcpServer.Send(_factory.CreateCreateChatRequest(name, participentsId));
            return int.Parse(_tcpServer.Recive());
        }
        public List<string> GetAllClients()
        {
            _tcpServer.Send(_factory.CreateGetAllClientsRequest());
            List<string> clients = new List<string>();
            int count = int.Parse(_tcpServer.Recive()); // send first the count
            for (int i = 0; i < count; i++)
            {
                clients.Add(_tcpServer.Recive());
            }
            return clients;
        }
    }
}
