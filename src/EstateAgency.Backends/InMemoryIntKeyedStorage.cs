using System;
using System.Collections.Generic;
using Storage.Common;

namespace EstateAgency.Backends
{
    public class InMemoryIntKeyedStorage<TValue> : InMemoryKeyValueStorage<int, TValue>
    where TValue: class
    {
        public int LastKey { get; protected set; }

        public InMemoryIntKeyedStorage () 
            : base ()
        { 
            this.LastKey = 0;
        }

        public InMemoryIntKeyedStorage (IDictionary<int, TValue> source, int lastKey)
            : base(source)
        {
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

        public override int Put (TValue value)
        {
            this.LastKey++;
            this.Data[this.LastKey] = value;
            return this.LastKey;
        }

        public override bool Clear ()
        {
            if (base.Clear()) {
                this.LastKey = 0;
                return true;
            }
            else {
                return false;
            }
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
