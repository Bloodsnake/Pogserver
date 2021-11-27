using System.Collections.Generic;

namespace Pogserver
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string,IRequest> requests = new Dictionary<string,IRequest>();
            requests.Add("/", new ContentRequest("index.html"));
            requests.Add("/shutdown", new ContentRequest("shutdown.html"));
            var server = new Server();
            server.Run("Pages/", requests).GetAwaiter().GetResult();
        }
    }
}