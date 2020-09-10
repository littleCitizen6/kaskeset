using Kaskeset.Client.MenuHandling;
using Kaskeset.Common.ResponsesInfo;
using System;
using System.Collections.Generic;
using System.Threading;
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

        public List<string> ConnectChat(int chatId)
        {
            _tcpServer.Send(_factory.CreateChatConnectionRequest(chatId, true));
            ResponseHistoryInfo historyInfo = new ResponseHistoryInfo();
            var historyResponse = _tcpServer.ReciveRequest();
            historyInfo.LoadFromDictionary(historyResponse.Properties);
            return historyInfo.Messages;

        }
        public void DisConnectChat(int chatId)
        {
            _tcpServer.Send(_factory.CreateChatConnectionRequest(chatId, false));
        }
        public void SendMessage(string msg, int chatId)
        {
            _tcpServer.Send(_factory.CreateMessageRequest(msg, chatId));
        }
        public void DisplayMessagesAsync(IDisplayer displayer, CancellationToken token)
        {
            Task.Run(()=>
            {
                string msg = _tcpServer.Recive();
                while (msg != "exit") // if client want to disconnect
                {
                    displayer.Display(msg);
                    msg = _tcpServer.Recive();
                }
            }, token);
        }
        public int CreateChat(string name, List<Guid> participentsId)
        {
            _tcpServer.Send(_factory.CreateCreateChatRequest(name, participentsId));
            return int.Parse(_tcpServer.Recive());
        }
        public List<string> GetAllClients()
        {
            _tcpServer.Send(_factory.CreateGetAllClientsRequest());
            ResponseAllClientsInfo allClientsInfo = new ResponseAllClientsInfo();
            allClientsInfo.LoadFromDictionary(_tcpServer.ReciveRequest().Properties);
            return allClientsInfo.Clients;
        }
        public List<string> GetRealatedChats()
        {
            _tcpServer.Send(_factory.CreateGetRealatedChatGroupsRequest());
            ResponseRelatedChatGroups relatedChatGroups = new ResponseRelatedChatGroups();
            relatedChatGroups.LoadFromDictionary(_tcpServer.ReciveRequest().Properties);
            return relatedChatGroups.Chats;
        }
    }
}
