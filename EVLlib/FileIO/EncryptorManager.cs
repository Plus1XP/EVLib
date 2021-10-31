using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EVLlib.FileIO
{
    /// <summary>
    /// A Semmetric Encrytor using AES. 128 bits for the block and 256 bits for the key.
    /// </summary>
    /// <remarks>
    /// An issue with the depreciated solution is that it always returns the same
    /// result (same sequence of bytes) when given your password together with
    /// the same data to encrypt. Because of that it is easier for the attacker
    /// to guess your password. There is a parameter to initialize the algorithm,
    /// intuitively named Initialization Vector (IV), which solves this problem.
    /// The IV must be of the same size as is the block size.
    /// The new randomly generated IV is simply prepending the IV to the encrypted data.
    /// That might seem strange, but an IV is not considered as
    /// something secret so it is not a problem from security perspective.
    /// </remarks>
    public class EncryptorManager: FileManager
    {
        private const int AesBlockByteSize = 128 / 8;

        private const int PasswordSaltByteSize = 128 / 8;
        private const int PasswordByteSize = 256 / 8;
        private const int PasswordIterationCount = 100_000;

        private const int SignatureByteSize = 256 / 8;

        private const int MinimumEncryptedMessageByteSize =
            PasswordSaltByteSize + // auth salt
            PasswordSaltByteSize + // key salt
            AesBlockByteSize + // IV
            AesBlockByteSize + // cipher text min length
            SignatureByteSize; // signature tag

        private readonly Encoding StringEncoding = Encoding.UTF8;
        private readonly RandomNumberGenerator Random = RandomNumberGenerator.Create();

        /// <summary>
        /// Encrypts a String of data to a file on disk using AES.
        /// </summary>
        /// <param name="filePath">Path to file.</param>
        /// <param name="stringToEncrypt">String of data to encrypt.</param>
        /// <param name="password">Password used to encrypt / decrypt data.</param>
        public void EncryptToFile(string filePath, string stringToEncrypt, string password)
        {
            byte[] encryptedByteArray = Encrypt(stringToEncrypt, password);
            SaveToFile(filePath, encryptedByteArray);
        }

        /// <summary>
        /// Encrypts a String of data to Base64 using AES.
        /// </summary>
        /// <remarks>
        /// Encoding.UTF8.GetString(bytes) does not convert a byte array containing
        /// arbitrary bytes to a string. Instead, it converts a byte array that
        /// is supposed to contain bytes making up an UTF8 encoded string back to
        /// that string. If the byte array contains arbitrary bytes, such as the
        /// result of encrypting text, it is almost certain to corrupt the data and/or
        /// lose bytes. Instead, you should use a different method of converting
        /// a byte array to a string and back. Base64 encoding has been choosen
        /// for this purpose.
        /// </remarks>
        /// <param name="stringToEncrypt">String of data to encrypt.</param>
        /// <param name="password">Password used to encrypt / decrypt data.</param>
        /// <returns>AES Encrypted String in Base64.</returns>
        public string EncryptToString(string stringToEncrypt, string password)
        {
            byte[] encryptedByteArray = Encrypt(stringToEncrypt, password);
            return Convert.ToBase64String(encryptedByteArray);
        }

        /// <summary>
        /// Encrypts a String of data as a Byte Array using AES.
        /// </summary>
        /// <param name="stringToEncrypt">String of data to encrypt.</param>
        /// <param name="password">Password used to encrypt / decrypt data.</param>
        /// <returns>AES Encrypted String as Byte Array.</returns>
        public byte[] EncryptToByteArray(string stringToEncrypt, string password)
        {
            return Encrypt(stringToEncrypt, password);
        }

        /// <summary>
        /// Decrypts data to a String from a file on disk using AES.
        /// </summary>
        /// <param name="filePath">Path to file.</param>
        /// <param name="password">Password used to encrypt / decrypt data.</param>
        /// <returns>Decrypted String.</returns>
        public string DecryptFromFile(string filePath, string password)
        {
            byte[] encryptedByteArray = ReadBytesFromFile(filePath);
            return Decrypt(encryptedByteArray, password);
        }

        /// <summary>
        /// Decrypts a Base64 String encrypted of data using AES.
        /// </summary>
        /// <remarks>
        /// Encoding.UTF8.GetString(bytes) does not convert a byte array containing
        /// arbitrary bytes to a string. Instead, it converts a byte array that
        /// is supposed to contain bytes making up an UTF8 encoded string back to
        /// that string. If the byte array contains arbitrary bytes, such as the
        /// result of encrypting text, it is almost certain to corrupt the data and/or
        /// lose bytes. Instead, you should use a different method of converting
        /// a byte array to a string and back. Base64 encoding has been choosen
        /// for this purpose.
        /// </remarks>
        /// <param name="stringToDecrypt">String of data to Decrypt.</param>
        /// <param name="password">Password used to encrypt / decrypt data.</param>
        /// <returns>Decrypted String.</returns>
        public string DecryptFromString(string stringToDecrypt, string password)
        {
            byte[] encryptedByteArray = Convert.FromBase64String(stringToDecrypt);
            return Decrypt(encryptedByteArray, password);
        }
    }
}