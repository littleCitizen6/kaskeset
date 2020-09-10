using Kaskeset.Client.MenuHandling;
using Kaskeset.Common.Extensions;
using MenuBuilder.Abstraction;
using MenuBuilder.Menus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kaskeset.Client
{
    public class ClientRunner
    {
        private ClientInfo _info;
        private ClientController _controller;
        private MenuHandler _menuHandler;
        private IMenu _headMenu;
        private IMenu _groupMenu;

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
            string name = _menuHandler.GetString("insert your name");
            _controller.Server.UpdateName(name);
            
        }
        public string CreateChat(string userKey)
        {
            var name = _menuHandler.GetString("please insert chat name");
            List<string> clients = new List<string> { _info.ClientId.ToString()}; //add the chat creator to chat participent
            Dictionary<string, string> optionalClients = new Dictionary<string, string>();
            _controller.GetAllClients().ForEach(cl => optionalClients.Add(cl.Split("::")[1], cl.Split("::")[0]));
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
            _controller.GetRelatedChats().ForEach(chat => optionDecriptor.Add(chat.Split("::")[1], chat.Split("::")[0]));
            return _menuHandler.MoveToDynamicMenu("groupChats", optionDecriptor, _controller.ChoosePrivateChat);
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
            menuOptions.Add(new Option<string>("select",_controller.ChooseGroupChat , "choose a group chat"));
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
