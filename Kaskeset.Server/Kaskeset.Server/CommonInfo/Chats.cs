using Kaskeset.Server.ClientsConnection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace Kaskeset.Server.CommonInfo
{
    public class Chats: IDisposable
    {
        private ILogger _logger;
        public ConcurrentDictionary<int, Chat> ChatById { get; set; }
        public Chats(ILogger logger)
        {
            _logger = logger;
            ChatById = new ConcurrentDictionary<int, Chat>();
            CreateChat("global");
        }
        public int CreateChat(string name)
        {
            var chat = new Chat(name, _logger);
            ChatById.TryAdd(chat.Id, chat);
            return chat.Id;
        }
        public int CreateChat(string name, List<IClientConnection> participents)
        {
            var chat = new Chat(name, _logger);
            chat.Clients = participents;
            ChatById.TryAdd(chat.Id, chat);
            return chat.Id;
        }

        public IEnumerable<Chat> GetChats(IClientConnection client)
        {
            return ChatById.Values.Where(chat => chat.Clients.Exists(cl => cl.Info.Id == client.Info.Id));
        }

        public void Dispose()
        {
            ChatById.Values.ToList().ForEach(ch => ch.Dispose());
            _logger.LogInformation("all chats disposed");
        }
    }
}
