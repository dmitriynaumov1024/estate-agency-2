using System;
using System.Collections.Generic;
using Storage.Common;

namespace EstateAgency.Backends
{
    public static class FilterExtensions
    {
        public static bool Apply (FilterInfo filter, object obj) {
            Type objType = obj.GetType();
            if (filter is FilterInfoCombination) {
                var fcomb = filter as FilterInfoCombination;
                return Apply(fcomb.Filter1, obj) && Apply(fcomb.Filter2, obj);
            }
            else if (filter is FilterInfoExact) {
                var f = filter as FilterInfoExact;
                try { 
                    var prop = obj.GetType().GetProperty(f.PropertyName, f.Value.GetType());
                    return f.Value.Equals(prop.GetValue(obj));
                }
                catch (Exception) {
                    return false;
                }
            }
            else if (filter is FilterInfoRange) {
                var f = filter as FilterInfoRange;
                IComparable lowerBound = f.LowerBound, upperBound = f.UpperBound;
                try {
                    var prop = obj.GetType().GetProperty(f.PropertyName, lowerBound.GetType());
                    IComparable v = prop.GetValue(obj) as IComparable;
                    return (lowerBound.CompareTo(v) <= 0) 
                        && (upperBound.CompareTo(v) >= 0); 
                }
                catch (Exception ex) {
                    return false;
                }
            }
            else if (filter is FilterInfoString) {
                var f = filter as FilterInfoString;
                var pos = f.Position;
                try {
                    var prop = obj.GetType().GetProperty(f.PropertyName, typeof(string));
                    string s = prop.GetValue(obj) as string;
                    if (pos == SubstringPosition.Start) {
                        return s.StartsWith(f.Substring);
                    }
                    else if (pos == SubstringPosition.End) {
                        return s.EndsWith(f.Substring);
                    }
                    else {
                        return s.Contains(f.Substring);
                    }
                }
                catch (Exception ex) {
                    return false;
                }
            }
            Console.Write("Unrecognized filter! \n");
            return true;
        }
    }
}