using System;

namespace EstateAgency.Common
{
    public class AccountInfo
    {
        public int? PersonId { get; set; }
        public AccountTypes AccountType { get; set; }
        public AccountStates AccountState { get; set; }
        public string Message { get; set; }
    }
}
