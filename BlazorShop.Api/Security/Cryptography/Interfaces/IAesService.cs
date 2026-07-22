namespace BlazorShop.Api.Security.Security.Interfaces;

public interface IAesService
{
    string Encrypt(string text);
    string Decrypt(string encryptedText);
}
