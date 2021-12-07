using System.Collections.Generic;
using Pogserver.Content;
using System;
using System.IO;
using System.Linq;
using MySql.Data.MySqlClient;

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

            Console.WriteLine("Loading common Files");

            foreach (var file in Directory.GetFiles(Environment.CurrentDirectory + "/Content/Common"))
            {
                var c = @"\".ToCharArray()[0];
                var path = file.Replace(Environment.CurrentDirectory + "/Content", "").Replace(c, '/');
                gets.Add(path, new ContentRequest(path));
            }

            var posts = new Dictionary<string, IRequest>();
            posts.Add("/GivePLZ/V1/shutdown", new GivePLZ.APIRequest(new GivePLZ.Requests.ShutDownRequest()));
            posts.Add("/GivePLZ/V1/generateNewData", new GivePLZ.APIRequest(new GivePLZ.Requests.GenerateNewDataRequest()));

            requests.Add("GET", gets);
            requests.Add("POST", posts);

            var server = new Server();

            server.AddContentLocation("Content/");
            server.AddRequestLists(requests);

            foreach (var requestmethods in requests.Values.ToList())
            {
                foreach (var request in requestmethods.Keys.ToList())
                {
                    Console.WriteLine("Loaded: " + request);
                }
            }
            Console.WriteLine("Configuring Database...");
            Database.Configure();
            Console.WriteLine("Database configured");

            server.Run().GetAwaiter().GetResult();
        }
    }
}