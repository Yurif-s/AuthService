using AuthService.Domain.Security.Cryptography;
using BC = BCrypt.Net.BCrypt;

namespace AuthService.Infrastructure.Security.Cryptography;

public class PasswordHasher : IPasswordHasher
{
    public string Hash(string password) => BC.HashPassword(password);

    public bool Verify(string password, string passwordHash) => BC.Verify(password, passwordHash);
}
