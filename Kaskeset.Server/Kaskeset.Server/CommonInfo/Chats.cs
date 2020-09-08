using Kaskeset.Server.ClientsConnection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections.Concurrent;

namespace Kaskeset.Server.CommonInfo
{
    public class Chats
    {
        public ConcurrentDictionary<int, Chat> ChatById { get; set; }
        public Chats()
        {
            ChatById = new ConcurrentDictionary<int, Chat>();
            CreateChat(new Chat("global"));
        }
        public void CreateChat(Chat chat)
        {
            ChatById.TryAdd(chat.Id, chat);
        }

        public IEnumerable<Chat> GetChats(IClientConnection client)
        {
            return ChatById.Values.Where(chat => chat.Clients.Exists(cl => cl.Info.Id == client.Info.Id));
        }
    }
}
