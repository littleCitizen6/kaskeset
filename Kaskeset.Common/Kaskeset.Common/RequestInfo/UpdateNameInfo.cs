using System;
using System.Collections.Generic;
using System.Text;

namespace Kaskeset.Common.RequestInfo
{
    public class UpdateNameInfo : IClientRequestInfo
    {
        public Guid ClientId { get; set ; }
        public string Name { get; set; }
        public void LoadFromDictionary(Dictionary<string, string> properties)
        {
            ClientId = Guid.Parse(properties["ClientId"]);
            Name = properties["Name"];
        }

        public Dictionary<string, string> ToDictionary()
        {
            Dictionary<string, string> prop = new Dictionary<string, string>();
            prop.Add("ClientId", ClientId.ToString());
            prop.Add("Name", Name);
            return prop;    
        }
    }
}
