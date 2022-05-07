using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using System.Linq;
using Storage.Common;
using EstateAgency.Common;

namespace EstateAgency.Backends
{
    public class FileBackend : EABackend, IDisposable
    {
        public FileBackend (string filePath) : base () 
        {
            this.file = new FileInfo(filePath);

            this.locations = new InMemoryIntKeyedStorage<Location>();
            this.persons = new InMemoryIntKeyedStorage<Person>();
            this.accounts = new InMemoryStringKeyedStorage<Account>();
            this.estateObjects = new InMemoryIntKeyedStorage<EstateObject>();
            this.clientWishes = new InMemoryIntKeyedStorage<ClientWish>();

            this.bookmarks = new InMemoryValueStorage<Bookmark>();
            this.matches = new InMemoryValueStorage<Match>();
            this.orders = new InMemoryValueStorage<Order>();
            this.reports = new InMemoryValueStorage<Report>();
        }

        protected FileInfo file;

        protected InMemoryIntKeyedStorage<Location> locations;
        protected InMemoryIntKeyedStorage<Person> persons;
        protected InMemoryStringKeyedStorage<Account> accounts;
        protected InMemoryIntKeyedStorage<EstateObject> estateObjects;
        protected InMemoryIntKeyedStorage<ClientWish> clientWishes;
        protected InMemoryValueStorage<Report> reports;
        protected InMemoryValueStorage<Bookmark> bookmarks;
        protected InMemoryValueStorage<Match> matches;
        protected InMemoryValueStorage<Order> orders;

        public override Storage<int, Location> Locations { 
            get { return this.locations; }
        }

        public override Storage<int, Person> Persons { 
            get { return this.persons; }
        }

        public override Storage<string, Account> Accounts { 
            get { return this.accounts; }
        }

        public override Storage<int, EstateObject> EstateObjects {
            get { return this.estateObjects; }
        }

        public override Storage<int, ClientWish> ClientWishes {
            get { return this.clientWishes; }
        }

        public override Storage<Report> Reports { 
            get { return this.reports; }
        }

        public override Storage<Bookmark> Bookmarks {
            get { return this.bookmarks; }
        }

        public override Storage<Match> Matches { 
            get { return this.matches; }
        }

        public override Storage<Order> Orders { 
            get { return this.orders; }
        }

        public override void Activate ()
        {
            if (!this.file.Exists) return;

            string input = this.file.OpenText().ReadToEnd();
            var be = JsonSerializer
                    .Deserialize<SerializableFileBackend> (input, jsonOptions);

            this.locations = new InMemoryIntKeyedStorage<Location>(be.Locations, be.LocationsLastKey);
            this.persons = new InMemoryIntKeyedStorage<Person>(be.Persons, be.PersonsLastKey);
            this.estateObjects = new InMemoryIntKeyedStorage<EstateObject>(be.EstateObjects, be.EstateObjectsLastKey);
            this.clientWishes = new InMemoryIntKeyedStorage<ClientWish>(be.ClientWishes, be.ClientWishesLastKey);
            this.accounts = new InMemoryStringKeyedStorage<Account>(be.Accounts);
            
            this.bookmarks = new InMemoryValueStorage<Bookmark>(be.Bookmarks);
            this.matches = new InMemoryValueStorage<Match>(be.Matches);
            this.orders = new InMemoryValueStorage<Order>(be.Orders);
            this.reports = new InMemoryValueStorage<Report>(be.Reports);
        }

        public override void Shutdown ()
        {
            var be = new SerializableFileBackend {
                LocationsLastKey = this.locations.LastKey,
                PersonsLastKey = this.persons.LastKey,
                EstateObjectsLastKey = this.estateObjects.LastKey,
                ClientWishesLastKey = this.clientWishes.LastKey,

                Locations = this.locations.StringKeyed(),
                Persons = this.persons.StringKeyed(),
                EstateObjects = this.estateObjects.StringKeyed(),
                ClientWishes = this.clientWishes.StringKeyed(),

                Accounts = this.accounts.Data,

                Bookmarks = this.bookmarks.Data.ToList(),
                Matches = this.matches.Data.ToList(),
                Orders = this.orders.Data.ToList(),
                Reports = this.reports.Data.ToList(),
            };

            string result = JsonSerializer.Serialize(be, jsonOptions);
            using (var writer = this.file.CreateText()) {
                writer.Write(result);
                writer.Flush();
            }
        }

        public void Dispose ()
        {
            this.Shutdown();
        }

        static JsonSerializerOptions jsonOptions = new JsonSerializerOptions {
            AllowTrailingCommas = true,
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        class SerializableFileBackend 
        {
            public int LocationsLastKey { get; set; }
            public int PersonsLastKey { get; set; }
            public int EstateObjectsLastKey { get; set; }
            public int ClientWishesLastKey { get; set; }

            public SortedDictionary<string, Location> Locations { get; set; }
            public SortedDictionary<string, Person> Persons { get; set; }
            public SortedDictionary<string, EstateObject> EstateObjects { get; set; }
            public SortedDictionary<string, ClientWish> ClientWishes { get; set; }
            public SortedDictionary<string, Account> Accounts { get; set; }

            public List<Bookmark> Bookmarks { get; set; }
            public List<Match> Matches { get; set; }
            public List<Order> Orders { get; set; }
            public List<Report> Reports { get; set; }
        }
    }
}
