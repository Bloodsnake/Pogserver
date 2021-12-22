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

            var request = JsonSerializer.Deserialize<ReceiveNewDataRequest>(ctx.input);

            var type = Type.GetType("Pogserver.Database+" + request.TypeName);
            string vars = "";
            string vals = "";

            var data = JsonSerializer.Deserialize(request.Data, type);
            int i = 1;
            foreach (var prop in type.GetProperties()) {
                if (i == type.GetProperties().Length) vars += prop.Name;
                else vars += (prop.Name + ", ");
                Console.WriteLine(i);
                Console.WriteLine(type.GetProperties().Length);
                if (prop.Name.Contains("ID")) vals += prop.GetValue(data);
                else vals += ("'" + prop.GetValue(data)+ "', ");
                i++;
            }
            
            Database.ExecuteCommand($"INSERT INTO {request.TypeName} (" + vars + ") VALUES (" + vals + ");");

            return "";
        }
    }
}
