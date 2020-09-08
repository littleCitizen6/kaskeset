using Kaskeset.Client.MenuHandling;
using MenuBuilder.Abstraction;
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
            _controller = new ClientController(new Server(new TcpClientHandler(adress,port)),new ConsoleDisplayer());
            _menuHandler = new MenuHandler();
            _headMenu = MenuHandler.CreateNumberMenu(GetHeadMenuOptions());
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

        private void InitMenus()
        {
            _menuHandler.Runner.AddMenu("head", _headMenu);
            //_menuHandler.Runner.AddMenu("1", _headMenu);
            //_menuHandler.Runner.AddMenu("2", _headMenu);
        }
        private List<Option<int>> GetHeadMenuOptions()
        {
            var mainActions = new List<Option<int>>();
            mainActions.Add(new Option<int>(1, _controller.InsertGlobalChat, "for global chat"));
            //mainActions.Add(new Option<int>(1, _menuHandler.MoveToOtherMenu, "for choose a chat"));
            //mainActions.Add(new Option<int>(2, _controller.GetCurrentAuctions, "for look at the auction that preforming right now"));

            return mainActions;
        }
    }
}
