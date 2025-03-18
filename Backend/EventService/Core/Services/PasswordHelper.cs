
using System;
using System.Security.Cryptography;
using System.Text;

namespace Eventify.Backend.UserService.Core.Services
{
    public class PasswordHelper
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;

        public string HashPassword(string password)
        {
            byte[] salt = new byte[SaltSize];
            RandomNumberGenerator.Fill(salt);

            using var hmac = new HMACSHA256(salt);
            byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            byte[] hashBytes = new byte[SaltSize + HashSize];
            Buffer.BlockCopy(salt, 0, hashBytes, 0, SaltSize);
            Buffer.BlockCopy(hash, 0, hashBytes, SaltSize, HashSize);

            return Convert.ToBase64String(hashBytes);
        }

       public bool VerifyPassword(string password, string hashedPassword)
        {
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);
            byte[] salt = new byte[SaltSize];
            Buffer.BlockCopy(hashBytes, 0, salt, 0, SaltSize);

            byte[] storedHash = new byte[HashSize];
            Buffer.BlockCopy(hashBytes, SaltSize, storedHash, 0, HashSize);

            using var hmac = new HMACSHA256(salt);
            byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return CompareHashes(storedHash, computedHash);
        }

        private bool CompareHashes(byte[] storedHash, byte[] computedHash)
        {
            for (int i = 0; i < storedHash.Length; i++)
            {
                if (storedHash[i] != computedHash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}







