using System;

namespace Storage.Common
{
    public abstract class FilterInfo
    {
        public FilterInfoCombination And (FilterInfo other)
        {
            return new FilterInfoCombination {
                Filter1 = this,
                Filter2 = other
            };
        }
    }

    public class FilterInfoCombination : FilterInfo
    {
        public FilterInfo Filter1 { get; set; }
        public FilterInfo Filter2 { get; set; }
    }

    public class FilterInfoExact : FilterInfo
    {
        public string PropertyName { get; set; }
        public object Value { get; set; }
    }

    public class FilterInfoRange : FilterInfo
    {
        public string PropertyName { get; set; }
        public IComparable LowerBound { get; set; }
        public IComparable UpperBound { get; set; }
    }

    public class FilterInfoString : FilterInfo
    {
        public string PropertyName { get; set; }
        public string Substring { get; set; }
        public SubstringPosition Position { get; set; }
    }

    public enum SubstringPosition
    {
        Start,
        Middle,
        End
    }
}
