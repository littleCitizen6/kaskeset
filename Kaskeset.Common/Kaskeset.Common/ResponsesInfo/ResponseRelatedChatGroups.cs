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
            try
            {
                Chats = properties["Chats"].Split('|').ToList();
            }catch(Exception) // if there is no chats
            {
                Chats = new List<string>();
            }
        }

        public Dictionary<string, string> ToDictionary()
        {
            Dictionary<string, string> prop = new Dictionary<string, string>();
            try
            {
                prop.Add("Chats", Chats.ToSeperateByVerticalString<string>());
            }
            catch (Exception) { } // if there is no chats then it will go to the eception handle in LoadFromDictionary
            return prop;
        }
    }
}
