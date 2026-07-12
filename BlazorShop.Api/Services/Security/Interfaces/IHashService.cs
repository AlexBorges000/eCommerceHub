namespace BlazorShop.Api.Services.Security.Interfaces;

public interface IHashService
{
    string GetHash(string text);
}
