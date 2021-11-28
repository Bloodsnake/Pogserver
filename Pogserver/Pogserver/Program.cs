using System.Collections.Generic;

namespace Pogserver
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string,IRequest> requests = new Dictionary<string,IRequest>();
            requests.Add("/", new ContentRequest("index.html");
            requests.Add("/shutdown", new ContentRequest("shutdown.html"));

            var apirequest = new API.APIRequest(new API.Requests.ShutDownRequest());

            requests.Add("/shutdown", apirequest);
            var server = new Server();

            server.Run("Content/", requests).GetAwaiter().GetResult();
        }
    }
}