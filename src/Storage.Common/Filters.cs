using System;

namespace Storage.Common
{
    public static class Filter
    {
        public static FilterInfo Exact ( 
                string propertyName, 
                object value )
        {
            return new FilterInfoExact {
                PropertyName = propertyName,
                Value = value
            };
        }

        // Create Range Filter for any type
        // based on property name and range bounds 
        public static FilterInfo Range ( 
                string propertyName, 
                IComparable lowerBound, 
                IComparable upperBound )
        {
            return new FilterInfoRange {
                PropertyName = propertyName,
                LowerBound = lowerBound,
                UpperBound = upperBound
            };
        }

        // Create String-Starts Filter
        // based on property name and substring
        public static FilterInfo StringStarts (
                string propertyName,
                string substring )
        {
            return new FilterInfoString {
                PropertyName = propertyName,
                Substring = substring,
                Position = SubstringPosition.Start
            };
        }

        // Create String-Ends Filter
        // based on property name and substring
        public static FilterInfo StringEnds (
                string propertyName,
                string substring )
        {
            return new FilterInfoString {
                PropertyName = propertyName,
                Substring = substring,
                Position = SubstringPosition.End
            };
        }

        // Create String-Contains Filter
        // based on property name and substring
        public static FilterInfo StringContains (
                string propertyName,
                string substring )
        {
            return new FilterInfoString {
                PropertyName = propertyName,
                Substring = substring,
                Position = SubstringPosition.Middle
            };
        }
    }
}