using System;

namespace Pogserver.API.Payloads
{
    interface IAPIRequestPayload
    {
        public string Token { get; set; }

        public string HandleRequest(string input);
    }
    class APIRequestPayloadBase : IAPIRequestPayload
    {
        public string Token { get; set; }

        public virtual string HandleRequest(string input)
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
