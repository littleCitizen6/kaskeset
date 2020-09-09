using Kaskeset.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kaskeset.Common.RequestInfo
{
    public class CreateChatInfo : IRequestInfo
    {
        public List<Guid> ParticipentsId { get; set; }
        public Guid ClientId { get; set; }
        public string Name { get; set; }

        public CreateChatInfo()
        {
            ParticipentsId = new List<Guid>();
        }
        public void LoadFromDictionary(Dictionary<string, string> properties)
        {
            ClientId = Guid.Parse(properties["ClientId"]);
            ParticipentsId = properties["Participents"].Split('|').ToList().ConvertListType<Guid>(); // check if good
            Name = properties["Name"];
        }

        public Dictionary<string, string> ToDictionary()
        {
            Dictionary<string, string> prop = new Dictionary<string, string>();
            prop.Add("Participents", ParticipentsId.ToSeperateByVerticalString<Guid>());
            prop.Add("ClientId", ClientId.ToString());
            prop.Add("Name", Name);
            return prop;
        }
    }
}
