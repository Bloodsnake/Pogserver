namespace Pogserver.GivePLZ.Payloads
{
    class Response
    {
        public ResponseStatus Status { get; set; }
        public string Result { get; set; }
        public enum ResponseStatus { Failed = 0, Sucess = 1}
        public Response(ResponseStatus status, string message)
        {
            this.Status = status;
            this.Result = message;
        }
    }
}
