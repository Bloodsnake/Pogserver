using System;
using System.Text.Json;

namespace Pogserver.API.Requests
{
    class ShutDownRequest : APIObjectBase
    {
        public override void HandleRequest(string input)
        {
            if (string.IsNullOrEmpty(input)) return;
            try
            {
                var request = JsonSerializer.Deserialize<ShutDownRequest>(input);
                Console.WriteLine(request.Token);
            }
            catch
            {
                Console.WriteLine("Could not parse: " + input);
            }
        }
    }
}