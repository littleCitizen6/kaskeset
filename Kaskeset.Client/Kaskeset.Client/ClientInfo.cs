using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Kaskeset.Client
{
    public class ClientInfo
    {
        public ClientInfo()
        {
            GlobalChatId = 0;
        }
        public Guid ClientId { get; set; }
        public int GlobalChatId { get; set; }
        public ConcurrentDictionary<Guid, int> PrivateChats { get; set; }

    }
}
