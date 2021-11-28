using System;

namespace Pogserver.API
{
    interface IAPIObject
    {
        public string Token { get; set; }

        public void HandleRequest(string input);
    }
    class APIObjectBase : IAPIObject
    {
        public string Token { get; set; }

        public virtual void HandleRequest(string input)
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
