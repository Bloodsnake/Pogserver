using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pogserver.GivePLZ.Payloads.Requests
{
    [APIPayload("removerequest", 1)]
    class RemoveDataRequest : APIRequestPayloadBase
    {
        public string ID { get; set; }
        public string Table { get; set; }
        public string VariableName { get; set; }
        public override async Task<Response> HandleRequest(APIManager.APIContext ctx)
        {
            if (!Database.IsConfigured)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Cannot access Database");
                Console.ForegroundColor = ConsoleColor.White;
                return new Response(Response.ResponseStatus.Failed, "");
            }
            var request = JsonSerializer.Deserialize<RemoveDataRequest>(ctx.Input);
            Console.WriteLine($"DELETE FROM {request.Table} WHERE {request.VariableName} = {request.ID}");
            Database.ExecuteCommand($"DELETE FROM {request.Table} WHERE {request.VariableName} = {request.ID}");
            return new Response(Response.ResponseStatus.Sucess, "");
        }
    }
}