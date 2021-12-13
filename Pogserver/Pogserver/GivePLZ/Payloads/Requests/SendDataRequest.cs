using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Pogserver.GivePLZ.Payloads.Requests
{
    class SendDataRequest : APIRequestPayloadBase
    {
        public string TypeName { get; set; }
        public override string HandleRequest(APIManager.APIContext ctx)
        {
            try
            {
                var request = JsonSerializer.Deserialize<SendDataRequest>(ctx.input);
                var type = Type.GetType("Pogserver.Database+" + request.TypeName);
                var reader = Database.Read("SELECT * FROM `" + request.TypeName + "`;");

                List<object> response = new List<object>();

                while (reader.Read())
                {
                    var instance = Activator.CreateInstance(type);
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        foreach (var prop in instance.GetType().GetProperties())
                        {
                            if (prop.Name == reader.GetName(i))
                            {
                                instance.GetType().GetProperty(prop.Name).SetValue(instance, reader.GetValue(i).ToString());
                            }
                        }
                    }
                    foreach (var proper in instance.GetType().GetProperties())
                    {
                        Console.WriteLine(proper.Name + ":" + proper.GetValue(instance));
                    }
                    Console.WriteLine(JsonSerializer.Serialize(instance, type));
                    response.Add(instance);
                }
                reader.Close();
                return JsonSerializer.Serialize(response);
            }
            catch
            {
                Console.WriteLine("Could not handle: " + ctx.input);
            }
            return "";
        }
    }
}