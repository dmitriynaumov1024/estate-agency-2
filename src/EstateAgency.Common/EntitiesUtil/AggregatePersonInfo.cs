using System;

namespace EstateAgency.Common
{
    public class AggregatePersonInfo
    {
        public Person Person { get; set; }
        public Location Location { get; set; }
        public DateTime RegDate { get; set; }
    }
}
