using System;
using System.Collections.Generic;
using System.Text;

namespace Kaskeset.Server.ClientInfo
{
    public interface IClientInfo
    {
        public string? Name { get; set; }
        public Guid Id { get; }
    }
}
