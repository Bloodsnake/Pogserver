using System;
using System.Threading.Tasks;

namespace Pogserver
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new Server();
            Task something = server.StartServer("");
            something.GetAwaiter().GetResult();
        }
    }
}