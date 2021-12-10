using Pogserver.GivePLZ.Payloads.Responses;
using System;
using System.Text.Json;

namespace Pogserver.GivePLZ.Payloads.Requests
{
    class ShutDownRequest : APIRequestPayloadBase
    {
        public override string HandleRequest(APIManager.APIContext ctx)
        {
            var resp = new ShutDownResponse();
            resp.RequestHandled = false;
            if (string.IsNullOrEmpty(ctx.input)) return JsonSerializer.Serialize<ShutDownResponse>(resp);
            try
            {
                Console.WriteLine(ctx.input);
                var request = JsonSerializer.Deserialize<ShutDownRequest>(ctx.input);
                resp.RequestHandled = true;
                Console.WriteLine(request.Token);
            }
            catch
            {
                Console.WriteLine("Could not parse: " + ctx.input);
            }
            return JsonSerializer.Serialize<ShutDownResponse>(resp);
        }
    }
}