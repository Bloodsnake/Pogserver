﻿namespace Pogserver.API
{
    class APIContext
    {
        public static void Update(Server server)
        {
            Server = server;
        }
        public static Server Server { get; set; }
    }
}