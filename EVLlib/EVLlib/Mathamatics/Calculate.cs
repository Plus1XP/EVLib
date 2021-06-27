using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace EVLlib.Mathamatics
{
    public class Calculate
    {
        /// <summary>
        /// Returns the power of an Interger without using the Math.Pow Method.
        /// </summary>
        /// <remarks>
        /// The power or exponent of a number says how many times to use the 
        /// number in multiplication.
        /// </remarks>
        /// <param name="baseNumber">The base number to be multiplied.</param>
        /// <param name="exponent">The number of times to multiply the base.</param>
        /// <returns>The power  of the result.</returns>
        public long Power(int baseNumber, int exponent)
        {
            long Result = baseNumber;
            for (int i = exponent; i > 1; i--)
            {
                Result = (Result * baseNumber);
            }
            return Result;
        }

        /// <summary>
        /// Calculate the average value of a List, using a foreach loop. 
        /// </summary>
        /// <remarks>
        /// The average of a set of numbers is simply the sum of the numbers 
        /// divided by the total number of values in the set.
        /// </remarks>
        /// <param name="listOfNumbers">List of Doubles to be divided by the 
        /// total amount of numbers in the list.</param>
        /// <returns>The average of a set of numbers.</returns>
        public double Average(List<double> listOfNumbers)
        {
            double total = 0;
            double counter = 0;

            foreach (double number in listOfNumbers)
            {
                total = (total + number);
                counter = (counter + 1);
            }

            double average = (total / counter);

            return average;
        }

        /// <summary>
        /// Create truely random numbers using an instance of an encryption class (RNGCryptoServiceProvider).
        /// </summary>
        /// <remarks>
        /// "Random" The .Net framework's built-in random number generating class, 
        /// doesn’t produce numbers that are really random. Using an instance of an encryption class 
        /// (RNGCryptoServiceProvider) is better at not following a pattern when it creates random numbers.
        /// </remarks>
        /// <param name="minimumValue">Minimum number.</param>
        /// <param name="maximumValue">Maximium number.</param>
        /// <returns>Random number.</returns>
        public int RandomNumber(int minimumValue, int maximumValue)
        {
            byte[] randomNumber = new byte[1];

            RNGCryptoServiceProvider generator = new RNGCryptoServiceProvider(randomNumber);

            double asciiValueOfRandomCharacter = Convert.ToDouble(randomNumber[0]);

            // We are using Math.Max, and substracting 0.00000000001,
            // to ensure "multiplier" will always be between 0.0 and .99999999999
            // Otherwise, it's possible for it to be "1", which causes problems in our rounding.
            double multiplier = Math.Max(0, (asciiValueOfRandomCharacter / 255d) - 0.00000000001d);

            // We need to add one to the range, to allow for the rounding done with Math.Floor
            int range = maximumValue - minimumValue + 1;

            double randomValueInRange = Math.Floor(multiplier * range);

            return (int)(minimumValue + randomValueInRange);
        }        
    }
}
