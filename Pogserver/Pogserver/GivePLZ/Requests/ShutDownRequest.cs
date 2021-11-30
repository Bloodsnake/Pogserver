using System;
using Pogserver.GivePLZ.Payloads;
using System.Text.Json;
using Pogserver.GivePLZ.Responses;

namespace Pogserver.GivePLZ.Requests
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
                Console.WriteLine(request.Token);
            }
            catch
            {
                Console.WriteLine("Could not parse: " + input);
            }
            return JsonSerializer.Serialize<ShutDownResponse>(resp);
        }
    }
}