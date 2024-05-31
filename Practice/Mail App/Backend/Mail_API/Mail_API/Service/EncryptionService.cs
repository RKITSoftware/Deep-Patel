using Mail_API.Interface;
using System.Security.Cryptography;
using System.Text;

namespace Mail_API.Service
{
    public class EncryptionService : IEncryptionService
    {
        private readonly Aes _aes;

        public EncryptionService(IConfiguration configuration)
        {
            _aes = Aes.Create();
            _aes.IV = Encoding.UTF8.GetBytes(configuration.GetValue<string>("AES:IV"));
            _aes.Key = Encoding.UTF8.GetBytes(configuration.GetValue<string>("AES:Key"));
        }

        public string Encrypt(string plaintext)
        {
            ICryptoTransform encryptor = _aes.CreateEncryptor(_aes.Key, _aes.IV);

            using MemoryStream msEncrypt = new();
            using (CryptoStream csEncrypt = new(msEncrypt,
                                                encryptor,
                                                CryptoStreamMode.Write))
            {
                using StreamWriter swEncrypt = new(csEncrypt);
                swEncrypt.Write(plaintext);
            }

            return Convert.ToBase64String(msEncrypt.ToArray());
        }
    }
}
