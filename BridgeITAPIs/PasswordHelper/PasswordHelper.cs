using System;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace BridgeITAPIs.PasswordHelper;

public class PasswordHelper
{
    private static readonly int SaltSize = 16;
    private static readonly int HashSize = 32;
    private static readonly int Iterations = 10000;

    public static (string password, string salt) HashPassword(string password)
    {
        var salt = new byte[SaltSize];

        using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        var saltString = Convert.ToBase64String(salt);

        var hash = KeyDerivation.Pbkdf2(
            password,
            salt,
            KeyDerivationPrf.HMACSHA256,
            Iterations,
            HashSize);

        return (Convert.ToBase64String(hash), saltString);
    }

    public static bool VerifyPassword(string password, string storedHash, string storedSalt)
    {
        var salt = Convert.FromBase64String(storedSalt);
        var hash = KeyDerivation.Pbkdf2(
            password,
            salt,
            KeyDerivationPrf.HMACSHA256,
            Iterations,
            HashSize);

        return storedHash == Convert.ToBase64String(hash);
    }
}
