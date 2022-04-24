using System;
using System.Collections.Generic;

namespace Storage.Common
{
    public abstract class Storage<TKey, TValue> 
    where TValue: class
    {
        public abstract TValue Get (TKey key);
        public abstract TKey Put (TValue value);
        public abstract TValue Replace (TKey key, TValue newValue);
        public abstract TValue Delete (TKey key);
        public abstract IDictionary<TKey, TValue> AsDictionary();
    }
}
