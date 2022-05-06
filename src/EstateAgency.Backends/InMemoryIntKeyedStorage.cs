using System;
using System.Collections.Generic;
using Storage.Common;

namespace EstateAgency.Backends
{
    public class InMemoryIntKeyedStorage<TValue> : Storage<int, TValue>
    where TValue: class
    {
        public SortedDictionary<int, TValue> Data { get; protected set; }
        protected int LastKey { get; set; }

        public InMemoryIntKeyedStorage () { }

        public InMemoryIntKeyedStorage (IDictionary<int, TValue> source, int lastKey)
        {
            this.Data = new SortedDictionary<int, TValue>(source);
            this.LastKey = lastKey;
        }

        public override TValue Get (int key)
        {
            TValue result = null;
            this.Data.TryGetValue(key, out result);
            return result;
        }

        public override IEnumerable<KeyValuePair<int, TValue>> Filter (FilterInfo filter, int? limit = null)
        {
            // to be implemented
            return null;
        }

        public override int Put (TValue value)
        {
            this.LastKey++;
            this.Data[this.LastKey] = value;
            return this.LastKey;
        }

        public override TValue Replace (int key, TValue newValue)
        {
            TValue oldValue = null;
            if (this.Data.TryGetValue(key, out oldValue)) {
                this.Data[key] = newValue;
            }
            return oldValue;
        }

        public override TValue Delete (int key)
        {
            TValue oldValue = null;
            this.Data.TryGetValue(key, out oldValue);
            this.Data.Remove(key);
            return oldValue;
        }

        public override IEnumerable<KeyValuePair<int, TValue>> AsEnumerable ()
        {
            return this.Data;
        }

        public override bool Clear ()
        {
            this.Data = new SortedDictionary<int, TValue>();
            this.LastKey = 0;
            return true;
        }
    }
}
