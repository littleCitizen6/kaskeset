using System;
using System.Collections.Generic;
using System.Text;

namespace Kaskeset.Client.MenuHandling
{
    public class Option<T>
    {
        public T Key { get; private set; }
        public Func<string, string> Func { get; private set; }
        public string Description { get; private set; }
        public Option(T key, Func<string, string> func, string description)
        {
            Key = key;
            Func = func;
            Description = description;
        }

    }
}
