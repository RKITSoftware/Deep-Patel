using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RinjdaelEncrDecrDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string originalText = "Hello Deep Patel";
            string key = "0123456789abcdef0123456789abcdef"; // 256-bit key
            string iv = "0123456789abcdef"; // 128-bit IV

            // Encrypt
            byte[] encryptedData = Encrypt(originalText, key, iv);
            Console.WriteLine("Encrypted text :- " + Convert.ToBase64String(encryptedData));

            string decryptedText = Decrypt(encryptedData, key, iv);
            Console.WriteLine("Decrypted text :- " + decryptedText);
        }

        private static string Decrypt(byte[] encryptedData, string key, string iv)
        {
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Encoding.UTF8.GetBytes(key);
                rijAlg.IV = Encoding.UTF8.GetBytes(iv);

                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(encryptedData))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        private static byte[] Encrypt(string originalText, string key, string iv)
        {
            using (RijndaelManaged rijAlgo = new RijndaelManaged())
            {
                rijAlgo.Key = Encoding.UTF8.GetBytes(key);
                rijAlgo.IV = Encoding.UTF8.GetBytes(iv);

                ICryptoTransform encryptor = rijAlgo.CreateEncryptor(rijAlgo.Key, rijAlgo.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(originalText);
                        }
                    }

                    return msEncrypt.ToArray();
                }
            }
        }
    }
}
