using System;
using System.Collections.Generic;
using System.Text;

namespace Kaskeset.Common.Requests
{
    public enum RequestType
    {
        TextMessage,
        ChatConnection,
        UpdateName,
        CreateChat,
        GetAllClients,
        GetRelatedChatGroups
    }
    [Serializable()]
    public class Request
    {
        public Guid Id { get; set; }
        public RequestType Type { get; set; }
        public Dictionary<string, string> Properties { get; set; }
        public Request(RequestType type, Dictionary<string, string> prop)
        {
            Id = Guid.NewGuid();
            Properties = prop;
            Type = type;
        }
    }
}
