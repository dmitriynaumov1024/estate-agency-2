using System;

namespace EstateAgency.Common
{
    public class Account
    {
        public AccountTypes AccountType { get; set; }
        public AccountStates AccountState { get; set; }
        public string PasswordHash { get; set; }
        public DateTime RegDate { get; set; }
    } 

    public enum AccountTypes 
    {
        Unknown,
        User,
        Agent,
        Moderator,
        Root
    }

    public enum AccountStates 
    {
        Unknown,
        Normal,
        Deactivated,
        Banned,
        NotFound,
        WrongPassword
    }
}
