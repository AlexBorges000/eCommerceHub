using BlazorShop.Api.Security.Security.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace BlazorShop.Api.Security.Security
{
    public class HashService : IHashService
    {
        private readonly byte[] _key;
        public HashService(IConfiguration configuration)
        {
            var key = configuration["Hash:Key"] 
                ?? throw new InvalidOperationException("Hash Key não configurada");
            _key = Convert.FromBase64String(key);
        }
        public string GetHash(string text)
        {
            using var hmac = new HMACSHA256(_key);
            byte[] data = Encoding.UTF8.GetBytes(text);
            byte[] result = hmac.ComputeHash(data);

            return Convert.ToBase64String(result);
        }
    }
}
