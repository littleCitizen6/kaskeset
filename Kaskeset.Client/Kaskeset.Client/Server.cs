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
        public Server(TcpClientHandler tcpServer)
        {
            _info = new ClientInfo();
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
        public Guid CreateChat(string name, List<Guid> participentsId)
        {
            _tcpServer.Send(_factory.CreateCreateChatRequest(name, participentsId));
            return Guid.Parse(_tcpServer.Recive());
        }
    }
}
