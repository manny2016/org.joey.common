using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Org.Joey.Common
{
    public static class CryptographyExtension
    {
        #region MD5 ...
        public static string MD5(this string plaintext)
        {
            if (string.IsNullOrEmpty(plaintext)) { return string.Empty; }
            var buffer = Encoding.Unicode.GetBytes(plaintext);
            return buffer.MD5();
        }

        public static string GetFileMD5Checksum(this string path)
        {
            if (File.Exists(path))
            {
                using (var stream = File.OpenRead(path))
                {
                    return stream.MD5();
                }
            }
            else
            {
                return null;
            }
        }

        public static string MD5(this byte[] bytes)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                var result = md5.ComputeHash(bytes);
                return BitConverter.ToString(result).Replace("-", "").ToLower();
            }
        }
        public static string MD5(this Stream stream)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                var result = md5.ComputeHash(stream);
                return BitConverter.ToString(result).Replace("-", "").ToLower();
            }
        }
        #endregion

        #region HMAC-SHA1 ...
        public static byte[] HMACSHA1(this string plaintext, Encoding encoding, byte[] key)
        {
            if (string.IsNullOrEmpty(plaintext)) { return new byte[0]; }
            return encoding.GetBytes(plaintext).HMACSHA1(key);
        }

        public static byte[] HMACSHA1(this byte[] bytes, byte[] key)
        {
            using (var hasher = new HMACSHA1(key))
            {
                return hasher.ComputeHash(bytes);
            }
        }
        #endregion
    }
}
