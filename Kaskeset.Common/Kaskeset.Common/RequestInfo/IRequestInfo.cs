using System;
using System.Collections.Generic;
using System.Text;

namespace Kaskeset.Common.RequestInfo
{
    public interface IRequestInfo
    {
        int UserId { get; set; }
        Dictionary<string, string> ToDictionary();
        void LoadFromDictionary(Dictionary<string, string> properties);
    }
}
