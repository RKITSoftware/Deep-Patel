using System.Security.Cryptography;
using System.Text;

namespace AESCryptographyDemo
{
    public class Program
    {
        /// <summary>
        /// RSA Encryption and Decryption Demo
        /// </summary>
        public void RSADemo()
        {
            UnicodeEncoding ByteConverter = new UnicodeEncoding();
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();

            byte[] plainText, cipherText;
            plainText = ByteConverter.GetBytes("Hello Deep.");

            // RSA Encryption
            cipherText = RSAEncryption(plainText, RSA.ExportParameters(false), false);
            Console.WriteLine("Cipher text :- " + Convert.ToBase64String(cipherText));

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
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(rSAParameters);
                    encryptedData = RSA.Encrypt(plainText, false);

                    return encryptedData;
                }
            }
            catch (Exception ex)
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
            Program objProgram = new Program();

            string plainText = "Hello, Deep Patel.";
            byte[] cipherBytes;
            string decryptedText;

            // AES Demo
            AESAlgo objAes = new AESAlgo();
            cipherBytes = objAes.Encrypt(plainText);
            decryptedText = objAes.Decrypt(cipherBytes);

            // Output :- 
            Console.WriteLine($"Plain text :- {plainText} \n");

            Console.WriteLine($"AES Cipher text :- {Convert.ToBase64String(cipherBytes)}");
            Console.WriteLine($"AES Decrypted text :- {decryptedText} \n");

            // DES Demo
            DESAlgo objDes = new DESAlgo();
            cipherBytes = objDes.Encrypt(plainText);
            decryptedText = objDes.Decrypt(cipherBytes);

            // DES Output :-
            Console.WriteLine($"DES Cipher text :- {Convert.ToBase64String(cipherBytes)}");
            Console.WriteLine($"DES Decrypted text :- {decryptedText} \n");

            objProgram.RSADemo();
        }
    }
}
