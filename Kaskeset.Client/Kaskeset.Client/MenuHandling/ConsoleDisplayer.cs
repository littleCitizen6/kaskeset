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
    }
}
