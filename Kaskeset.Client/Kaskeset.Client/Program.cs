using Kaskeset.Common.Extensions;
using System;

namespace Kaskeset.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientRunner runner = new ClientRunner("10.1.0.14", 9000);
            runner.Run();
        }
    }
}
