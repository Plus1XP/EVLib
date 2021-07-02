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

        /// <summary>
        /// Get string value after [first] a.
        /// </summary>
        /// <remarks>
        /// The Before method accepts 1 string (a) 
        /// and locates it in the source string.
        /// </remarks>
        /// <param name="a">First String.</param>
        /// <returns>
        /// The substring before the first string.
        /// </returns>
        public static string Before(this string value, string a)
        {
            int posA = value.IndexOf(a);
            if (posA == -1)
            {
                return "";
            }
            return value.Substring(0, posA);
        }

        /// <summary>
        /// Get string value after [last] a.
        /// </summary>
        /// <remarks>
        /// The After method accepts 1 string (a) 
        /// and locates it in the source string.
        /// </remarks>
        /// <param name="a">Last String.</param>
        /// <returns>
        /// The substring After the Last string.
        /// </returns>
        public static string After(this string value, string a)
        {
            int posA = value.LastIndexOf(a);
            if (posA == -1)
            {
                return "";
            }
            int adjustedPosA = posA + a.Length;
            if (adjustedPosA >= value.Length)
            {
                return "";
            }
            return value.Substring(adjustedPosA);
        }
        #endregion
    }
}
