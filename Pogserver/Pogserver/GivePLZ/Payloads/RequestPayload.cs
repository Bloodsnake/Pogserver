using System;

namespace Pogserver.GivePLZ.Payloads
{
    interface IAPIRequestPayload
    {
        public string Token { get; set; }

        public string HandleRequest(APIManager.APIContext ctx);
    }
    class APIRequestPayloadBase : IAPIRequestPayload
    {
        public string Token { get; set; }

        public virtual string HandleRequest(APIManager.APIContext ctx)
        {
            throw new RequestHandlerNotImplementedException();
        }
        public class RequestHandlerNotImplementedException : Exception
        {
            public RequestHandlerNotImplementedException()
        : base("No Request Handler got implemented")
            {
            }
        }
    }
}
