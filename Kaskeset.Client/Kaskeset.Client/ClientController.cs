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
        public ClientController(Server server, IDisplayer displayer)
        {
            Server = server;
            _displayer = displayer;
        }

        public string InsertGlobalChat(string userKey)
        {
            _displayer.DisplayOnly("welcome to global chat");
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            Server.ConnectChat(0, true);
            Task.Run(() => Server.DisplayMessagesAsync(_displayer), token);
            string msg = Console.ReadLine(); // change to param getter ;
            while (msg != "exit")
            {
                Server.SendMessage(msg, 0);
                msg = Console.ReadLine(); // change to param getter 
            }
            tokenSource.Cancel();
            Server.ConnectChat(0, false);
            return "exit succesfully";
        }

    }
}
