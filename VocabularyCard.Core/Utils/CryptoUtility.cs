using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace VocabularyCard.Core.Utils
{
    public class CryptoUtility
    {
        private const string _key = "hello world";
        public static string Encrypt(string input)
        {
            return AesHelper.Encrypt(input, GenerateKey(_key), GenerateIv(_key));
        }
        public static string Decrypt(string input)
        {
            return AesHelper.Decrypt(input, GenerateKey(_key), GenerateIv(_key));
        }

        private static byte[] GenerateKey(string input)
        {
            return HashHelper.ComputeSHA256Hash(Encoding.Unicode.GetBytes(input));
        }
        private static byte[] GenerateIv(string input)
        {
            return HashHelper.ComputeMD5Hash(Encoding.Unicode.GetBytes(input));
        }
    }
}
