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

        /// <summary>
        /// Get string value after removing [excludedWords].
        /// </summary>
        /// <remarks>
        /// The RemoveWords method accepts 1 array (excludedWords) and
        /// iterates through it remove each match in the source string.
        /// </remarks>
        /// <param name="excludedWords">Array of strings.</param>
        /// <returns>
        /// The substring with the excluded words removed.
        /// </returns>
        public static string RemoveWords(this string value, string[] excludedWords)
        {
            foreach (string word in excludedWords)
            {
                value = value.Replace(word, string.Empty);
            }
            return value.Trim().ReduceWhitespace();
        }

        /// <summary>
        /// Get string value after removing swapping letters.
        /// </summary>
        /// <remarks>
        /// The ReplaceLetters method accepts 2 Chars (lettersToFind)
        /// and (lettersToReplace), then iterates through the source string
        /// replacing each match.
        /// </remarks>
        /// <param name="letterToFind">Char to find in source string.</param>
        /// <param name="letterToReplace">Char to replace.</param>
        /// <returns>
        /// The substring with letters replaced upon match.
        /// </returns>
        public static string ReplaceLetters(this string wordToSearch, char letterToFind, char letterToReplace)
        {
            StringBuilder word = new StringBuilder(wordToSearch);

            int index = 0;

            foreach (char letter in wordToSearch)
            {
                if (letter.Equals(letterToFind))
                {
                    word.Remove(index, 1);
                    word.Insert(index, letterToReplace);
                }

                index++;
            }

            return word.ToString();
        }

        /// <summary>
        /// Get string value after removing any extra white spaces.
        /// </summary>
        /// <remarks>
        /// This method REDUCES not REMOVES white spaces.
        /// It will also leave the first white space and remove any extra.
        /// </remarks>
        /// <param name="value">String to inspect for any extra white spaces.</param>
        /// <returns>A Single spaced string.</returns>
        public static string ReduceWhitespace(this string value)
        {
            var newString = new StringBuilder();
            bool previousIsWhitespace = false;
            for (int i = 0; i < value.Length; i++)
            {
                if (Char.IsWhiteSpace(value[i]))
                {
                    if (previousIsWhitespace)
                    {
                        continue;
                    }

                    previousIsWhitespace = true;
                }
                else
                {
                    previousIsWhitespace = false;
                }

                newString.Append(value[i]);
            }

            return newString.ToString();
        }
        #endregion
    }
}
