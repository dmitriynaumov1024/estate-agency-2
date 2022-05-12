using System;
using System.Collections.Generic;
using System.Linq;

using Storage.Common;

namespace EstateAgency.Common
{
    public partial class EstateAgency
    {
        protected EABackend backend;

        public EstateAgency(EABackend backend)
        {
            this.backend = backend;
        }

        // Returns user id if login was successful
        public AccountInfo LogIn (string phone, string password)
        {
            Account acc = this.backend.Accounts.Get(phone);
            if (acc == null) {
                Console.Write("Account not found\n");
                return new AccountInfo {
                    PersonId = null,
                    AccountState = AccountStates.NotFound,
                    AccountType = AccountTypes.Unknown,
                    Message = "accountNotFound"
                };
            }
            else if (acc.PasswordHash == password.Hash()) {
                Console.Write("Password is correct\n");
                return new AccountInfo {
                    PersonId = acc.PersonId, 
                    AccountState = acc.AccountState,
                    AccountType = acc.AccountType
                };
            }
            else {
                Console.Write("Password is incorrect\n");
                return new AccountInfo {
                    PersonId = null, 
                    AccountState = AccountStates.WrongPassword,
                    AccountType = AccountTypes.Unknown,
                    Message = "wrongPassword"
                };
            }
        }

        public AccountInfo SignUp (Person person, string password)
        {
            if (person == null) {
                return new AccountInfo {
                    Message = "personNull"
                };
            }
            if (person.Phone == null) {
                return new AccountInfo {
                    Message = "phoneNull"
                };
            }
            if (this.backend.Accounts.Get(person.Phone) != null) {
                return new AccountInfo {
                    Message = "accountAlreadyExists"
                };
            }
            int key = this.backend.Persons.Put(person);
            this.backend.Accounts.Replace (person.Phone, new Account {
                PersonId = key,
                PasswordHash = password.Hash(),
                RegDate = DateTime.UtcNow,
                AccountType = AccountTypes.User,
                AccountState = AccountStates.Normal
            });
            return new AccountInfo {
                AccountType = AccountTypes.User,
                AccountState = AccountStates.Normal,
                PersonId = key
            };
        }

        public Person GetPerson (int id) 
        {
            return this.backend.Persons.Get(id);
        }

        public Location GetLocation (int id) 
        {
            return this.backend.Locations.Get(id);
        }

        public AggregatePersonInfo GetAggregatePerson (int id)
        {
            Person p = this.backend.Persons.Get(id);
            if (p == null) return null;
            Location l = this.backend.Locations.Get(p.LocationId);
            DateTime t = this.backend.Accounts.Get(p.Phone).RegDate;
            return new AggregatePersonInfo {
                Person = p,
                Location = l,
                RegDate = t
            };
        }

        public bool PersonCanSeeOthers (int id) 
        {
            Person p = this.backend.Persons.Get(id);
            if (p == null) return false;
            string phone = p.Phone;
            Account acc = this.backend.Accounts.Get(phone);
            if (acc == null) return false;
            AccountTypes t = acc.AccountType;
            return ((int)t >= (int)AccountTypes.Agent);
        }

        public EstateObject GetEstateObject (int id)
        {
            return this.backend.EstateObjects.Get(id);
        }

        public int GetObjectBookmarksCount (int id)
        {
            return this.backend.Bookmarks
                .Filter(Filter.Exact("ObjectId", id))
                .Count();
        }

        public IEnumerable<Bookmark> GetPersonBookmarks (int id)
        {
            return this.backend.Bookmarks
                .Filter(Filter.Exact("PersonId", id));
        }

    }
}
