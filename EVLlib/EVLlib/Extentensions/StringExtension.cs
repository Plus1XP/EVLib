using System;
using System.Collections.Generic;
using System.Text;

namespace EVLlib.Extentensions
{
    public static class StringExtension
    {
        #region SubString Extension Methods
        /// <summary>
        /// Get string value between [first] a and [last] b.
        /// </summary>
        /// <remarks>
        /// The Between method accepts 2 strings (a and b) 
        /// and locates each in the source string.
        /// </remarks>
        /// <param name="a">First String.</param>
        /// <param name="b">Last String.</param>
        /// <returns>
        /// The substring between 2 strings.
        /// </returns>
        public static string Between(this string value, string a, string b)
        {
            int posA = value.IndexOf(a);
            int posB = value.LastIndexOf(b);
            if (posA == -1)
            {
                return "";
            }
            if (posB == -1)
            {
                return "";
            }
            int adjustedPosA = posA + a.Length;
            if (adjustedPosA >= posB)
            {
                return "";
            }
            return value.Substring(adjustedPosA, posB - adjustedPosA);
        }
        #endregion
    }
}
