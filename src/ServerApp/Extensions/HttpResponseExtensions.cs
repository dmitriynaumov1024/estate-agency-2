using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace ServerApp
{
    public static class HttpResponseExtensions
    {
        public static void IsPlainText (this HttpResponse response)
        {
            response.Headers["Content-Type"] = "text/plain; charset=utf-8";
        }

        public static void IsJson (this HttpResponse response)
        {
            response.Headers["Content-Type"] = "application/json; charset=utf-8";
        }
    }
}