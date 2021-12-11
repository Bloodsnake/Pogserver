using System;
using System.Text.Json;

namespace Pogserver.GivePLZ.Payloads.Requests
{
    class SendDataRequest : APIRequestPayloadBase
    {
        public string TypeName { get; set; }
        public override string HandleRequest(APIManager.APIContext ctx)
        {
            string resp = "";
            
            var request = JsonSerializer.Deserialize<SendDataRequest>(ctx.input);
            var type = Type.GetType("Pogserver.Database+" + request.TypeName);

            var reader = Database.Read("SELECT * FROM `" + request.TypeName + "`");

            while (reader.Read())
            {
                var instance = Activator.CreateInstance(type);
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    foreach (var prop in instance.GetType().GetProperties())
                    {
                        if (prop.Name == reader.GetName(i))
                        {
                            instance.GetType().GetProperty(prop.Name).SetValue(instance, reader.GetValue(i));
                        }
                    }
                }
                var s = JsonSerializer.Serialize(instance, type);
                resp += s;
            }
            Console.WriteLine(resp);
            return resp;
        }
    }
}