using System.Security.Cryptography;
using System.Text;

namespace CareApi.Services
{
    public class HashingService(IConfiguration configuration)
    {
        private readonly string secretKey = configuration.GetValue<string>("SECRET_KEY") ?? "DefaultSecret";


        public string HashWithSecretKey(string input)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(hash);
        }
    }
}