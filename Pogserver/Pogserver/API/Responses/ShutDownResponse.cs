using Pogserver.API.Payloads;

namespace Pogserver.API.Responses
{
    class ShutDownResponse : IAPIResponsePayload
    {
        public bool RequestHandled { get; set; }
    }
}
