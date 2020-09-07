using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kaskeset.Server.ClientsConnection
{
    public interface IClientConnection : IDisposable

    {
        bool IsConnected { get; }
        Task Run();
        void Send(string response);
    }
}
