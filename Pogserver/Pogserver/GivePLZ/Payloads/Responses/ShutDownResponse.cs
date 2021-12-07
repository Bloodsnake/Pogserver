using Pogserver.GivePLZ.Payloads;

namespace Pogserver.GivePLZ.Payloads.Responses
{
    class ShutDownResponse : IAPIResponsePayload
    {
        public bool RequestHandled { get; set; }
    }
}
