using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using SellSpasibo.Core.Interfaces;
using SellSpasibo.Core.Options;

namespace SellSpasibo.Core.Services
{
    public class StringCrypt : IStringCrypt
    {
        private const string ConstPartKey = "47F2E988-83CF-4439-8419-2DD6E00E1801";
        private static readonly byte[] Salt = {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76};

        private readonly string _key;
        
        public StringCrypt(IOptions<StringCryptOptions> options)
        {
            _key = options.Value.Key + ConstPartKey;
        }

        public string Encrypt(string value)
        {
            var clearBytes = Encoding.Unicode.GetBytes(value);
            using var encryptor = Aes.Create();
            var pdb = new Rfc2898DeriveBytes(_key, Salt);
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(clearBytes, 0, clearBytes.Length);
                cs.Close();
            }
            value = Convert.ToBase64String(ms.ToArray());

            return value;
        }
        public string Decrypt(string hash)
        {
            var encryptionKey = _key;
            hash = hash.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(hash);
            using var encryptor = Aes.Create();
            var pdb = new Rfc2898DeriveBytes(encryptionKey, Salt);
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
            {
                cs.Write(cipherBytes, 0, cipherBytes.Length);
                cs.Close();
            }
            hash = Encoding.Unicode.GetString(ms.ToArray());

            return hash;
        }
    }
}