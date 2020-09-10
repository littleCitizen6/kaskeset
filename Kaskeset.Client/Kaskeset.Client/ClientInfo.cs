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
        public Dictionary<Guid, int> PrivateChats { get; set; }
        public Dictionary<int, string> GroupChats { get; set; }
        public ClientInfo()
        {
            GlobalChatId = 0;
            PrivateChats = new Dictionary<Guid, int>();
            GroupChats = new Dictionary<int, string>();
        }

    }
}
