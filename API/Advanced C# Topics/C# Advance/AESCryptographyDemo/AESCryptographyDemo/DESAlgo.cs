using System.Security.Cryptography;
using System.Text;

namespace AESCryptographyDemo
{
    /// <summary>
    /// Provides methods to perform DES encryption and decryption.
    /// </summary>
    public class DESAlgo
    {
        // Private field to store DES instance
        private readonly DES _des;

        /// <summary>
        /// Initializes a new instance of the DESAlgo class with default DES settings.
        /// </summary>
        public DESAlgo()
        {
            _des = DES.Create();

            _des.GenerateKey();
            _des.GenerateIV();
        }

        /// <summary>
        /// Encrypts the specified plaintext using DES algorithm.
        /// </summary>
        /// <param name="plainText">The plaintext to encrypt.</param>
        /// <returns>The encrypted ciphertext.</returns>
        public byte[] Encrypt(string plainText)
        {
            byte[] cipherBytes;

            // Create an encryptor object using DES instance
            using (ICryptoTransform encryptor = _des.CreateEncryptor())
            {
                // Convert plaintext to byte array
                byte[] plaintextBytes = Encoding.UTF8.GetBytes(plainText);
                cipherBytes = encryptor.TransformFinalBlock(plaintextBytes, 0, plaintextBytes.Length);
            }

            return cipherBytes;
        }

        /// <summary>
        /// Decrypts the specified ciphertext using DES algorithm.
        /// </summary>
        /// <param name="cipherBytes">The ciphertext to decrypt.</param>
        /// <returns>The decrypted plaintext.</returns>
        public string Decrypt(byte[] cipherBytes)
        {
            string decryptedText;

            // Create a decryptor object using DES instance
            using (ICryptoTransform decryptor = _des.CreateDecryptor())
            {
                // Perform decryption
                byte[] decryptedByte = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                decryptedText = Encoding.UTF8.GetString(decryptedByte);
            }

            return decryptedText;
        }
    }
}
