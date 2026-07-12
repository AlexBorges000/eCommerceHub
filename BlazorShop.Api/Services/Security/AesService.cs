using BlazorShop.Api.Services.Security.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace BlazorShop.Api.Services.Security;

public class AesService : IAesService
{

    private readonly byte[] _key;
    public AesService(IConfiguration configuration)
    {
        var key = configuration["Encryption:Key"]
            ?? throw new InvalidOperationException("Encryption key not configured.");

        _key = Convert.FromBase64String(key);
    }

    public string Encrypt(string text)
    {
        byte[] nonce = RandomNumberGenerator.GetBytes(AesGcm.NonceByteSizes.MaxSize);
        byte[] byteText = Encoding.UTF8.GetBytes(text);
        byte[] cipherText = new byte[byteText.Length];
        byte[] tag = new byte[AesGcm.TagByteSizes.MaxSize];

        using var aes = new AesGcm(_key, AesGcm.TagByteSizes.MaxSize);

        aes.Encrypt(
            nonce,
            byteText,
            cipherText,
            tag
        );
        return  $"{Convert.ToBase64String(nonce)}." +
                $"{Convert.ToBase64String(tag)}." +
                $"{Convert.ToBase64String(cipherText)}"; ;
    }

    public string Decrypt(string encryptedText)
    {
        var parts = encryptedText.Split(".");
        byte[] nonce = Convert.FromBase64String(parts[0]);
        byte[] tag = Convert.FromBase64String(parts[1]);
        byte[] cipherText = Convert.FromBase64String(parts[2]);
        byte[] plainText = new byte[cipherText.Length];

        using var aes = new AesGcm(_key, AesGcm.TagByteSizes.MaxSize);

        aes.Decrypt(
            nonce,
            cipherText,
            tag,
            plainText     
        );

        return Encoding.UTF8.GetString(plainText); ;
    }
}
