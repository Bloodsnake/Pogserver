﻿namespace Pogserver.Content
{
    class ContentRequest : IRequest
    {
        public ContentRequest(string contentPath)
        {
            this.ContentPath = contentPath;
            this.Type = IRequest.RequestType.Content;
        }
        public enum ContentType { HTML, JS, CSS}
        //public ContentType ExtensionType { get; set; }
        public string ContentPath { get; set; }
        public IRequest.RequestType Type { get; set; }
    }
}