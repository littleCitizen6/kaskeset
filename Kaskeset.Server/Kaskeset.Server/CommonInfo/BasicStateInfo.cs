using System;
using System.Collections.Generic;
using System.Text;

namespace Kaskeset.Server.CommonInfo
{
    public class BasicStateInfo : IStateInfo
    {
        public Pbx Pbx { get; set; }
        public Chats Chats { get; set; }
        public BasicStateInfo()
        {
            Pbx = new Pbx();
            Chats = new Chats();
        }
    }
}
