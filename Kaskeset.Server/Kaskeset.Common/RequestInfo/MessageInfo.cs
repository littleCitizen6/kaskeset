using System;
using System.Collections.Generic;
using System.Text;

namespace Kaskeset.Common.RequestInfo
{
    public class MessageInfo : IRequestInfo
    {

        public int ChatId { get; set; }
        public string Value { get; set; }
        public Guid ClientId { get; set ; }

        public Dictionary<string, string> ToDictionary()
        {
            Dictionary<string, string> prop = new Dictionary<string, string>();
            prop.Add("ChatId", ChatId.ToString());
            prop.Add("Value", ChatId.ToString());
            prop.Add("ClientId", ChatId.ToString());
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
