using System;
using System.IO;
using System.Text.Json;

namespace Tests
{
    public class SimpleTest
    {
        public static void Run ()
        {
            string rawData = File.ReadAllText("./test.json");
            Console.Write("Raw data: \n{0}\n", rawData);
            object c = JsonSerializer.Deserialize<SimpleClass>(rawData);
            Console.Write("Deserialized object: \n{0}\n", c);
            
        }
    }

    public class SimpleClass {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return String.Format (
                "I am {0} {1}, my e-mail is {2}.", 
                this.Name, this.Surname, this.Email 
            );
        }
    }
}
