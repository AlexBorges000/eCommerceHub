using BlazorShop.Models.Commons;
using BlazorShop.Models.Config;
using BlazorShop.Models.DTOs.CarrinhoDtos;
using BlazorShop.Models.DTOs.ProdutoDtos;
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

    public async Task<OperationResult<IEnumerable<ProdutoDto>>> GetItens()
    {
        try
        {
            var produtosDto = await _httpClient.
            GetAsync("Produtos");

            var produtos = await produtosDto.Content.ReadFromJsonAsync<IEnumerable<ProdutoDto>>();
            if (produtos is not null)
            {
                return OperationResult<IEnumerable<ProdutoDto>>.Ok(produtos);
            }
            
            return OperationResult<IEnumerable<ProdutoDto>>.Fail("NENHUM PRODUTO ENCONTRADO");
        }
        catch (Exception)
        {
            _logger.LogError("Erro ao obter os produtos ou acessar a API");
            throw;
        }
    }

    public async Task<OperationResult<ProdutoDto>> GetItem(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"Produtos/{id}");
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return OperationResult<ProdutoDto>.Ok(new ProdutoDto());
                }
                var produto = await response.Content.ReadFromJsonAsync<ProdutoDto>();
                if (produto is not null)
                {
                    return OperationResult<ProdutoDto>.Ok(produto);
                }
                return OperationResult<ProdutoDto>.Fail("Produto não encontrado");

            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro ao obter o produto pelo id {Id} - {Message}", id, message);
                throw new Exception($"Status Code: {response.StatusCode} - {message}");
            }

        }
        catch (Exception ex)
        {
            return OperationResult<ProdutoDto>.Fail("ERRO INTERNO CONTATAR O SUPORTE, OU TENTE MAIS TARDE!"+ex.Message);
        }
    }
}
