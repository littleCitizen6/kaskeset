using Kaskeset.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kaskeset.Common.ResponsesInfo
{
    public class ResponseHistoryInfo : IResponseInfo
    {
        public List<string> Messages { get; set; }
        public void LoadFromDictionary(Dictionary<string, string> properties)
        {
            try
            {
                Messages = properties["Messages"].Split('|').ToList();
            }
            catch (Exception)
            {
                Messages = new List<string>();
            }
        }

        public Dictionary<string, string> ToDictionary()
        {
            Dictionary<string, string> prop = new Dictionary<string, string>();
            try
            {
                prop.Add("Messages", Messages.ToSeperateByVerticalString<string>());
            }
            catch (Exception) { } // if there is no chats then it will go to the exception handle in LoadFromDictionary
            return prop;
        }
    }
}
