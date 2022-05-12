using System;
using System.Collections.Generic;

namespace Tests
{
    public class Program
    {
        static readonly string 
            HELP_STRING = "Usage: <test-name>\n";

        static Dictionary<string, Action> KnownTests = 
            new Dictionary<string, Action> {
            { "simple", SimpleTest.Run },
            { "dbtest", DbTest.Run },
            { "dbgen", DbGenerator.Run },
            { "hash", HashTest.Run },
            { "filters", FilterTest.Run },
            { "filter2", FilterTest.Run2 },
        };

        public static void Main(string[] args)
        {
            if (args.Length < 1) {
                Console.Write(HELP_STRING);
            }

            string testId = args[0].ToLower();
            Action testAction = null;

            if (KnownTests.TryGetValue(testId, out testAction)) {
                testAction();
            }
            else {
                Console.Write("Known test IDs: \n{0}\n", 
                              String.Join("\n", KnownTests.Keys));
            }
        }
    }
}
