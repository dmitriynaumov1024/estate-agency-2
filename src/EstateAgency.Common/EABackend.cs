using System;
using Storage.Common;

namespace EstateAgency.Common
{
    public abstract class EABackend
    {
        public abstract Storage<int, Location> Locations { get; }
        public abstract Storage<int, Person> Persons { get; }
        // public abstract Storage<string, Credential> Credentials { get; }
        // public abstract Storage<int, EstateObject> EstateObjects { get; }
        // public abstract Storage<Tuple<int, int>, Bookmark> Bookmarks { get; }
        // public abstract Storage<Tuple<int, int>, Match> Matches { get; }
    }
}
