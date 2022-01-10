using System;
using System.Threading.Tasks;

namespace Pogserver.GivePLZ.Payloads
{
    interface IAPIRequestPayload
    {
        public Task<Response> HandleRequest(APIManager.APIContext ctx);
    }
    class APIRequestPayloadBase : IAPIRequestPayload
    {
        public virtual Task<Response> HandleRequest(APIManager.APIContext ctx)
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
