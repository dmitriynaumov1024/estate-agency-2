using System;
using System.Collections.Generic;
using System.Linq;
using Storage.Common;

namespace EstateAgency.Backends
{
    public class InMemoryKeyValueStorage<TKey, TValue> : Storage<TKey, TValue>
    where TKey: IComparable<TKey>
    where TValue: class
    {
        public SortedDictionary<TKey, TValue> Data { get; protected set; }

        public InMemoryKeyValueStorage () 
        { 
            this.Data = new SortedDictionary<TKey, TValue>();
        }

        public InMemoryKeyValueStorage (IDictionary<TKey, TValue> source) : this ()
        {
            this.Data = new SortedDictionary<TKey, TValue>();
            foreach (var kvPair in source) {
                this.Data[kvPair.Key] = kvPair.Value;
            }
        }

        public override TValue Get (TKey key)
        {
            TValue result = null;
            this.Data.TryGetValue(key, out result);
            return result;
        }

        public override TKey Put (TValue value) 
        {
            return default(TKey);
        }

        public override IEnumerable<KeyValuePair<TKey, TValue>> Filter (FilterInfo filter, int? limit = null)
        {
            // to be implemented
            var result = this.Data.Where(kvPair => FilterExtensions.Apply(filter, kvPair.Value));
            if (limit != null) {
                result = result.Take((int)limit);
            }
            return result;
        }

        public override TValue Replace (TKey key, TValue newValue)
        {
            TValue oldValue = null;
            this.Data.TryGetValue(key, out oldValue);
            this.Data[key] = newValue;
            return oldValue;
        }

        public override TValue Delete (TKey key)
        {
            TValue oldValue = null;
            this.Data.TryGetValue(key, out oldValue);
            this.Data.Remove(key);
            return oldValue;
        }

        public override IEnumerable<KeyValuePair<TKey, TValue>> AsEnumerable ()
        {
            return this.Data;
        }

        public override void PutMany (IEnumerable<KeyValuePair<TKey, TValue>> values)
        {
            foreach (var kvPair in values) {
                this.Data[kvPair.Key] = kvPair.Value; 
            }
        }

        public override bool Clear ()
        {
            this.Data = new SortedDictionary<TKey, TValue>();
            return true;
        }
    }
}
