using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace VocabularyCard.Core.Utils
{
    public class HashHelper
    {
        public static string ComputeSHA256Hash(string input)
        {
            byte[] data = Encoding.Unicode.GetBytes(input);
            byte[] sha256Data = ComputeSHA256Hash(data);
            return BitConverter.ToString(sha256Data).Replace("-", string.Empty).ToLower();
        }
        public static byte[] ComputeSHA256Hash(byte[] input)
        {
            SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
            byte[] sha256output = sha256.ComputeHash(input);
            return sha256output;
        }

        public static string ComputeMD5Hash(string input)
        {
            byte[] data = Encoding.Unicode.GetBytes(input);
            byte[] md5Data = ComputeMD5Hash(data);
            return BitConverter.ToString(md5Data).Replace("-", string.Empty).ToLower();
        }
        public static byte[] ComputeMD5Hash(byte[] input)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] md5output = md5.ComputeHash(input);
            return md5output;
        }
    }
}
