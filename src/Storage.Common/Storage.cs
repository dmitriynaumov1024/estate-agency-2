using System;
using System.Collections.Generic;

namespace Storage.Common
{
    public abstract class Storage<TKey, TValue> 
    where TValue: class
    {
        public abstract TValue Get (TKey key);
        public abstract IEnumerable<TValue> Filter (FilterInfo filter, int? limit = null);
        public abstract TKey Put (TValue value);
        public abstract TValue Replace (TKey key, TValue newValue);
        public abstract TValue Delete (TKey key);
        public abstract IEnumerable<KeyValuePair<TKey, TValue>> AsEnumerable();
        public abstract bool Clear();
    }
}
