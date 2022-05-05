using System;
using Storage.Common;

namespace EstateAgency.Common
{
    public class EAMigration 
    {
        protected EABackend source, target;

        public EAMigration (EABackend source, EABackend target)
        {
            if (source == target) {
                throw new Exception 
                    ("Source and target backends should not be equal.");
            }
            if (source == null || target == null) {
                throw new NullReferenceException 
                    ("Source and target backends should not be null.");
            }
        }

        public void Migrate ()
        {
            Migrate(source.Locations, target.Locations);
            Migrate(source.Persons, target.Persons);
            Migrate(source.Accounts, target.Accounts);
            Migrate(source.EstateObjects, target.EstateObjects);
            Migrate(source.ClientWishes, target.ClientWishes);
            Migrate(source.Bookmarks, target.Bookmarks);
            Migrate(source.Matches, target.Matches);
        }

        private void Migrate<TKey, TValue> (
                Storage<TKey, TValue> sSource, 
                Storage<TKey, TValue> sTarget ) 
            where TKey: IComparable<TKey>
            where TValue: class
        {
            sTarget.Clear();
            foreach (var kvPair in sSource.AsEnumerable()) {
                sTarget.Replace(kvPair.Key, kvPair.Value);
            }
        }

        private void Migrate<TValue> (
                Storage<TValue> sSource, 
                Storage<TValue> sTarget ) 
            where TValue: class, IComparable<TValue>
        {
            sTarget.Clear();
            foreach (var item in sSource.AsEnumerable()) {
                sTarget.Put(item);
            }
        }
    }
}
