using Kaskeset.Client.MenuHandling;
using Kaskeset.Common.Extensions;
using MenuBuilder.Abstraction;
using System;
using System.Collections.Generic;

namespace Kaskeset.Client
{
    public class ClientRunner
    {
        private ClientInfo _info;
        private ClientController _controller;
        private MenuHandler _menuHandler;
        private IMenu _headMenu;

        public ClientRunner(string adress, int port)
        {
            _info = new ClientInfo();
            _controller = new ClientController(new Server(new TcpClientHandler(adress,port), _info),_info ,new ConsoleDisplayer());
            _menuHandler = new MenuHandler();
            _headMenu = MenuHandler.CreateStringsMenu(GetHeadMenuOptions());
            InitMenus();
        }
        public void Run()
        {
            Console.WriteLine("start");
            GetClientName();
            _menuHandler.Runner.Run(_headMenu);
        }

        private void GetClientName()
        {
            string name = _menuHandler.GetValidatedString("insert your name", new List<string> { "::"});
            _controller.Server.UpdateName(name);
            
        }
        public string CreateChat(string userKey)
        {
            var name = _menuHandler.GetValidatedString("please insert chat name", new List<string> { "::" });
            List<string> clients = new List<string> { _info.ClientId.ToString()}; //add the chat creator to chat participent
            Dictionary<string, string> optionalClients = new Dictionary<string, string>();
            _controller.GetAllClients().ForEach(cl => optionalClients.Add(cl.Split("::")[1], cl.Split("::")[0]));
            optionalClients.Add("next", "finish add clients");
            string input = _menuHandler.ChooseFromOption(optionalClients);
            while (input != "next") // create the clients for chat
            {
                clients.Add(input);
                optionalClients.Remove(input);
                input = _menuHandler.ChooseFromOption(optionalClients);
            }
            _controller.Server.CreateChat(name, clients.ConvertListToType<Guid>());
            return "created succesfully";
        }

        public string PrivateChatMenu(string userKey)
        {
            Dictionary<string, string> optionDecriptor = new Dictionary<string, string>();
            _controller.GetAllClients().ForEach(cl => optionDecriptor.Add(cl.Split("::")[1], cl.Split("::")[0]));
            return _menuHandler.MoveToDynamicMenu("private", optionDecriptor, _controller.ChoosePrivateChat);
        }
        public string GroupChatsMenu(string userKey)
        {
            Dictionary<string, string> optionDecriptor = new Dictionary<string, string>();
            var relatedChat = _controller.GetRelatedChats();
            if (relatedChat.Count ==0)
            {
                return "you are have no group chats...";
            }
           relatedChat.ForEach(chat => optionDecriptor.Add(chat.Split("::")[1], chat.Split("::")[0]));
           _menuHandler.MoveToDynamicMenu("groupChats", optionDecriptor, _controller.ChooseGroupChat);
            return "inserting";
        }

        private void InitMenus()
        {
            _menuHandler.Runner.AddMenu("head", _headMenu);
            _menuHandler.Runner.AddMenu("group", GetGroupMenu());
        }

        private IMenu GetGroupMenu()
        {
            var menuOptions = new List<Option<string>>();
            menuOptions.Add(new Option<string>("create", CreateChat, "create a new chat"));
            menuOptions.Add(new Option<string>("select", GroupChatsMenu, "choose a group chat"));
            menuOptions.Add(new Option<string>("back", _menuHandler.GetPrevious, "previous menu"));
            return MenuHandler.CreateStringsMenu(menuOptions);

        }

        private List<Option<string>> GetHeadMenuOptions()
        {
            var mainActions = new List<Option<string>>();
            mainActions.Add(new Option<string>("global", _controller.InsertGlobalChat, "global chat"));
            mainActions.Add(new Option<string>("private", PrivateChatMenu, "choose a private chat"));
            mainActions.Add(new Option<string>("group",_menuHandler.MoveToOtherMenu , "choose a Group chat"));
            mainActions.Add(new Option<string>("exit", _menuHandler.Exit, "exit"));
            return mainActions;
        }
    }
}
