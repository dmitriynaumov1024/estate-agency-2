using System;
using System.Collections.Generic;
using Storage.Common;

namespace EstateAgency.Backends
{
    public class InMemoryStringKeyedStorage<TValue> : InMemoryKeyValueStorage<string, TValue>
    where TValue: class
    {
        public InMemoryStringKeyedStorage() : base() { }

        public InMemoryStringKeyedStorage (IDictionary<string, TValue> source) : base(source) { }
    }
}
