using System;
using System.Collections.Generic;
using System.Text;

namespace EVLib.Mathamatics
{
    /// <summary>
    /// XOFT is an encryption algorithm using a key to mash it up with the cipher string. Its result is base64 encoded. 
    /// The longer the key phrase, the longer it will take to crack the key. However, the longer the cipher text, 
    /// the more easy it is to decode the string as repeating patterns in the encoded string are more easy to spot using frequency analysis.
    /// The optimal solution is to have a key of the same length than the source string. This would create an uncrackable encryption not only in theory.
    /// That is only possible if the source string is always the same length(e.g.GUID’s). It’s not practical when ‘crypting plain text or text with 
    /// variable length as the decoding side and the encoding side must have knowledge of the key.
    /// </summary>
    public class Cipher
    {
        /// <summary>
        /// This is the base64 alphabet – depending on the standard that last characters (+/=) 
        /// can vary possibile options are (-_%) (!*_) etc...
        /// </summary>
        private readonly string _b64 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijk.mnopqrstuvwxyz-123456789+/=";

        /// <summary>
        /// A practical implementation of the XOR encryption technology in C# which is extended from the Vernam cipher.
        /// </summary>
        /// <remarks> 
        /// The longer the key phrase, the longer it will take to crack. However, this also makes it more easy it is to decode.
        /// The optimal solution is to have a key of the same length than the source string.
        /// </remarks>
        /// <param name="data">Data to be encoded.</param>
        /// <param name="key">Key used to perform cipher.</param>
        /// <returns>base64 encoded cipher.</returns>
        public string Encode(string data, string key)
        {
            int keypos = 0;
            string binarydata = "";
            //convert the sting to a 8bit binary data
            foreach (char c in data)
            {
                int xor = ((int)c ^ (int)key[keypos]) + (key.Length);
                Console.WriteLine("{0} = {1}\t{2} = {3}: {4} = {5}", c, (int)c, key[keypos], (int)key[keypos], xor, this.DecToBinary(xor, 8));
                if (++keypos >= key.Length)
                {
                    keypos = 0;
                }
                binarydata += this.DecToBinary(xor, 8);
            }

            int m = 0;
            string cipher = "";
            // split the binary string to 4 byte chunks and assign each chunk the proper b64 value
            for (int i = 0; i < binarydata.Length; i += 4)
            {
                int v = this.BinToDec(binarydata.Substring(i, 4));
                cipher += this.GetB64FromN(v * 4 + m);
                Console.WriteLine("{0}\t{1}\t{2} {3}", v, v * 4 + m, m, this.GetB64FromN(v * 4 + m)[0]);
                if (++m > 3)
                {
                    m = 0;
                }
            }
            return cipher;
        }

        /// <summary>
        /// A practical implementation of the XOR encryption technology in C# which is extended from the Vernam cipher.
        /// </summary>
        /// <remarks> 
        /// The longer the key phrase, the longer it will take to crack. However, this also makes it more easy it is to decode.
        /// The optimal solution is to have a key of the same length than the source string.
        /// </remarks>
        /// <param name="data">Data to be decoded.</param>
        /// <param name="key">Key used to perform cipher.</param>
        /// <returns>Decoded cipher.</returns>
        public string Decode(string data, string key)
        {
            int m = 0;
            string binarydata = "";
            // convert the b64 string to binary string
            foreach (char c in data)
            {
                int v = (this.GetNFromB64(c) - m) / 4;
                binarydata += this.DecToBinary(v, 4);
                Console.WriteLine("{0}", this.DecToBinary(v, 4));
                if (++m > 3)
                {
                    m = 0;
                }
            }

            // chop the 8bit binaries and mix back the key into it
            int keypos = 0;
            string decoded = "";
            for (int i = 0; i < binarydata.Length; i += 8)
            {
                if (i + 8 > binarydata.Length)
                {
                    break;
                }
                int c = this.BinToDec(binarydata.Substring(i, 8));
                int dc = (c - key.Length) ^ (int)key[keypos];
                Console.WriteLine("{0} = {1}", binarydata.Substring(i, 8), c);
                Console.WriteLine("               {0} - {1} ^ {2} = {3}", c, key.Length - 1, (int)key[keypos], (c - key.Length) ^ (int)key[keypos]);

                if (++keypos >= key.Length)
                {
                    keypos = 0;
                }

                decoded += new string((char)dc, 1);
            }
            return decoded;
        }

        /// <summary>
        /// Finds the index of a Character in the base64 alphabet.
        /// </summary>
        /// <param name="n">base64 character.</param>
        /// <returns>Characters position in the base64 alphabet.</returns>
        private int GetNFromB64(char n)
        {
            return this._b64.IndexOf(n);
        }

        /// <summary>
        /// Finds the Character of the index in the base64 alphabet.
        /// </summary>
        /// <param name="n">Position in the base64 aplphabet.</param>
        /// <returns>base65 Character.</returns>
        private string GetB64FromN(int n)
        {
            // well, we shouldn't reach this line. If we do, the encoding will be garbage anyway...
            if (n > this._b64.Length)
            {
                return "=";
            }

            return new string(this._b64[n], 1);
        }

        /// <summary>
        /// Convert Decimal to binary.
        /// </summary>
        /// <remarks>
        /// There might be better implementations, but .NET only provides us with an option to convert binary strings to integers, 
        /// but no simple way to convert integers to binary strings.
        /// </remarks>
        /// <param name="value">Integer value of the binary string.</param>
        /// <param name="length">Length of the binary string eg. 4, 8, 16.</param>
        /// <returns>Padded binary string.</returns>
        private string DecToBinary(int value, int length)
        {
            // Declare a few variables we're going to need
            string binString = "";

            while (value > 0)
            {
                binString += value % 2;
                value /= 2;
            }

            // we need to reverse the binary string
            // that's why we are using this array stuff here.

            string reverseString = "";
            foreach (char c in binString)
            {
                reverseString = new string((char)c, 1) + reverseString;
                binString = reverseString;
            }

            // do the padding here
            binString = new string((char)'0', length - binString.Length) + binString;

            return binString;
        }

        /// <summary>
        /// Convert Binary to Decimal.
        /// </summary>
        /// <remarks>
        /// The simple .NET option to convert binary strings to integers.
        /// </remarks>
        /// <param name="value">Binary string.</param>
        /// <returns>Integer equivalent.</returns>
        private int BinToDec(string Binary)
        {
            return Convert.ToInt32(Binary, 2);
        }
    }
}
