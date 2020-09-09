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

        public string ChoosePrivateChat(string userKey)
        {
            string[] clientInfo = userKey.Split("::"); //because server send in name::Id format
            var name = clientInfo[0];
            var id = Guid.Parse(clientInfo[1]); 
            if (_info.PrivateChats.ContainsKey(id))
            {
                InsertChat(_info.PrivateChats[id], name);
            }
            else
            {
                _info.PrivateChats.TryAdd(id,Server.CreateChat("private", new List<Guid> { id, _info.ClientId })); 
                InsertChat(_info.PrivateChats[id], name);
            }
            return "exit succesfully"; //because happend only when client exited from the chat
        }

        private void InsertChat(int chatId, string representingName)
        {
            _displayer.DisplayOnly($"welcome to {representingName} chat");
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            Server.ConnectChat(chatId, true);
            Task.Run(() => Server.DisplayMessagesAsync(_displayer), token);
            string msg = Console.ReadLine(); // change to param getter 
            while (msg != "exit")
            {
                Server.SendMessage(msg, chatId);
                msg = Console.ReadLine(); // change to param getter 
            }
            tokenSource.Cancel();
            Server.ConnectChat(chatId, false);
        }

    }
}
