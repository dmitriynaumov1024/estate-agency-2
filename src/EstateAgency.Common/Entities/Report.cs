using System;
using System.Collections.Generic;

namespace EstateAgency.Common
{
    public class Report : IComparable<Report>
    {
        public int SubmitterId { get; set; }
        public int ObjectId { get; set; }
        public ReportReasons Reason { get; set; }
        public string Description { get; set; }
        public DateTime SubmitTime { get; set; }

        public int CompareTo (Report other)
        {
            int personIdComparison = this.SubmitterId.CompareTo(other.SubmitterId);
            if (personIdComparison != 0) return personIdComparison;
            int objectIdComparison = this.ObjectId.CompareTo(other.ObjectId);
            return objectIdComparison;
        }
    }

    public enum ReportReasons
    {
        Unknown,
        Irrelevant,
        Explicit,
        Offensive,
        Repeating,
        Misleading
    }
}
