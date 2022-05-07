using System;
using System.Collections.Generic;

namespace Storage.Common
{
    public abstract class Storage<TKey, TValue> 
    where TKey: IComparable<TKey>
    where TValue: class
    {
        public abstract TValue Get (TKey key);
        public abstract IEnumerable<KeyValuePair<TKey, TValue>> Filter (FilterInfo filter, int? limit = null);
        public abstract TKey Put (TValue value);
        public abstract TValue Replace (TKey key, TValue newValue);
        public abstract TValue Delete (TKey key);
        public abstract IEnumerable<KeyValuePair<TKey, TValue>> AsEnumerable();
        public abstract void PutMany(IEnumerable<KeyValuePair<TKey, TValue>> values);
        public abstract bool Clear();
    }

    public abstract class Storage<TValue> 
    where TValue: class, IComparable<TValue>
    {
        public abstract bool Put(TValue value);
        public abstract IEnumerable<TValue> Filter (FilterInfo filter, int? limit = null);
        public abstract bool Contains(TValue value);
        public abstract bool Delete(TValue value);
        public abstract IEnumerable<TValue> AsEnumerable();
        public abstract void PutMany(IEnumerable<TValue> values);
        public abstract bool Clear();
    }
}
