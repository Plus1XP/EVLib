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

        /// <summary>
        /// Decrypts a Byte Array of encrypted data using AES.
        /// </summary>
        /// <param name="byteArrayToDecrypt">Byte Array to decrypt.</param>
        /// <param name="password">Password used to encrypt / decrypt data.</param>
        /// <returns>Decrypted String.</returns>
        public string DecryptFromByteArray(byte[] byteArrayToDecrypt, string password)
        {
            return Decrypt(byteArrayToDecrypt, password);
        }

        /// <summary>
        /// Encrypt String data to a Byte Array using AES.
        /// </summary>
        /// <remarks>
        /// One additional security feature added is signing. otherwise
        /// we have no way to verify whether some data is missing or some extra
        /// data has been injected. With signing we will make sure our message
        /// hasn’t been altered in any way. To implement this we will use Message
        /// authentication code (MAC), more specifically HMAC-SHA256 which belongs
        /// to a MAC subcategory called hash-based message authentication code.
        /// In a simplified way the idea behind HMAC is to take your encrypted
        /// message, hash it and then hash it again with an authentication key.
        /// It’s important to note that this key must be different from the one
        /// used for encrypting your message.
        /// </remarks>
        /// <param name="stringToEncrypt">String of data to encrypt.</param>
        /// <param name="password">Password used to encrypt / decrypt data.</param>
        /// <returns>AES Encrypted String as Byte Array.</returns>
        private byte[] Encrypt(string stringToEncrypt, string password)
        {
            // encrypt
            var keySalt = GenerateRandomBytes(PasswordSaltByteSize);
            var key = GetKey(password, keySalt);
            var iv = GenerateRandomBytes(AesBlockByteSize);

            byte[] cipherText;
            using (var aes = CreateAes())
            using (var encryptor = aes.CreateEncryptor(key, iv))
            {
                var plainText = StringEncoding.GetBytes(stringToEncrypt);
                cipherText = encryptor
                    .TransformFinalBlock(plainText, 0, plainText.Length);
            }

            // sign
            var authKeySalt = GenerateRandomBytes(PasswordSaltByteSize);
            var authKey = GetKey(password, authKeySalt);

            var result = MergeArrays(
                additionalCapacity: SignatureByteSize,
                authKeySalt, keySalt, iv, cipherText);

            using (var hmac = new HMACSHA256(authKey))
            {
                var payloadToSignLength = result.Length - SignatureByteSize;
                var signatureTag = hmac.ComputeHash(result, 0, payloadToSignLength);
                signatureTag.CopyTo(result, payloadToSignLength);
            }

            return result;
        }

        /// <summary>
        /// Decrypt data from a Byte Array to a String using AES.
        /// </summary>
        /// <remarks>
        /// One little thing maybe worth mentioning is the decryption part which
        /// handles verification of the signature. There we are comparing all the
        /// bytes of the signature without any branching. So no matter what data
        /// we get, the verification should take a constant amount of time thus
        /// mitigating any attempt to timing attack.
        /// </remarks>
        /// <param name="byteArrayToDecrypt">Byte Array to decrypt.</param>
        /// <param name="password">Password used to encrypt / decrypt data.</param>
        /// <returns>Decrypted String.</returns>
        private string Decrypt(byte[] byteArrayToDecrypt, string password)
        {
            if (byteArrayToDecrypt is null
                || byteArrayToDecrypt.Length < MinimumEncryptedMessageByteSize)
            {
                throw new ArgumentException("Invalid length of encrypted data");
            }

            var authKeySalt = byteArrayToDecrypt
                .AsSpan(0, PasswordSaltByteSize).ToArray();
            var keySalt = byteArrayToDecrypt
                .AsSpan(PasswordSaltByteSize, PasswordSaltByteSize).ToArray();
            var iv = byteArrayToDecrypt
                .AsSpan(2 * PasswordSaltByteSize, AesBlockByteSize).ToArray();
            var signatureTag = byteArrayToDecrypt
                .AsSpan(byteArrayToDecrypt.Length - SignatureByteSize, SignatureByteSize).ToArray();

            var cipherTextIndex = authKeySalt.Length + keySalt.Length + iv.Length;
            var cipherTextLength =
                byteArrayToDecrypt.Length - cipherTextIndex - signatureTag.Length;

            var authKey = GetKey(password, authKeySalt);
            var key = GetKey(password, keySalt);

            // verify signature
            using (var hmac = new HMACSHA256(authKey))
            {
                var payloadToSignLength = byteArrayToDecrypt.Length - SignatureByteSize;
                var signatureTagExpected = hmac
                    .ComputeHash(byteArrayToDecrypt, 0, payloadToSignLength);

                // constant time checking to prevent timing attacks
                var signatureVerificationResult = 0;
                for (int i = 0; i < signatureTag.Length; i++)
                {
                    signatureVerificationResult |= signatureTag[i] ^ signatureTagExpected[i];
                }

                if (signatureVerificationResult != 0)
                {
                    throw new CryptographicException("Invalid signature");
                }
            }

            // decrypt
            using (var aes = CreateAes())
            {
                using (var encryptor = aes.CreateDecryptor(key, iv))
                {
                    var decryptedBytes = encryptor
                        .TransformFinalBlock(byteArrayToDecrypt, cipherTextIndex, cipherTextLength);
                    return StringEncoding.GetString(decryptedBytes);
                }
            }
        }

        /// <summary>
        /// Creates a new instance of the Aes class.
        /// </summary>
        /// <remarks>
        /// Aes.Create() is the recommended way to get an instance of the best
        /// available implementation of the Aes abstract class and that also
        /// gives you good defaults, but I still prefer to be explicit, so I have
        /// added small helper function which I can then reuse. Important!
        /// Always make sure you are using CBC mode over ECB, since ECB has
        /// serious security issues.
        /// </remarks>
        /// <returns>An Aes object.</returns>
        private Aes CreateAes()
        {
            var aes = Aes.Create();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            return aes;
        }

        /// <summary>
        /// Encryption key used to encrypt the String.
        /// (Note: The same key must be used for both encrypting / decrypting data)
        /// </summary>
        /// <remarks>
        /// To make sure our password is usable as a key for AES we are currently
        /// simply hashing it with MD5. That stretches shorter or longer password
        /// into a key of exactly the size we need it to be. Even though the
        /// entropy of our chosen password will not increase we can still
        /// strengthen our resulting key against bruteforce and dictionary attacks.
        /// Algorithms for this purpose belong to a category named password-based
        /// key derivation functions.
        /// </remarks>
        /// <param name="password">Password used to encrypt / decrypt data.</param>
        /// <param name="passwordSalt">Random bytes used to randomise the hash.</param>
        /// <returns>Key derived from the password, salt, number of iterations, hash name.</returns>
        private byte[] GetKey(string password, byte[] passwordSalt)
        {
            var keyBytes = StringEncoding.GetBytes(password);

            using (var derivator = new Rfc2898DeriveBytes(
                keyBytes, passwordSalt,
                PasswordIterationCount, HashAlgorithmName.SHA256))
            {
                return derivator.GetBytes(PasswordByteSize);
            }
        }

        /// <summary>
        /// Generates a specific amount random bytes.
        /// </summary>
        /// <param name="numberOfBytes">number of bytes to be randomised.</param>
        /// <returns>Random bytes.</returns>
        private byte[] GenerateRandomBytes(int numberOfBytes)
        {
            var randomBytes = new byte[numberOfBytes];
            Random.GetBytes(randomBytes);
            return randomBytes;
        }
    }
}