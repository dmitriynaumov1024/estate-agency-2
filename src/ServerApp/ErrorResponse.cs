using System;

namespace ServerApp
{
    public class ErrorResponse
    {
        public string Message { get; set; }

        public ErrorResponse () { }

        public ErrorResponse (string message)
        {
            this.Message = message;
        }
    }
}
