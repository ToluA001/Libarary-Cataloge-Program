namespace Libarary_Cataloge_Program.Model;

using System;
using System.Security.Cryptography;
public class User
{
    public int Id {get; set;}
    public string Username {get; set;}
    
    public byte[] PasswordHash { get; private set; }
    public byte[] PasswordSalt { get; private set; }
    
    public bool IsLoggedIn {get; set;}

    /// <summary>
    /// Encrypt the given password.
    /// </summary>
    /// <param name="password">String</param>
    public void SetPassword(String password)
    {
        using (var rang = RandomNumberGenerator.Create())
        {
            PasswordSalt = new byte[16];
            rang.GetBytes(PasswordSalt);
        }

        using (var pbkdf2 = new Rfc2898DeriveBytes(password, PasswordSalt, 100_000, HashAlgorithmName.SHA256))
        {
            PasswordHash = pbkdf2.GetBytes(32);
        }
    }

    /// <summary>
    /// Verify the given password.
    /// </summary>
    /// <param name="password">String</param>
    /// <returns></returns>
    public bool VerifyPassword(String password)
    {
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, PasswordSalt, 100_000, HashAlgorithmName.SHA256))
        {
            byte[] enteredHash = pbkdf2.GetBytes(32);
            return CryptographicOperations.FixedTimeEquals(PasswordHash, enteredHash);
        }
    }
    
    public User(string username)
    {
        Username = username;
    }
    
}