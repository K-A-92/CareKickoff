using System.Security.Cryptography;
using System.Text;

namespace CareApi.Services;

public class HashingService
{
    private readonly string _secretKey;

    public HashingService()
    {
        _secretKey = Environment.GetEnvironmentVariable("SECRET_KEY") ?? "DefaultSecret";
    }

    public string HashWithSecretKey(string input)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_secretKey));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(input));
        return Convert.ToBase64String(hash);
    }
}