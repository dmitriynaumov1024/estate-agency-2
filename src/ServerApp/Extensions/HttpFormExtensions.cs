using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace ServerApp
{
    public static class HttpFormExtensions
    {
        public static string GetString (this IFormCollection query, string key)
        {
            var stringVals = query[key];
            if (stringVals.Count == 0) {
                return null;
            }
            else {
                return stringVals[0];
            }
        }

        public static int? GetInt32 (this IFormCollection query, string key)
        {
            var stringVals = query[key];
            if (stringVals.Count == 0) {
                return null;
            }
            else {
                int result = 0;
                if (int.TryParse(stringVals[0], out result)) {
                    return result;
                }
                else {
                    return null;
                }
            }
        }
    }
}