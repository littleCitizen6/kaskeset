using System;
using System.Collections.Generic;
using System.Text;

namespace Kaskeset.Common.Requests
{
    public enum RequestType
    {
        Message,
        Action
    }
    public class Request
    {
        public Guid Id { get; set; }
        public RequestType Type { get; set; }
        public Dictionary<string, string> Properties { get; set; }
        public Request()
        {
            Properties = new Dictionary<string, string>();
        }
    }
}
