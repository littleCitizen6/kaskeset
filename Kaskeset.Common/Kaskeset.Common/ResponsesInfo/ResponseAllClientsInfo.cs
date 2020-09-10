using Kaskeset.Common.Extensions;
using Kaskeset.Common.RequestInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kaskeset.Common.ResponsesInfo
{
    public class ResponseAllClientsInfo : IResponseInfo
    {
        public List<string> Clients { get; set; }
        public void LoadFromDictionary(Dictionary<string, string> properties)
        {
            Clients = properties["Clients"].Split('|').ToList(); 
        }

        public Dictionary<string, string> ToDictionary()
        {
            Dictionary<string, string> prop = new Dictionary<string, string>();
            prop.Add("Clients", Clients.ToSeperateByVerticalString<string>());
            return prop;
        }
    }
}
