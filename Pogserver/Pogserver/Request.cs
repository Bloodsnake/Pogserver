namespace Pogserver
{
    interface IRequest
    {
        public enum RequestType { Page, API }

        public RequestType Type { get; set; }
    }
}
