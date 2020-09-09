using MenuBuilder;
using MenuBuilder.Abstraction;
using MenuBuilder.Browsers;
using MenuBuilder.Menus;
using MenuBuilder.Presenters;
using MenuBuilder.Providers;
using MenuBuilder.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kaskeset.Client.MenuHandling
{
    public class MenuHandler
    {
        public MenuRunner<string> Runner { get; set; }
        public IParamProvider Provider { get; set; }
        public IPresenter Presenter { get; set; }
        public IParamVaidator Validator { get; set; }
        public IBrowser Browser { get; set; }
        public MenuHandler()
        {
            Browser = new StackBrowser();
            Provider = new ConsoleProvider();
            Presenter = new ConsolePresenter();
            Validator = new StringParamValidator();
            Runner = new MenuRunner<string>(Presenter, Provider, Validator, Browser);
        }

        public static IMenu CreateNumberMenu(List<Option<int>> options)
        {
            var menu = new NumbersMenu();
            foreach (var option in options)
            {
                menu.AddAction(option.Key.ToString(), option.Func, option.Description);
            }
            return menu;
        }
        public static IMenu CreateStringsMenu(List<Option<string>> options)
        {
            var menu = new StringMenu();
            foreach (var option in options)
            {
                menu.AddAction(option.Key.ToString(), option.Func, option.Description);
            }
            return menu;
        }
        public IMenu CreateStringsFunctionMenu(IEnumerable<string> keys, Func<string, string> func)
        {
            var menu = new StringMenu();
            foreach (var key in keys)
            {
                menu.AddAction(key, func, key);
            }
            menu.AddAction("back", GetPrevious, "back to previous menu");
            return menu;
        }

        public string MoveToOtherMenu(string index)
        {
            Runner.Browser.Current = Runner.Menus[index];
            return "succeded";
        }

        public string GetPrevious(string userParam)
        {
            Browser.PreviousOrDefult();
            return "moving back";
        }

        public string Exit(string userParam)
        {
            Runner.Exit();
            return "goodbye";
        }

        public int GetInt(string message)
        {
            int num = 0;
            bool converted;
            do
            {
                try
                {
                    converted = true;
                    Presenter.Display(message);
                    num = Provider.Get<int>();
                }
                catch (FormatException)
                {
                    Presenter.Display("invalid input please insert a number");
                    converted = false;
                }
            } while (!converted);
            return num;
        }
        public string GetString(string message)
        {
            Presenter.Display(message);
            return Provider.Get<string>();
        }
    }
}
