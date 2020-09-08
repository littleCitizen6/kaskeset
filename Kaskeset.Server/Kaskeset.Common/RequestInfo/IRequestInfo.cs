using System;
using System.Collections.Generic;
using System.Text;

namespace Kaskeset.Common.RequestInfo
{
    public interface IRequestInfo
    {
        Guid ClientId { get; set; } // because each Request must have this field
        Dictionary<string, string> ToDictionary();
        void LoadFromDictionary(Dictionary<string, string> properties);
    }
}
