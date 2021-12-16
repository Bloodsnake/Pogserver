using System;
using System.Text.Json;

namespace Pogserver.GivePLZ.Payloads.Requests
{
    class ReceiveNewDataRequest : APIRequestPayloadBase
    {
        public string TypeName { get; set; }
        public string Data { get; set; }
        public override string HandleRequest(APIManager.APIContext ctx)
        {
            if (!Database.IsConfigured)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Cannot access Database");
                Console.ForegroundColor = ConsoleColor.White;
                return "";
            }
            Console.WriteLine(ctx.input);
            var request = JsonSerializer.Deserialize<ReceiveNewDataRequest>(ctx.input);
            Console.WriteLine(request.TypeName);
            Console.WriteLine(request.Data);
            var type = Type.GetType("Pogserver.Database+" + request.TypeName);
            string vars = "";
            string vals = "";
            Console.WriteLine(request.Data);
            Console.WriteLine(type.Name);
            var data = JsonSerializer.Deserialize(request.Data, type);
            foreach (var prop in type.GetProperties()) {
                vars += (prop.Name + ", ");
                Console.WriteLine(prop.Name);
                vals += ("'" + prop.GetValue(data)+ "', ");
            }
            Console.WriteLine("INSERT INTO '" + request.TypeName + "' (" + vars + ") VAUES (" + vals + ");");
            Database.ExecuteCommand("INSERT INTO '" + request.TypeName + "' (" + vars + ") VAUES (" + vals + ");");
            return "";
        }
    }
}
