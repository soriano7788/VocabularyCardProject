using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace VocabularyCard.Util
{
    public class PasswordUtility
    {
        public static string AESEncrytor(string plainText, byte[] key, byte[] iv)
        {
            byte[] data = ASCIIEncoding.ASCII.GetBytes(plainText);
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            string encryptedString = Convert.ToBase64String(aes.CreateEncryptor(key, iv).TransformFinalBlock(data, 0, data.Length));

            return encryptedString;
        }
        public static string AESDecryptor(string encryptedString, byte[] key, byte[] iv)
        {
            byte[] data = Convert.FromBase64String(encryptedString);
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            string decryptedString = ASCIIEncoding.ASCII.GetString(aes.CreateDecryptor(key, iv).TransformFinalBlock(data, 0, data.Length));

            return decryptedString;
        }
    }
}
