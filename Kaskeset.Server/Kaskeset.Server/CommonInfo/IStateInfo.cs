using System;
using System.Collections.Generic;
using System.Text;

namespace Kaskeset.Server.CommonInfo
{
    public interface IStateInfo : IDisposable
    {
        Pbx Pbx { get; set; }
        Chats Chats { get; set; }
    }
}
