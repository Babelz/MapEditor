using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor.Components
{
    public static class StringHelper
    {
        /// <summary>
        /// Returns true if given string only contains digits.
        /// </summary>
        public static bool OnlyContainsDigits(string str)
        {
            int i = 0;

            return int.TryParse(str, out i);
        }

        /// <summary>
        /// Removes all non digit characters from given string.
        /// </summary>
        public static string RemoveAllNonDigitCharacters(string str)
        {
            string fixedString = string.Empty;

            foreach (char ch in str) if (char.IsDigit(ch)) fixedString += ch;

            return fixedString;
        }
    }
}
