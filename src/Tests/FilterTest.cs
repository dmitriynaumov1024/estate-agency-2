using System;
using System.IO;
using EstateAgency.Common;
using EstateAgency.Backends;
using Storage.Common;

namespace Tests
{
    public class FilterTest 
    {
        public static void Run ()
        {
            string fPath = "./database.g.json";
            EABackend backend = new FileBackend(fPath);
            backend.Activate();

            var filter = Filter.Range("LocationId", 8, 9);

            var d = backend.Persons.Filter(filter);

            foreach (var kvPair in d) {
                Console.Write("- key : {0}\n", kvPair.Key);
                Console.Write("  Surname : {0}\n", kvPair.Value.Surname);
                Console.Write("  Name : {0}\n", kvPair.Value.Name);
                Console.Write("  Phone : {0}\n\n", kvPair.Value.Phone);
                Console.Write("  LocationId : {0}\n\n", kvPair.Value.LocationId);
            }
        }

        public static void Run2 ()
        {
            var filter = Filter.Range("X", 2.0, 4.0)
                .And(Filter.Range("Y", 2.0, 3.0));

            var entities = new InMemoryValueStorage<TestEntity>();
            entities.PutMany ( new[] {
                new TestEntity { X = 2.0, Y = 3.0 },
                new TestEntity { X = 2.3, Y = 2.8 },
                new TestEntity { X = 4.5, Y = 2.0 },
                new TestEntity { X = 3.2, Y = 2.3 },
                new TestEntity { X = -1.0, Y = 3.0 },
                new TestEntity { X = 2.4, Y = 2.9 }
            });
            foreach (var entity in entities.Filter(filter)) {
                Console.Write("Entity (X = {0:F3}, Y = {1:F3})\n", entity.X, entity.Y);
            }
        }

        class TestEntity : IComparable<TestEntity>
        {
            public double X { get; set; }
            public double Y { get; set; }

            public int CompareTo (TestEntity other) {
                int xComparison = this.X.CompareTo(other.X);
                if (xComparison != 0) return xComparison;
                int yComparison = this.Y.CompareTo(other.Y);
                return yComparison;
            }
        }
    }
}
