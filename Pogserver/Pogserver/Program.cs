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
            gets.Add("/", new ContentRequest("index.html"));
            gets.Add("/shutdown", new ContentRequest("shutdown.html"));

            var posts = new Dictionary<string, IRequest>();
            posts.Add("/shutdown", new API.APIRequest(new API.Requests.ShutDownRequest()));

            requests.Add("GET", gets);
            requests.Add("POST", posts);

            //Dictionary<string,IRequest> requests = new Dictionary<string,IRequest>();
            //requests.Add("/", new ContentRequest("index.html");
            //requests.Add("/shutdown", new ContentRequest("shutdown.html"));

            //var apirequest = new API.APIRequest(new API.Requests.ShutDownRequest());

            //requests.Add("/shutdown", apirequest);
            var server = new Server();

            server.Run("Content/", requests).GetAwaiter().GetResult();
        }
    }
}