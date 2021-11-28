namespace Pogserver
{
    interface IRequest
    {
        public enum RequestType { Content, API }

        public enum HTTPType { GET, POST}

        public RequestType Type { get; set; }
    }
}
