using System;
using System.Collections.Generic;
using System.Text;

namespace Kaskeset.Server.RequestHandeling
{
    public interface IRequestHandler
    {
        public bool Continue { get; }
        public byte[] Handle(byte[] msg, int bytesRec);
    }
}
