using System.Security.Cryptography;
using System.Text;

namespace AESCryptographyDemo
{
    internal class Program
    {
        /// <summary>
        /// Symmetric algorithm AES Implementation using System.Security.Cryptography class
        /// </summary>
        static void AESDemo()
        {
            // Create an instance of the class
            Aes objAes = Aes.Create();

            // set the key size and the mode
            objAes.KeySize = 256;
            objAes.Mode = CipherMode.CBC;

            // Generate a random key and initialization vector
            objAes.GenerateKey();
            objAes.GenerateIV();

            // Input text
            string plainText = "Hello Deep.";
            byte[] cipherText;

            // AES Encryption
            using (ICryptoTransform encryptor = objAes.CreateEncryptor())
            {
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                cipherText = encryptor.TransformFinalBlock(plainTextBytes, 0, plainTextBytes.Length);
            }

            string decryptedText;

            // AES Decyrption
            using (ICryptoTransform decryptor = objAes.CreateDecryptor())
            {
                byte[] decryptedByte = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);
                decryptedText = Encoding.UTF8.GetString(decryptedByte);
            }

            // Output :- 
            Console.WriteLine($"Plain text :- {plainText}");
            Console.WriteLine($"Cipher text :- {Convert.ToBase64String(cipherText)}");
            Console.WriteLine($"Decrypted text :- {decryptedText}");
        }

        /// <summary>
        /// DES Encryption & Decryption Demo
        /// </summary>
        static void DESDemo()
        {
            // Creating a des instance od DES Class
            DES objDes = DES.Create();

            // Generating a key and initialization vector for DES
            objDes.GenerateIV();
            objDes.GenerateKey();

            // Input string
            string plainText = "Hello Deep.";
            byte[] cipherText;

            // DES Encryption
            using (ICryptoTransform encryptor = objDes.CreateEncryptor())
            {
                byte[] plaintextBytes = Encoding.UTF8.GetBytes(plainText);
                cipherText = encryptor.TransformFinalBlock(plaintextBytes, 0, plaintextBytes.Length);
            }

            string decryptedText;

            // DES Decyrption
            using (ICryptoTransform decryptor = objDes.CreateDecryptor())
            {
                byte[] decryptedByte = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);
                decryptedText = Encoding.UTF8.GetString(decryptedByte);
            }

            // Output :- 
            Console.WriteLine($"Plain text :- {plainText}");
            Console.WriteLine($"Cipher text :- {Convert.ToBase64String(cipherText)}");
            Console.WriteLine($"Decrypted text :- {decryptedText}");
        }

        static void RSADemo()
        {
            UnicodeEncoding ByteConverter = new UnicodeEncoding();
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();

            byte[] plainText, cipherText;
            plainText = ByteConverter.GetBytes("Hello Deep.");

            // RSA Encryption
            cipherText = RSAEncryption(plainText, RSA.ExportParameters(false), false);
            Console.WriteLine("Cipher text :- " + ByteConverter.GetString(cipherText));

            // RSA Decryption
            byte[] decryptedText = RSADecryption(cipherText, RSA.ExportParameters(true), false);
            Console.WriteLine("Decrypted text :- " + ByteConverter.GetString(decryptedText));
        }

        /// <summary>
        /// RSA Algorithm Encryption Process
        /// </summary>
        /// <param name="plainText">Plaintext to be converted into ciphertext</param>
        /// <param name="rSAParameters"></param>
        /// <param name="v"></param>
        /// <returns>Ciphertext</returns>
        private static byte[]? RSAEncryption(byte[] plainText, RSAParameters rSAParameters, bool v)
        {
            try
            {
                byte[] encryptedData;
                using(RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(rSAParameters);
                    encryptedData = RSA.Encrypt(plainText, false);

                    return encryptedData;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// RSA Algorithms Decyrption Process
        /// </summary>
        /// <param name="Ciphertext">Cipher text</param>
        /// <param name="rSAParameters">True</param>
        /// <param name="v"></param>
        /// <returns>Decrypted text</returns>
        private static byte[]? RSADecryption(byte[] Ciphertext, RSAParameters rSAParameters, bool v)
        {
            try
            {
                byte[] decryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(rSAParameters);
                    decryptedData = RSA.Decrypt(Ciphertext, v);

                    return decryptedData;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        static void Main(string[] args)
        {

        }
    }
}