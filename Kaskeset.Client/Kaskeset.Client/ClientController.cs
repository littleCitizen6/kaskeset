using Kaskeset.Client.MenuHandling;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kaskeset.Client
{
    public class ClientController
    {
        private IDisplayer _displayer;
        public Server Server { get; set; }
        private ClientInfo _info;
        public ClientController(Server server, ClientInfo info, IDisplayer displayer)
        {
            _info = info;
            Server = server;
            _displayer = displayer;
        }

        public string InsertGlobalChat(string userKey)
        {
            InsertChat(_info.GlobalChatId, "global");
            return "exit succesfully";
        }
        public List<string> GetAllClients()
        {
            return Server.GetAllClients();
        }
        // remove from controller
        public List<string> GetRelatedChats()
        {
            return Server.GetRealatedChats();
        }

        public string ChoosePrivateChat(string userKey)
        {
            Guid id = Guid.Parse(userKey);
            if (_info.PrivateChats.ContainsKey(id))
            {
                InsertChat(_info.PrivateChats[id], $"with {userKey}");
            }
            else
            {
                _info.PrivateChats.TryAdd(id,Server.CreateChat("private", new List<Guid> { id, _info.ClientId })); 
                InsertChat(_info.PrivateChats[id], $"with {userKey}");
            }
            return "exit succesfully"; //because happend only when client exited from the chat
        }

        private void InsertChat(int chatId, string representingName)
        {
            _displayer.DisplayOnly($"press exit to get back from chat : {representingName}");
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            Server.ConnectChat(chatId, true);
            Server.DisplayMessagesAsync(_displayer, token);
            string msg = Console.ReadLine();
            while (msg != "exit")
            { 
                Server.SendMessage(msg, chatId); 
                msg = Console.ReadLine(); // change to param getter
            }
            tokenSource.Cancel(); // more safe to cancell before sending exit
            Server.SendMessage("exit", chatId);
            Server.ConnectChat(chatId, false);
        }
    }
}
