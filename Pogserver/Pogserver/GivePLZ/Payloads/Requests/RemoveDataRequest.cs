using System;
using System.Text.Json;

namespace Pogserver.GivePLZ.Payloads.Requests
{
    class RemoveDataRequest : APIRequestPayloadBase
    {
        public string ID { get; set; }
        public string Table { get; set; }
        public string VariableName { get; set; }
        public override string HandleRequest(APIManager.APIContext ctx)
        {
            if (!Database.IsConfigured)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Cannot access Database");
                Console.ForegroundColor = ConsoleColor.White;
                return "";
            }
            var request = JsonSerializer.Deserialize<RemoveDataRequest>(ctx.input);
            Console.WriteLine(request.ID);
            Console.WriteLine(request.Table);
            Console.WriteLine(request.VariableName);
            Console.WriteLine("DELETE FROM " + request.Table + " WHERE " + request.VariableName + " = " + request.ID);
            Database.ExecuteCommand("DELETE FROM " + request.Table + " WHERE " + request.VariableName + " = " + request.ID);
            return "";
        }
    }
}