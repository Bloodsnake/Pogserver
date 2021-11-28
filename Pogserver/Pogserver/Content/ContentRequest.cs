namespace Pogserver
{
    class ContentRequest : IRequest
    {
        public ContentRequest(string contentPath, IRequest.HTTPType type)
        {
            this.ContentPath = contentPath;
            this.Type = IRequest.RequestType.Content;
        }
        public string ContentPath { get; set; }
        public IRequest.RequestType Type { get; set; }
    }
}