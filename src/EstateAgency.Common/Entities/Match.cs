using System;

namespace EstateAgency.Common
{
    public class Match : IComparable<Match>
    {
        public int PersonId { get; set; }
        public int ObjectId { get; set; }
        public DateTime MatchTime { get; set; }

        public int CompareTo (Match other)
        {
            int personIdComparison = this.PersonId.CompareTo(other.PersonId);
            if (personIdComparison != 0) return personIdComparison;
            int objectIdComparison = this.ObjectId.CompareTo(other.ObjectId);
            return objectIdComparison;
        }
    }
}
