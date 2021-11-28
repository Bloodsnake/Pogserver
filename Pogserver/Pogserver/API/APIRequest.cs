namespace Pogserver.API
{
    class APIRequest : IRequest
    {
        public APIRequest(IAPIObject obj, IRequest.HTTPType type)
        {
            this.Type = IRequest.RequestType.API;
            this.RequestObject = obj;
        }
        public IRequest.RequestType Type { get; set; }
        public IAPIObject RequestObject { get; set; }
    }
}
