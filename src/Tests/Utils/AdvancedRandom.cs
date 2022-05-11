using System;
using System.Collections.Generic;

namespace Tests
{
    public class AdvancedRandom : Random
    {
        public AdvancedRandom () : base () { }

        public AdvancedRandom (int seed) : base (seed) { }

        public T NextOf<T> (T[] source)
        {
            return source[this.Next(source.Length)];
        }
    }
}
