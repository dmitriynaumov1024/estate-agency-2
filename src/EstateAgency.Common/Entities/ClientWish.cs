using System;

namespace EstateAgency.Common
{
    public class ClientWish
    {
        public EstateObjectTypes ObjectType { get; set; }
        public int UserId { get; set; }
        public DateTime PostDate { get; set; }
        public bool IsOpen { get; set; }
        public string Description { get; set; }
    }
}
