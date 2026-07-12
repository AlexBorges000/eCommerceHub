namespace BlazorShop.Api.Services.Security.Interfaces;

public interface IAesService
{
    string Encrypt(string text);
    string Decrypt(string encryptedText);
}
