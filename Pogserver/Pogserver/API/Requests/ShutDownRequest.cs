using System;
using Pogserver.API.Payloads;
using System.Text.Json;
using Pogserver.API.Responses;

namespace Pogserver.API.Requests
{
    class ShutDownRequest : APIRequestPayloadBase
    {
        public override string HandleRequest(string input)
        {
            var resp = new ShutDownResponse();
            resp.RequestHandled = false; ;
            if (string.IsNullOrEmpty(input)) return JsonSerializer.Serialize<ShutDownResponse>(resp);
            try
            {
                var request = JsonSerializer.Deserialize<ShutDownRequest>(input);
                resp.RequestHandled = true;
            }
            catch
            {
                Console.WriteLine("Could not parse: " + input);
            }
            return JsonSerializer.Serialize<ShutDownResponse>(resp);
        }
    }
}