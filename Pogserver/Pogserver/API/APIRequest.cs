namespace Pogserver.API
{
    class APIRequest : IRequest
    {
        public string Path { get; set; }
        public IRequest.RequestType Type { get; set; }
        public IAPIObject ContentPath { get; set; }

    }

    interface IAPIObject
    {
        public uint Token { get; set; }

        public bool IsTokenValid()
        {
            return true;
        }
    }
    class APIObjectBase : IAPIObject
    {
        public uint Token { get; set; }

        public bool IsTokenValid()
        {
            return true;
        }
    }
}
