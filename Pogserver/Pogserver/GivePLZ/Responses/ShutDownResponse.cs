using Pogserver.GivePLZ.Payloads;

namespace Pogserver.GivePLZ.Responses
{
    class ShutDownResponse : IAPIResponsePayload
    {
        public bool RequestHandled { get; set; }
    }
}
