using Kaskeset.Common.RequestInfo;
using Kaskeset.Common.Requests;
using Kaskeset.Server.CommonInfo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Kaskeset.Server.RequestHandeling.RequestHandlers
{
    public class CreateChatHandler : IRequestHandler
    {
        private IStateInfo _stateInfo;
        public CreateChatHandler(IStateInfo stateInfo)
        {
            _stateInfo = stateInfo;
        }

        public void Handle(Request request)
        {
            CreateChatInfo requestInfo = new CreateChatInfo();
            requestInfo.LoadFromDictionary(request.Properties);
            if (requestInfo.Name == "private")
            {
                HandlePrivateChat(requestInfo);
            }
            else
            {
                HandleGroupChat(requestInfo);
            }
        }

        private void HandleGroupChat(CreateChatInfo requestInfo)
        {
            var chatId = _stateInfo.Chats.CreateChat(requestInfo.Name);
            requestInfo.ParticipentsId.ForEach(id => _stateInfo.Chats.ChatById[chatId].AppendClient(_stateInfo.Pbx.Clients[id]));
            _stateInfo.Pbx.Clients[requestInfo.ClientId].Send(chatId.ToString());
        }

        private void HandlePrivateChat(CreateChatInfo requestInfo)
        {
            var chat = _stateInfo.Chats.ChatById.Values.ToList().FirstOrDefault(chat =>     //validate if exist by participents in privates chat
                     chat.Name == "private" &&                       
                     chat.Clients.Exists(client => client.Info.Id == requestInfo.ParticipentsId[0]) &&
                     chat.Clients.Exists(client2 => client2.Info.Id == requestInfo.ParticipentsId[1]));
            bool chatExist = chat != null; 
            if (chatExist)//send chat id
            {
                _stateInfo.Pbx.Clients[requestInfo.ClientId].Send(chat.Id.ToString());
            }
            else //send created chat id
            {
                var paticipents = _stateInfo.Pbx.Clients.Values.Where(cl =>requestInfo.ParticipentsId.Contains(cl.Info.Id)).ToList();
                _stateInfo.Pbx.Clients[requestInfo.ClientId].Send(_stateInfo.Chats.CreateChat("private", paticipents).ToString());
            }
        }
    }
}
