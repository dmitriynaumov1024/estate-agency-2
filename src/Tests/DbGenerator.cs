using System;
using System.IO;
using EstateAgency.Common;
using EstateAgency.Backends;

namespace Tests
{
    public class DbGenerator
    {
        static AdvancedRandom R = new AdvancedRandom();

        public static void Run ()
        {
            string fPath = "./database.generated.json";
            EABackend backend = new FileBackend(fPath);
            backend.Activate();

            for (int i=0; i<200; i++) {
                backend.Persons.Put (CreatePerson());
            }

            backend.Shutdown();
        }

        static Person CreatePerson()
        {
            string name = R.NextOf(knownNames);
            string surname = R.NextOf(knownSurnames);
            string email = String.Format ( "{0}{1}{2}@nmail.com", 
                name.ToLower(), surname.ToLower(), R.Next(10,99) 
            );
            string phone = String.Format ( "{0:D3}-{1:D3}-{2:D4}", 
                R.Next(50, 100), R.Next(1, 1000), R.Next(0, 10000)
            );

            return new Person {
                Name = name,
                Surname = surname,
                Phone = phone,
                Email = email,
                LocationId = 1
            };
        }

        static string[] knownNames = {
            "Alice", "Anna", "Angelina", "Alicia", "Catherine", "Elizabeth",
            "Harmony", "Kate", "Lia", "Olivia", "Julia", "Susan", "Scarlett",
            "John", "Bob", "Mark", "Chris", "Evan", "Thomas", "Eugene",
            "Daniel", "Paul", "Sebastian", "Nicolas", "Steven", "Ronald", 
            "Gary", "Robert", "Eric", "Austin", "Peter", "William", "Oscar"
        };

        static string[] knownSurnames = {
            "Miller", "Juhanson", "Peters", "Jansson", "Parker", "Harper",
            "Fury", "Stalberg", "Williams", "Johnson", "Smith", "Murphy",
            "Willis", "Porter", "Robinson", "Reeves"
        };
    }
}
