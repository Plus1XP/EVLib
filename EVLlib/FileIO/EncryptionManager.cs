using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EVLlib.FileIO
{
    public class EncryptionManager
    {
        Aes aes;
        private byte[] key;
        private byte[] iv;

        /// <summary>
        /// Create a new instance of the default AES implementation class
        /// </summary>
        public EncryptionManager()
        {
            aes = Aes.Create();
        }

        /// <summary>
        /// Encrypts a String of data to a file on disk using AES.
        /// </summary>
        /// <param name="filePath">Path to file.</param>
        /// <param name="dataToEncrypt">String of data to encrypt.</param>
        /// <param name="keyPhrase">Key to encrypt the data stream.</param>
        public void EncryptStringToFile(string filePath, string dataToEncrypt, string keyPhrase)
        {
            SetEncryptionKey(keyPhrase);
            SetAesKey();
            EncryptStream(filePath, dataToEncrypt);
        }

        /// <summary>
        /// Decrypts data from a file on disk to a String using AES.
        /// </summary>
        /// <param name="filePath">Path to file.</param>
        /// <param name="keyPhrase">Key to decrypt the data stream.</param>
        /// <returns>Decrypted String.</returns>
        public string DecryptStringFromFile(string filePath, string keyPhrase)
        {
            SetEncryptionKey(keyPhrase);
            GetAesKey();
            return DecryptStream(filePath);
        }

        /// <summary>
        /// Encryption key used to encrypt the stream.
        /// </summary>
        /// <remarks>
        /// The same value must be used for encrypting and decrypting the stream.
        /// </remarks>
        /// <param name="keyPhrase">Key to decrypt the data stream.</param>
        private void SetEncryptionKey(string keyPhrase)
        {
            key = Encoding.ASCII.GetBytes(keyPhrase);
        }
    }
}
