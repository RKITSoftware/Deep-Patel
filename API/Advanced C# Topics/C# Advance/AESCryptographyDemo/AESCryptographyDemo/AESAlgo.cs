using System.Security.Cryptography;
using System.Text;

namespace AESCryptographyDemo
{
    /// <summary>
    /// Provides methods to perform AES encryption and decryption.
    /// </summary>
    public class AESAlgo
    {
        /// <summary>
        /// AES encryption algorithm instance
        /// </summary>
        private readonly Aes _aes;

        /// <summary>
        /// Initializes a new instance of the AESAlgo class with default AES settings.
        /// </summary>
        public AESAlgo()
        {
            // Create a new AES instance
            _aes = Aes.Create();

            _aes.KeySize = 256;
            _aes.Mode = CipherMode.CBC;

            _aes.GenerateKey();
            _aes.GenerateIV();
        }

        /// <summary>
        /// Encrypts the specified plaintext using AES algorithm.
        /// </summary>
        /// <param name="plainText">The plaintext to encrypt.</param>
        /// <returns>The encrypted ciphertext.</returns>
        public byte[] Encrypt(string plainText)
        {
            byte[] cipherText;

            // Create an encryptor object using AES instance
            using (ICryptoTransform encryptor = _aes.CreateEncryptor())
            {
                // Convert plaintext to byte array
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                cipherText = encryptor.TransformFinalBlock(plainTextBytes, 0, plainTextBytes.Length);
            }

            return cipherText;
        }

        /// <summary>
        /// Decrypts the specified ciphertext using AES algorithm.
        /// </summary>
        /// <param name="cipherText">The ciphertext to decrypt.</param>
        /// <returns>The decrypted plaintext.</returns>
        public string Decrypt(byte[] cipherText)
        {
            string decryptedText;

            // Create a decryptor object using AES instance
            using (ICryptoTransform decryptor = _aes.CreateDecryptor())
            {
                // Perform decryption
                byte[] decryptedByte = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);
                decryptedText = Encoding.UTF8.GetString(decryptedByte);
            }

            return decryptedText;
        }
    }
}
