using System;

namespace EstateAgency.Common
{
    public class Order : IComparable<Order>
    {
        public int PersonId { get; set; }
        public int ObjectId { get; set; }
        public DateTime OrderTime { get; set; }
        public int AssignedAgentId { get; set; }

        public int CompareTo (Order other)
        {
            int personIdComparison = this.PersonId.CompareTo(other.PersonId);
            if (personIdComparison != 0) return personIdComparison;
            int objectIdComparison = this.ObjectId.CompareTo(other.ObjectId);
            return objectIdComparison;
        }
    }
}