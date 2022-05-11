using System;
using System.IO;
using EstateAgency.Common;
using EstateAgency.Backends;

namespace Tests
{
    public class DbTest
    {
        public static void Run ()
        {
            string fPath = "./database.json";
            string fPath2 = "./result.json";
            EABackend backend = new FileBackend(fPath);
            backend.Activate();

            /*
            foreach (var kvPair in backend.Locations.AsEnumerable()) {
                Console.Write("- key : {0}\n", kvPair.Key);
                Console.Write("  Region : {0}\n", kvPair.Value.Region);
                Console.Write("  Town : {0}\n", kvPair.Value.Town);
                Console.Write("  District : {0}\n\n", kvPair.Value.District);
            }
            */

            /*
            backend.Persons.Put (new Person {
                Name = "Alice",
                Surname = "Benner",
                Phone = "+4-566-901-2200",
                Email = "abenner2002@icloud.com",
                LocationId = 1
            });

            backend.Reports.Put (new Report {
                SubmitterId = 1,
                ObjectId = 0,
                Reason = ReportReasons.Irrelevant,
                Description = "irrelevant",
                SubmitTime = DateTime.UtcNow
            });
            */

            EABackend backend2 = new FileBackend(fPath2);
            backend2.Activate();

            new EAMigration(backend, backend2).Migrate();

            backend.Shutdown();
            backend2.Shutdown();
        }
    }
}