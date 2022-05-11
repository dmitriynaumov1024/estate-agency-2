using System;
using System.Collections.Generic;
using System.Linq;

namespace ServerApp
{
    public static class StringExtensions
    {
        public static string Hash (this string source)
        {
            int[] h = new int[] {
                513, 511, 71, 8273, 349, 2219
            };

            for (int i=0; i<source.Length; i++) {
                h[i % h.Length] <<= 11;
                h[i % h.Length] ^= (int)(source[i]);
            }

            return String.Join("", h.Select (number => number.ToString("x8")));
        }
    }
}
