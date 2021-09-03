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
    }
}
