using System;
using System.Collections.Generic;
using System.Text;

namespace Kaskeset.Common.RequestInfo
{
    public class MessageInfo : IRequestInfo
    {

        public int ChatId { get; set; }
        public string Value { get; set; }
        public int UserId { get; set ; }

        public Dictionary<string, string> ToDictionary()
        {
            Dictionary<string, string> prop = new Dictionary<string, string>();
            prop.Add("ChatId", ChatId.ToString());
            prop.Add("Value", ChatId.ToString());
            prop.Add("UserId", ChatId.ToString());
            return prop;
        }
        public void LoadFromDictionary(Dictionary<string, string> properties)
        {
            ChatId = int.Parse(properties["ChatId"]);
            Value = properties["Value"];
            UserId = int.Parse(properties["UserId"]);
        }
        public override string ToString()
        {
            return $"{UserId}: {Value}";
        }
    }
}
