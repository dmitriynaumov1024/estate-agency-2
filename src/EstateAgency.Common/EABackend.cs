using System;
using Storage.Common;

namespace EstateAgency.Common
{
    public abstract class EABackend
    {
        public abstract Storage<int, Location> Locations { get; }
        public abstract Storage<int, Person> Persons { get; }
        public abstract Storage<string, Account> Accounts { get; }
        public abstract Storage<int, EstateObject> EstateObjects { get; }
        public abstract Storage<int, ClientWish> ClientWishes { get; }
        public abstract Storage<Report> Reports { get; }
        public abstract Storage<Bookmark> Bookmarks { get; }
        public abstract Storage<Match> Matches { get; }
        public abstract Storage<Order> Orders { get; }

        public abstract void Activate();
        public abstract void Shutdown();
    }
}
