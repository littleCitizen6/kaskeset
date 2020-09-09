using Kaskeset.Server.ClientInfo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kaskeset.Server.ClientsConnection
{
    public interface IClientConnection : IDisposable

    {
        IClientInfo Info { get; set; }
        bool IsConnected { get; }
        void Run();
        void Send(string response);
    }
}
