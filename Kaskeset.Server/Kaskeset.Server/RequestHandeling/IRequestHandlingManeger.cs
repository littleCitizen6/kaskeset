using System;
using System.Collections.Generic;
using System.Text;

namespace Kaskeset.Server.RequestHandeling
{
    public interface IRequestHandlingManeger
    {
        bool Continue { get; }
        void Handle(byte[] msg, int bytesRec);
    }
}
