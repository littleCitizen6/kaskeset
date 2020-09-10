using System;
using System.Collections.Generic;
using System.Text;

namespace Kaskeset.Common.RequestInfo
{
    public class GetAllClientsInfo : IClientRequestInfo
    {
        public Guid ClientId { get; set; }

        public void LoadFromDictionary(Dictionary<string, string> properties)
        {
            ClientId = Guid.Parse(properties["ClientId"]);
        }

        public Dictionary<string, string> ToDictionary()
        {
            Dictionary<string, string> prop = new Dictionary<string, string>();
            prop.Add("ClientId", ClientId.ToString());
            return prop;
        }
    }
}
