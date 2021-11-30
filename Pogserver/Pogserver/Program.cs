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
            gets.Add("/", new ContentRequest("Content/index.html", ContentRequest.ContentType.HTML));
            gets.Add("/shutdown", new ContentRequest("Content/shutdown.html", ContentRequest.ContentType.HTML));

            gets.Add("/PogStd/PogScript.js", new ContentRequest("Content/PogStd/PogScript.js", ContentRequest.ContentType.JS));
            gets.Add("/PogStd/PogStyles.css", new ContentRequest("Content/PogStd/PogStyles.css", ContentRequest.ContentType.CSS));

            var posts = new Dictionary<string, IRequest>();
            posts.Add("/shutdown", new GivePLZ.APIRequest(new GivePLZ.Requests.ShutDownRequest()));

            requests.Add("GET", gets);
            requests.Add("POST", posts);

            var server = new Server();

            server.Run("", requests).GetAwaiter().GetResult();
        }
    }
}