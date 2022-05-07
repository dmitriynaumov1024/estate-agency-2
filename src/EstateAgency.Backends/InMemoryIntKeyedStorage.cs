using System;
using System.Collections.Generic;
using Storage.Common;

namespace EstateAgency.Backends
{
    public class InMemoryIntKeyedStorage<TValue> : Storage<int, TValue>
    where TValue: class
    {
        public SortedDictionary<int, TValue> Data { get; protected set; }
        public int LastKey { get; protected set; }

        public InMemoryIntKeyedStorage () 
        { 
            this.Data = new SortedDictionary<int, TValue>();
            this.LastKey = 0;
        }

        public InMemoryIntKeyedStorage (IDictionary<int, TValue> source, int lastKey)
        {
            this.Data = new SortedDictionary<int, TValue>(source);
            this.LastKey = lastKey;
        }

        public InMemoryIntKeyedStorage (IDictionary<string, TValue> source, int lastKey)
        {
            this.Data = new SortedDictionary<int, TValue>();
            this.LastKey = lastKey;
            int currentKey = 0;
            foreach (var kvPair in source) {
                if (int.TryParse(kvPair.Key, out currentKey)) {
                    this.Data[currentKey] = kvPair.Value;
                }
            }
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
            this.Data.TryGetValue(key, out oldValue);
            this.Data[key] = newValue;
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

        public override void PutMany (IEnumerable<KeyValuePair<int, TValue>> values)
        {
            foreach (var kvPair in values) {
                if (this.LastKey < kvPair.Key) this.LastKey = kvPair.Key;
                this.Data[kvPair.Key] = kvPair.Value; 
            }
        }

        public override bool Clear ()
        {
            this.Data = new SortedDictionary<int, TValue>();
            this.LastKey = 0;
            return true;
        }

        public SortedDictionary<string, TValue> StringKeyed ()
        {
            var result = new SortedDictionary<string, TValue>();
            foreach (var kvPair in this.Data) {
                result[kvPair.Key.ToString("D10")] = kvPair.Value;
            }
            return result;
        }
    }
}
