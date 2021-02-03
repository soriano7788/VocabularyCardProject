using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Collections;
using System.Globalization;
using System.IO;

namespace VocabularyCard.Core.Utils
{
    public class AesHelper
    {
        public static string Encrypt(string input, byte[] key, byte[] iv)
        {
            string encrypt = string.Empty;
            byte[] data = Encoding.Unicode.GetBytes(input);
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                ICryptoTransform aesEncryptor = aes.CreateEncryptor(key, iv);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, aesEncryptor, CryptoStreamMode.Write))
                    {
                        csEncrypt.Write(data, 0, data.Length);
                        csEncrypt.FlushFinalBlock();
                        encrypt = Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
            return encrypt;
        }
        public static string Decrypt(string input, byte[] key, byte[] iv)
        {
            string decrypt = string.Empty;
            byte[] data = Convert.FromBase64String(input);
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(key, iv), CryptoStreamMode.Write))
                    {
                        cs.Write(data, 0, data.Length);
                        cs.FlushFinalBlock();
                        decrypt = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
            }
            return decrypt;
        }
    }
}
