using BankApp.Data.Interfaces;
using System.Text;
using BankApp.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using BankApp.Data.DTOs;


namespace BankApp.Core.Services
{
    public class EncryptionHelper : IEncryptionHelper
    {
        private readonly string _encryptionKey;

        public EncryptionHelper(IConfiguration configuration)
        {
            _encryptionKey = configuration.GetSection("EncryptionSettings:EncryptionKey").Value;
            if (string.IsNullOrEmpty(_encryptionKey) || _encryptionKey.Length < 16)
            {
                throw new ArgumentException("Encryption key must be at least 16 characters long.");
            }
        }

        public string HashValue(string value)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(value);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

    }
}


//           
//        }

//        public string Encrypt(string plainText)
//        {
//            using (var aes = Aes.Create())
//            {
//                aes.Key = Encoding.UTF8.GetBytes(_encryptionKey);
//                aes.IV = new byte[16]; //initialiseringsvektor

//                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
//                {
//                    var plainBytes = Encoding.UTF8.GetBytes(plainText);
//                    var encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

//                    return Convert.ToBase64String(encryptedBytes);

//                }
//            }
//        }

//        public string Decrypt(string encryptedText)
//        {
//            using (var aes = Aes.Create())
//            {
//                aes.Key = Encoding.UTF8.GetBytes(_encryptionKey);
//                aes.IV = new byte[16];

//                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
//                {
//                    var encryptedBytes = Convert.FromBase64String(encryptedText);
//                    var plainBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
//                    return Encoding.UTF8.GetString(plainBytes);
//                }
//            }
//        }
//    }
//}

/*A. Kryptering (Encrypt)
   Tar en vanlig textsträng (plainText) och krypterar den med AES.
   Använder en statisk krypteringsnyckel (EncryptionKey) och en initialiseringsvektor (IV) för att skapa krypterad data.
   Returnerar den krypterade texten som en Base64-sträng.
 
B. Dekryptering (Decrypt)
   Tar en krypterad Base64-sträng (encryptedText) och dekrypterar den tillbaka till vanlig text.
   Använder samma krypteringsnyckel och initialiseringsvektor som vid kryptering.*/