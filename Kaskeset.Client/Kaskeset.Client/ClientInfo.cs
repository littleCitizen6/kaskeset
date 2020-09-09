using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Kaskeset.Client
{
    public class ClientInfo
    {
        public Guid ClientId { get; set; }
        public int GlobalChatId { get; set; }
        public ConcurrentDictionary<Guid, int> PrivateChats { get; set; }
        public ClientInfo()
        {
            GlobalChatId = 0;
            PrivateChats = new ConcurrentDictionary<Guid, int>();
        }

    }
}
