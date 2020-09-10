using Kaskeset.Server.ClientsConnection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kaskeset.Server.CommonInfo
{
    public class Pbx
    {
        public Dictionary<Guid,IClientConnection> Clients { get; }
        public Pbx()
        {
            Clients = new Dictionary<Guid, IClientConnection>();
        }
        public Guid AddClient(IClientConnection client)
        {
            Clients.Add(client.Info.Id, client);
            return client.Info.Id;
        }
    }
}
