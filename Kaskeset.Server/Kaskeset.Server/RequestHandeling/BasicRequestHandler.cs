using System;
using System.Collections.Generic;
using System.Text;

namespace Kaskeset.Server.RequestHandeling
{
    public class BasicRequestHandler : IRequestHandler
    {
        private bool _continue;
        public bool Continue => _continue;
        public BasicRequestHandler()
        {
            _continue = true;
        }
        public byte[] Handle(byte[] msg, int bytesRec)
        {
            throw new NotImplementedException();
        }
    }
}
