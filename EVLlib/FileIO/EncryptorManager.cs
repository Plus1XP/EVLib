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
    }
}