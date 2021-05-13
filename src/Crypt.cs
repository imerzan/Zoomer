using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace Zoomer
{
    internal static class Crypt
    {
        private const string Password = @"EX1^64P#NNYv?^p:Els-4a6>;EAlYU";
        private static HashSet<byte[]> UsedHashes = new HashSet<byte[]>(new bytearraycomparer());

        public static byte[] Encrypt(byte[] _plaintext)
        {
            using (var psk = new Rfc2898DeriveBytes(Password, 8, 1000))
            using (var aes = new AesManaged() { Mode = CipherMode.CBC, KeySize = 256 })
            using (var ms = new MemoryStream())
            {
                aes.Padding = PaddingMode.PKCS7;
                aes.BlockSize = 128;
                byte[] _key = psk.GetBytes(96);
                aes.Key = _key.Take(32).ToArray();
                aes.GenerateIV();
                ms.Write(psk.Salt, 0, psk.Salt.Length); // 8 bytes
                ms.Write(aes.IV, 0, aes.IV.Length); // 16 bytes
                using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(_plaintext, 0, _plaintext.Length);
                }
                using (var msOut = new MemoryStream())
                using (var hmac = new HMACSHA512(_key.Skip(32).Take(64).ToArray()))
                {
                    byte[] payload = ms.ToArray();
                    byte[] computedHash = hmac.ComputeHash(payload);
                    msOut.Write(computedHash, 0, computedHash.Length); // 64 bytes
                    msOut.Write(payload, 0, payload.Length);
                    return msOut.ToArray();
                }
            }
        }

        public static byte[] Decrypt(byte[] _ciphertext)
        {
            byte[] _hash = _ciphertext.Take(64).ToArray();
            byte[] _payload = _ciphertext.Skip(64).ToArray();
            using (var psk = new Rfc2898DeriveBytes(Password, _payload.Take(8).ToArray(), 1000))
            using (var aes = new AesManaged() { Mode = CipherMode.CBC, KeySize = 256 })
            using (var ms = new MemoryStream())
            {
                aes.Padding = PaddingMode.PKCS7;
                aes.BlockSize = 128;
                byte[] _key = psk.GetBytes(96);
                aes.Key = _key.Take(32).ToArray();
                aes.IV = _payload.Skip(8).Take(16).ToArray();
                using (var hmac = new HMACSHA512(_key.Skip(32).Take(64).ToArray()))
                {
                    byte[] computedHash = hmac.ComputeHash(_payload);
                    for (int i = 0; i < _hash.Length; i++)
                    {
                        if (_hash[i] != computedHash[i]) throw new CryptographicException("Computed hash value does not match!");
                    }
                    if (UsedHashes.Contains(_hash)) throw new CryptographicException("Hash has already been used! Possible replay attack.");
                    UsedHashes.Add(_hash);
                }
                using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(_payload, 24, _payload.Length - 24);
                }
                return ms.ToArray();
            }
        }
    }
    public class bytearraycomparer : IEqualityComparer<byte[]> // Compare if two byte arrays are equal
    {
        public bool Equals(byte[] a, byte[] b)
        {
            if (a.Length != b.Length) return false;
            for (int i = 0; i < a.Length; i++)
                if (a[i] != b[i]) return false;
            return true;
        }
        public int GetHashCode(byte[] a)
        {
            uint b = 0;
            for (int i = 0; i < a.Length; i++)
                b = ((b << 23) | (b >> 9)) ^ a[i];
            return unchecked((int)b);
        }
    }
}