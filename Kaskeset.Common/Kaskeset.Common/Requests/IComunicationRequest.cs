using System;
using System.Collections.Generic;
using System.Text;

namespace Kaskeset.Common.Requests
{
    public interface IRequestInfo
    {
        Dictionary<string, string> ToDictionary();
        void LoadFromDictionary(Dictionary<string, string> properties);
    }
}
