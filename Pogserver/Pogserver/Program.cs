using System.Collections.Generic;
using Pogserver.Content;
using System;

namespace Pogserver
{
    class Program
    {
        static void Main(string[] args)
        {
            var requests = new Dictionary<string, Dictionary<string, IRequest>>();

            var gets = new Dictionary<string, IRequest>();
            gets.Add("/", new ContentRequest("Content/index.html"));
            gets.Add("/shutdown", new ContentRequest("Content/shutdown.html"));

            var posts = new Dictionary<string, IRequest>();
            posts.Add("/shutdown", new GivePLZ.APIRequest(new GivePLZ.Requests.ShutDownRequest()));

            requests.Add("GET", gets);
            requests.Add("POST", posts);

            var server = new Server();

            server.AddContentLocation("Content/");
            server.AddRequestLists(requests);

            server.Run().GetAwaiter().GetResult();
        }
    }
}