namespace BlazorShop.Api.Security.Security.Interfaces;

public interface IHashService
{
    string GetHash(string text);
}
