using System;

namespace Tests
{
    public class HashTest
    {
        static string[] TestStrings = {
            "zxcvbnm567567!",
            "12345",
            "lhalfkjhfeioweafweiofaiwh",
            "password",
            "12345",
            "password",
            "Password",
            "PASSWORD",
            "00000000"
        };

        public static void Run ()
        {
            foreach (string item in TestStrings) {
                Console.Write("item: {0}\n", item);
                Console.Write("hash: {0}\n\n", item.Hash());
            }
        }
    }
}