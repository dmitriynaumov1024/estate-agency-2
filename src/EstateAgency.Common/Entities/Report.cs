using System;
using System.Collections.Generic;

namespace EstateAgency.Common
{
    public class Report
    {
        public int SubmitterId { get; set; }
        public int ObjectId { get; set; }
        public ReportReasons Reason { get; set; }
        public string Description { get; set; }
        public DateTime SubmitTime { get; set; }
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
