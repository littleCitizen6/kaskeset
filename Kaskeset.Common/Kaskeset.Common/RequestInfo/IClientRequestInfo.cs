using Kaskeset.Common.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kaskeset.Common.RequestInfo
{
    public interface IClientRequestInfo : IRequestInfo
    {
        Guid ClientId { get; set; } // because each Request must have this field
    }
}
