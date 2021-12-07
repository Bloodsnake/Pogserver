﻿using System.Collections.Generic;
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

            requests.Add("GET", ContentManager.GetAllContentRequests());
            requests.Add("POST", GivePLZ.APIManager.GetAllAPIRequests());

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