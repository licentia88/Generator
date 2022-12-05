using System;
using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Generator.Shared.Services
{
    public static class CryptoService
    {
        public static string HashKey { get; set; }

        

        /// <summary>
        /// Encrypts Data
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Encrypt(string input)
        {
            var clearBytes = Encoding.Unicode.GetBytes(input);

            using var encryptor = Aes.Create();

            var pdb = new Rfc2898DeriveBytes(HashKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 }, 1000, HashAlgorithmName.SHA256);
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write);

            cs.Write(clearBytes, 0, clearBytes.Length);
            cs.Close();

            input = Convert.ToBase64String(ms.ToArray());

            return input;
        }

        /// <summary>
        /// Decrypts Data
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Decrypt(string cipherText)
        {
            var cipherBytes = Convert.FromBase64String(cipherText ?? "");
            using var encryptor = Aes.Create();
            var pdb = new Rfc2898DeriveBytes(HashKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 }, 1000, HashAlgorithmName.SHA256);
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using var ms = new MemoryStream();
            var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(cipherBytes, 0, cipherBytes.Length);
            cs.Close();
            cipherText = Encoding.Unicode.GetString(ms.ToArray());
            return cipherText;
        }

        /// <summary>
        /// Creates Key to be used for Encryptions or to store in db
        /// </summary>
        /// <returns></returns>
        public static string CreateKey()
        {
            using var rijndael = Aes.Create();
            rijndael.GenerateKey();
            return Convert.ToBase64String(rijndael.Key);
        }
    }


    
}

