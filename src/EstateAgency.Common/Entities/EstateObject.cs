using System;

namespace EstateAgency.Common
{
    public class EstateObject
    {
        public bool IsActive { get; set; }
        public EstateObjectTypes ObjectType { get; set; }
        public int SellerId { get; set; }
        public DateTime PostDate { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        public int LocationId { get; set; }
        public int Price { get; set; }
    }

    public enum EstateObjectTypes 
    {
        Unknown,
        House,
        Flat,
        Landplot
    }
}
