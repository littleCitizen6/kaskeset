using System;

namespace Kaskeset.Server.ClientInfo
{
    class BasicClientInfo : IClientInfo
    {
        
        private Guid _id;
        public string? Name { get ; set ; }
        public Guid Id => _id;
        public BasicClientInfo()
        {
            _id = Guid.NewGuid();
        }
    }
}
