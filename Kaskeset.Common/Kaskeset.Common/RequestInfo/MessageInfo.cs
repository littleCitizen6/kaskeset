using System;
using System.Collections.Generic;
using System.Text;

namespace Kaskeset.Common.RequestInfo
{
    public class MessageInfo : IClientRequestInfo
    {

        public int ChatId { get; set; }
        public string Value { get; set; }
        public Guid ClientId { get; set ; }

        public Dictionary<string, string> ToDictionary()
        {
            Dictionary<string, string> prop = new Dictionary<string, string>();
            prop.Add("ChatId", ChatId.ToString());
            prop.Add("Value", Value.ToString());
            prop.Add("ClientId", ClientId.ToString());
            return prop;
        }
        public void LoadFromDictionary(Dictionary<string, string> properties)
        {
            ChatId = int.Parse(properties["ChatId"]);
            Value = properties["Value"];
            ClientId = Guid.Parse(properties["ClientId"]);
        }
        public override string ToString()
        {
            return $"{ClientId}: {Value}";
        }
    }
}
