using BlazorShop.Models.Config;
using BlazorShop.Models.DTOs;
using BlazorShop.Web.Services.Interfaces;
using System.Net;

namespace BlazorShop.Web.Services;

public class ProdutoService : IProdutoService
{
    public HttpClient _httpClient;
    private readonly ILogger<ProdutoService> _logger;

    public ProdutoService(IHttpClientFactory factory, ILogger<ProdutoService> logger)
    {
        _httpClient = factory.CreateClient(HttpConfiguration.Produtos);
        _logger = logger;
    }

    public async Task<IEnumerable<ProdutoDto>> GetItens()
    {
        try
        {
            var produtosDto = await _httpClient.
            GetFromJsonAsync<IEnumerable<ProdutoDto>>("/api/produtos");
            return produtosDto;
        }
        catch (Exception)
        {
            _logger.LogError("Erro ao obter os produtos ou acessar a API");
            throw;
        }
    }

    public async Task<ProdutoDto> GetItem(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/produtos/{id}");
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return default(ProdutoDto);
                }
                return await response.Content.ReadFromJsonAsync<ProdutoDto>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro ao obter o produto pelo id {Id} - {Message}", id, message);
                throw new Exception($"Status Code: {response.StatusCode} - {message}");
            }
            
        }
        catch (Exception)
        {
            _logger.LogError("Erro ao obter o produto ou acessar a API");
            throw;
        }
    }
}
