using System;
using System.Collections.Generic;
using Storage.Common;

namespace EstateAgency.Backends
{
    public class InMemoryValueStorage<TValue> : Storage<TValue>
    where TValue: class, IComparable<TValue>
    {
        public SortedSet<TValue> Data { get; protected set; }

        public InMemoryValueStorage () { }

        public InMemoryValueStorage (IEnumerable<TValue> source)
        {
            this.Data = new SortedSet<TValue>(source);
        }

        public override bool Put (TValue value)
        {
            return this.Data.Add(value);
        }

        public override IEnumerable<TValue> Filter (FilterInfo filter, int? limit = null)
        {
            // to be implemented
            return null;
        }

        public override bool Contains (TValue value)
        {
            return this.Data.Contains(value);
        }

        public override bool Delete (TValue value)
        {
            return this.Data.Remove(value);
        }

        public override IEnumerable<TValue> AsEnumerable ()
        {
            return this.Data;
        }

        public override bool Clear ()
        {
            this.Data = new SortedSet<TValue>();
            return true;
        }
    }
}