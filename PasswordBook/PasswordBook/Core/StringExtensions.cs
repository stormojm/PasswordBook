using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PasswordBook.Core
{
    public static class StringExtensions
    {
        public static bool Contains(this string s, string value, StringComparison comparisonType)
        {
            return s.IndexOf(value, comparisonType) >= 0;
        }
    }
}
