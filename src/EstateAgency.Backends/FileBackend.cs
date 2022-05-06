using System;
using System.IO;
using Storage.Common;
using EstateAgency.Common;

namespace EstateAgency.Backends
{
    public class FileBackend : EABackend, IDisposable
    {
        protected FileInfo file;

        protected Storage<int, Location> locations;
        protected Storage<int, Person> persons;
        protected Storage<string, Account> accounts;
        protected Storage<int, EstateObject> estateObjects;
        protected Storage<int, ClientWish> clientWishes;
        protected Storage<int, Report> reports;
        protected Storage<Bookmark> bookmarks;
        protected Storage<Match> matches;
        protected Storage<Order> orders;

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

        public override Storage<int, Report> Reports { 
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

        public void SaveAll ()
        {

        }

        public void Dispose ()
        {
            this.SaveAll();
        }
    }
}
