using System;
using System.Collections.Generic;
using Storage.Common;

namespace EstateAgency.Backends
{
    public class InMemoryStringKeyedStorage<TValue> : Storage<string, TValue>
    where TValue: class
    {
        public SortedDictionary<string, TValue> Data { get; protected set; }

        public InMemoryStringKeyedStorage () 
        {
            this.Data = new SortedDictionary<string, TValue>();
        }

        public InMemoryStringKeyedStorage (IDictionary<string, TValue> source)
        {
            this.Data = new SortedDictionary<string, TValue>(source);
        }

        public override TValue Get (string key)
        {
            TValue result = null;
            this.Data.TryGetValue(key, out result);
            return result;
        }

        public override IEnumerable<KeyValuePair<string, TValue>> Filter (FilterInfo filter, int? limit = null)
        {
            // to be implemented
            return null;
        }

        public override string Put (TValue value)
        {
            return null;
        }

        public override TValue Replace (string key, TValue newValue)
        {
            TValue oldValue = null;
            this.Data.TryGetValue(key, out oldValue);
            this.Data[key] = newValue;
            return oldValue;
        }

        public override TValue Delete (string key)
        {
            TValue oldValue = null;
            this.Data.TryGetValue(key, out oldValue);
            this.Data.Remove(key);
            return oldValue;
        }

        public override IEnumerable<KeyValuePair<string, TValue>> AsEnumerable ()
        {
            return this.Data;
        }

        public override void PutMany (IEnumerable<KeyValuePair<string, TValue>> values)
        {
            foreach (var kvPair in values) {
                this.Data[kvPair.Key] = kvPair.Value; 
            }
        }

        public override bool Clear ()
        {
            this.Data = new SortedDictionary<string, TValue>();
            return true;
        }

    }
}
