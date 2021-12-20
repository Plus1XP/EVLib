using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EVLib.FileIO
{
    [Obsolete("This Class was depreciated 12 November, 2021. Use EncryptorManager instead.", false)]
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
            this.aes = Aes.Create();
        }

        /// <summary>
        /// Encrypts a String of data to a file on disk using AES.
        /// </summary>
        /// <param name="filePath">Path to file.</param>
        /// <param name="dataToEncrypt">String of data to encrypt.</param>
        /// <param name="keyPhrase">Key to encrypt the data stream.</param>
        public void EncryptStringToFile(string filePath, string dataToEncrypt, string keyPhrase)
        {
            this.SetEncryptionKey(keyPhrase);
            this.SetAesKey();
            this.EncryptStream(filePath, dataToEncrypt);
        }

        /// <summary>
        /// Decrypts data from a file on disk to a String using AES.
        /// </summary>
        /// <param name="filePath">Path to file.</param>
        /// <param name="keyPhrase">Key to decrypt the data stream.</param>
        /// <returns>Decrypted String.</returns>
        public string DecryptStringFromFile(string filePath, string keyPhrase)
        {
            this.SetEncryptionKey(keyPhrase);
            this.GetAesKey();
            return this.DecryptStream(filePath);
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
            this.key = Encoding.ASCII.GetBytes(keyPhrase);
        }

        /// <summary>
        /// Configures the encrytion key and IV 
        /// </summary>
        /// <remarks>
        /// The IV is stored at the begining of the file.
        /// The Key and IV will both be used for encrypting and decrypting.
        /// </remarks>
        private void SetAesKey()
        {
            this.aes.Key = this.key;
            this.iv = this.aes.IV;
        }

        /// <summary>
        /// Reads the IV value from the begining of the file.
        /// </summary>
        private void GetAesKey()
        {
            this.iv = new byte[this.aes.IV.Length];
        }

        /// <summary>
        /// Stream and Encrypt String data to a file on disk using AES.
        /// </summary>
        /// <param name="filePath">Path to file.</param>
        /// <param name="dataToEncrypt">String of data to encrypt.</param>
        private void EncryptStream(string filePath, string dataToEncrypt)
        {
            //Create a file stream
            FileStream dataStream = new FileStream(filePath, FileMode.OpenOrCreate);

            //Stores IV at the beginning of the file.
            //This information will be used for decryption.
            dataStream.Write(this.iv, 0, this.iv.Length);

            //Create a CryptoStream, pass it the FileStream, and encrypt
            //it with the Aes class. 
            CryptoStream cryptStream = new CryptoStream(dataStream, this.aes.CreateEncryptor(), CryptoStreamMode.Write);

            //Create a StreamWriter for easy writing to the file stream.  
            StreamWriter streamWriter = new StreamWriter(cryptStream);

            //Write to the stream.  
            streamWriter.Write(dataToEncrypt);

            // Close streams.
            streamWriter.Close();
            cryptStream.Close();
            dataStream.Close();

        }

        /// <summary>
        /// Stream and Decrypt data from a file on disk to a String using AES.
        /// </summary>
        /// <param name="filePath">Path to file.</param>
        /// <returns>Decrypted String.</returns>
        private string DecryptStream(string filePath)
        {
            //Create a file stream
            FileStream dataStream = new FileStream(filePath, FileMode.Open);

            //Reads IV value from beginning of the file.
            dataStream.Read(this.iv, 0, this.iv.Length);

            //Create a CryptoStream, pass it the file stream, and decrypt
            //it with the Aes class using the key and IV.
            CryptoStream cryptStream = new CryptoStream(dataStream, this.aes.CreateDecryptor(this.key, this.iv), CryptoStreamMode.Read);

            //Read the stream.
            StreamReader streamReader = new StreamReader(cryptStream);

            //MemoryStream memoryStream = new MemoryStream();

            //cryptStream.CopyTo(memoryStream);
            //byte[] data = memoryStream.ToArray();

            string decryptedString = streamReader.ReadToEnd();

            // Convert string stream to byte array.
            //byte[] data = File.ReadAllBytes(test);

            // Close streams.
            streamReader.Close();
            cryptStream.Close();
            dataStream.Close();

            return decryptedString;
        }
    }
}
