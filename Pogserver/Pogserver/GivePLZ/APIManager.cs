using Pogserver.GivePLZ.Payloads;
using Pogserver.GivePLZ.Payloads.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Pogserver.GivePLZ
{
    class APIManager
    {
        public static readonly string Path = "/GivePLZ/";
        public static Dictionary<string, IRequest> GetAllAPIRequests()
        {
            var requests = new Dictionary<string, IRequest>();

            var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsDefined(typeof(APIPayloadAttribute)));

            foreach (var type in types)
            {
                APIPayloadAttribute attribute = (APIPayloadAttribute)Attribute.GetCustomAttribute(type, typeof(APIPayloadAttribute));
                var inst = Activator.CreateInstance(type);
                var obj = (IAPIRequestPayload)inst;
                Console.WriteLine(attribute.Name);
                if (string.IsNullOrEmpty(attribute.GivenPath))
                {
                    requests.Add($"{Path}V{attribute.Version}/{attribute.Name}", new APIRequest(obj));
                }
                else
                {
                    requests.Add(attribute.GivenPath, new APIRequest(obj));
                }
            }
            foreach (var request in requests.Keys) Console.WriteLine(request);
            return requests;
        }
        public class APIContext
        {
            public string Input { get; set; }
            public Server Sender { get; set; }
        }
    }
}
