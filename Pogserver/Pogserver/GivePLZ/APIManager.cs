using Pogserver.GivePLZ.Payloads.Requests;
using System.Collections.Generic;

namespace Pogserver.GivePLZ
{
    class APIManager
    {
        public static readonly string Path = "/GivePLZ/V1/";
        public static Dictionary<string, IRequest> GetAllAPIRequests()
        {
            var requests = new Dictionary<string, IRequest>();
            requests.Add(Path + "shutdown", new APIRequest(new ShutDownRequest()));
            requests.Add(Path + "generateNewData", new APIRequest(new GenerateNewDataRequest()));
            return requests;
        }
        public class APIContext
        {
            public string input { get; set; }
            public Server sender { get; set; }
        }
    }
}
