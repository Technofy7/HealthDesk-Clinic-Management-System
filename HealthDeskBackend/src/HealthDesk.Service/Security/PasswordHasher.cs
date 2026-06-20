using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace HealthDesk.Service.Security
{
    public static class PasswordHasher
    {
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int Iterations = 100_000;
        private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA256;

        // Hashing the given password
        public static string Hash(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var key = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, KeySize);
            return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(key)}";
        }

        //Verifying the password
        public static bool Verify(string password, string hashed)
        {
            var parts = hashed.Split('.');
            if (parts.Length != 2) return false;

            var salt = Convert.FromBase64String(parts[0]);
            var key = Convert.FromBase64String(parts[1]);
            var keyToCheck = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, KeySize);

            return CryptographicOperations.FixedTimeEquals(keyToCheck, key);
        }
    }
}
