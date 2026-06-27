using BlazorShop.Models.Commons;
using BlazorShop.Models.Config;
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
            var response = await _httpClient.GetAsync("Produtos");

            if (!response.IsSuccessStatusCode)
            {
                var erro = await response.Content
                    .ReadFromJsonAsync<OperationResult<IEnumerable<ProdutoDto>>>();

                return erro ?? OperationResult<IEnumerable<ProdutoDto>>
                    .Fail("Erro desconhecido na API");
            }
            var produtos = await response.Content
                .ReadFromJsonAsync<IEnumerable<ProdutoDto>>();
            if (produtos is null || !produtos.Any())
            {
                return OperationResult<IEnumerable<ProdutoDto>>
                    .Fail("Nenhum produto encontrado");
            }

            return OperationResult<IEnumerable<ProdutoDto>>.Ok(produtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter os produtos ou acessar a API");

            return OperationResult<IEnumerable<ProdutoDto>>
                .Fail("Erro inesperado ao chamar a API");
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
