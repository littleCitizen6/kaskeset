using System;
using System.Collections.Generic;
using System.Text;

namespace Kaskeset.Client
{
    public class ClientInfo
    {
        public Guid ClientId { get; set; }
        public int GlobalChatId { get; set; }
        public Dictionary<Guid, int> PrivateChats { get; set; }

    }
}
