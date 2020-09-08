using Kaskeset.Server.ClientsConnection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Kaskeset.Server.CommonInfo
{
    public class Chat : IDisposable
    {
        private static int idGenrate = 0;
        public List<IClientConnection> Clients { get; set; }
        public List<IClientConnection> Connected { get; set; }
        public List<string> History { get; set; }
        private event Action<string> _onWrite;
        private object _locker;
        public int Id { get;}
        public string Name { get; set; }
        public Chat(string name)
        {
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
            _onWrite += History.Add;
        }
        public void Write(string msg, IClientConnection client)
        {
            _onWrite -= client.Send;
            _onWrite?.Invoke(msg);
            _onWrite += client.Send;
        }
        public void Connect(IClientConnection client)
        {
            _onWrite += client.Send;
        }
        public void Disconnect(IClientConnection client)
        {
            _onWrite -= client.Send;
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
            return $"{Id}:: {Name}";
        }

    }
}
