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
        private ClientController _controller;
        private MenuHandler _menuHandler;
        IMenu _headMenu;
        public ClientRunner(string adress, int port)
        {
            ClientInfo info = new ClientInfo();
            _controller = new ClientController(new Server(new TcpClientHandler(adress,port), info),info ,new ConsoleDisplayer());
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
            List<string> clients = new List<string>();
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
        private void InitMenus()
        {
            _menuHandler.Runner.AddMenu("head", _headMenu);
            //_menuHandler.Runner.AddMenu("2", _headMenu);
        }
        private List<Option<string>> GetHeadMenuOptions()
        {
            var mainActions = new List<Option<string>>();
            mainActions.Add(new Option<string>("global", _controller.InsertGlobalChat, "for global chat"));
            mainActions.Add(new Option<string>("private", PrivateChatMenu, "for choose a private chat"));
            //mainActions.Add(new Option<int>(2, _controller.GetCurrentAuctions, "for look at the auction that preforming right now"));

            return mainActions;
        }
    }
}
