using System;

namespace EstateAgency.Common
{
    public class Bookmark : IComparable<Bookmark>
    {
        public int PersonId { get; set; }
        public int ObjectId { get; set; }

        public int CompareTo (Bookmark other)
        {
            int personIdComparison = this.PersonId.CompareTo(other.PersonId);
            if (personIdComparison != 0) return personIdComparison;
            int objectIdComparison = this.ObjectId.CompareTo(other.ObjectId);
            return objectIdComparison;
        }
    }
}
