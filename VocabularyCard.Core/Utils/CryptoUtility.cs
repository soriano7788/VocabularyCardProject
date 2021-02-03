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
            SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
            byte[] sha256input = sha256.ComputeHash(Encoding.Unicode.GetBytes(input));
            return sha256input;
        }
        private static byte[] GenerateIv(string input)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] md5input = md5.ComputeHash(Encoding.Unicode.GetBytes(input));
            return md5input;
        }
    }
}
