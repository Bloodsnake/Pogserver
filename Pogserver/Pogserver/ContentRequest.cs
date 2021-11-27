namespace Pogserver
{
    class ContentRequest : IRequest
    {
        public ContentRequest(string contentPath)
        {
            this.ContentPath = contentPath;
        }
        public string ContentPath { get; set; }
        public IRequest.RequestType Type { get; set; }
    }
}