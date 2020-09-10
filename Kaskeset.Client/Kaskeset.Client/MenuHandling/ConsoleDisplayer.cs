using System;
using System.Collections.Generic;
using System.Text;

namespace Kaskeset.Client.MenuHandling
{
    public class ConsoleDisplayer : IDisplayer
    {
        public void Display(string msg)
        {
            Console.WriteLine(msg); ;
        }

        public void DisplayOnly(string msg)
        {
            Console.Clear();
            Console.WriteLine(msg);
        }
    }
}
