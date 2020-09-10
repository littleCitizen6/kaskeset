using Kaskeset.Server.ClientsConnection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Kaskeset.Server.CommonInfo
{
    public class Chat : IDisposable
    {
        private static int idGenrate = 0;
        private ILogger _logger;
        public List<IClientConnection> Clients { get; set; }
        public List<IClientConnection> Connected { get; set; }
        public List<string> History { get; set; }
        private event Action<string> _onWrite;
        private object _locker;
        public int Id { get;}
        public string Name { get; set; }
        public Chat(string name, ILogger logger)
        {
            _logger = logger;
            Name = name;
            _locker = new object();
            History = new List<string>();
            Clients = new List<IClientConnection>();
            Connected = new List<IClientConnection>();
            lock(_locker) //make  sure no one change the id while chat crerated
            {
                Id = idGenrate;
                Interlocked.Increment(ref idGenrate);
            }
            _logger.LogInformation($"chat created with name {name}, and id: {Id}");
            _onWrite += History.Add;
        }
        public void Write(string msg, IClientConnection client)
        {
            _logger.LogInformation($"recived meesage, in chat {this}, from client {client.Info}, with value {msg}");
            _onWrite -= client.Send;
            _onWrite?.Invoke(msg);
            _onWrite += client.Send;
        }
        public void Connect(IClientConnection client)
        {
            _logger.LogInformation($"client has connected for {this} - {client.Info}");
            _onWrite += client.Send;
            Connected.Add(client);
        }
        public void Disconnect(IClientConnection client)
        {
            _logger.LogInformation($"client has disconnected - {client.Info}");
            _onWrite -= client.Send;
            Connected.Remove(client);
        }

        public void AppendClient(IClientConnection client)
        {
            Clients.Add(client);
        }
        public void Dispose() // dispose event
        {
            Connected.ForEach(cl => _onWrite -= cl.Send);
            _onWrite -= History.Add; 
        }
        public override string ToString()
        {
            return $"{Name}::{Id}";
        }

    }
}
