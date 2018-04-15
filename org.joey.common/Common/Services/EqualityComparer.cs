using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Joey.Common
{
    public class EqualityComparer : IEqualityComparer<string>
    {
        bool bIgnoreCase = false;
        private EqualityComparer(bool ignoreCase)
        {
            bIgnoreCase = ignoreCase;
        }
        public bool Equals(string x, string y)
        {
            if (string.IsNullOrEmpty(x) && string.IsNullOrEmpty(y))
                return true;
            return string.Compare(x, y, bIgnoreCase) == 0;
        }
        public static EqualityComparer StringIgnoreCase
        {
            get { return new EqualityComparer(true); }
        }
        public static EqualityComparer String
        {
            get { return new EqualityComparer(true); }
        }
        public int GetHashCode(string obj)
        {
            if (bIgnoreCase)
                return obj.ToLower().GetHashCode();
            else
                return obj.GetHashCode();
            
        }
    }
}
