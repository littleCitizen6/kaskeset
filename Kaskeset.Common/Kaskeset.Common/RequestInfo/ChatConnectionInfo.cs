using System;
using System.Collections.Generic;
using System.Text;

namespace Kaskeset.Common.RequestInfo
{
    public class ChatConnectionInfo : IRequestInfo
    {
        public bool ToConnect { get; set; }
        public Guid ClientId { get ; set; }
        public int ChatId { get; set; }
        public virtual void LoadFromDictionary(Dictionary<string, string> properties)
        {
            ClientId = Guid.Parse(properties["ClientId"]);
            ChatId = int.Parse(properties["ChatId"]);
            ToConnect = bool.Parse(properties["ToConnect"]);
        }

        public virtual Dictionary<string, string> ToDictionary()
        {
            Dictionary<string, string> prop = new Dictionary<string, string>();
            prop.Add("ClientId", ClientId.ToString());
            prop.Add("ChatId", ChatId.ToString());
            prop.Add("ToConnect", ToConnect.ToString());
            return prop;
        }
    }
}
