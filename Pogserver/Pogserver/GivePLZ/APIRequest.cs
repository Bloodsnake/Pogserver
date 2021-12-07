using Pogserver.GivePLZ.Payloads;
using System;

namespace Pogserver.GivePLZ
{
    class APIRequest : IRequest
    {
        public APIRequest(IAPIRequestPayload obj)
        {
            this.Type = IRequest.RequestType.API;
            this.RequestObject = obj;
        }
        public IRequest.RequestType Type { get; set; }
        public IAPIRequestPayload RequestObject { get; set; }
    }
}
