using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kaskeset.Server.CommonInfo
{
    public class BasicStateInfo : IStateInfo
    {
        private ILogger _logger;
        public Pbx Pbx { get; set; }
        public Chats Chats { get; set; }
        public BasicStateInfo(ILogger logger)
        {
            _logger = logger;
            Pbx = new Pbx();
            Chats = new Chats(logger);
        }

        public void Dispose()
        {
            Chats.Dispose();
            Pbx.Clients.Values.ToList().ForEach(cl => cl.Dispose());
            _logger.LogInformation("all clients remained have disposed");
        }
    }
}
