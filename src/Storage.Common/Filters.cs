using System;

namespace Storage.Common
{
    public static class Filters
    {
        // Create Range Filter for any type
        // based on property name and range bounds 
        public static FilterInfo Range ( 
                string propertyName, 
                object lowerBound, 
                object upperBound )
        {
            return null;
        }

        // Create String-Starts Filter
        // based on property name and substring
        public static FilterInfo StringStarts (
                string propertyName,
                string subString )
        {
            return null;
        }

        // Create String-Ends Filter
        // based on property name and substring
        public static FilterInfo StringEnds (
                string propertyName,
                string subString )
        {
            return null;
        }

        // Create String-Contains Filter
        // based on property name and substring
        public static FilterInfo StringContains (
                string propertyName,
                string subString )
        {
            return null;
        }
    }
}