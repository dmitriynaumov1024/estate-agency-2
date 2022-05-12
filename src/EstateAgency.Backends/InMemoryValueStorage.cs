using System;
using System.Collections.Generic;
using System.Linq;
using Storage.Common;

namespace EstateAgency.Backends
{
    public class InMemoryValueStorage<TValue> : Storage<TValue>
    where TValue: class, IComparable<TValue>
    {
        public SortedSet<TValue> Data { get; protected set; }

        public InMemoryValueStorage () 
        {
            this.Data = new SortedSet<TValue>();
        }

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
            var result = this.Data.Where(val => FilterExtensions.Apply(filter, val));
            if (limit != null) {
                result = result.Take((int)limit);
            }
            return result;
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

        public override void PutMany (IEnumerable<TValue> values) 
        {
            foreach (TValue value in values) {
                this.Data.Add(value);
            }
        }

        public override bool Clear ()
        {
            this.Data = new SortedSet<TValue>();
            return true;
        }
    }
}