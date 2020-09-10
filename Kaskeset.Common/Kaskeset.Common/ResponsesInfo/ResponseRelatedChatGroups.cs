using Kaskeset.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kaskeset.Common.ResponsesInfo
{
    public class ResponseRelatedChatGroups : IResponseInfo
    {
        public List<string> Chats { get; set; }
        public void LoadFromDictionary(Dictionary<string, string> properties)
        {
            Chats = properties["Chats"].Split('|').ToList();
        }

        public Dictionary<string, string> ToDictionary()
        {
            Dictionary<string, string> prop = new Dictionary<string, string>();
            prop.Add("Chats", Chats.ToSeperateByVerticalString<string>());
            return prop;
        }
    }
}
