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

    public async Task<OperationResult<IEnumerable<RequestGetProdutoDto>>> GetItens()
    {
        try
        {
            var response = await _httpClient.GetAsync("Produtos");

            if (!response.IsSuccessStatusCode)
            {
                var erro = await response.Content
                    .ReadFromJsonAsync<OperationResult<IEnumerable<RequestGetProdutoDto>>>();

                return erro ?? OperationResult<IEnumerable<RequestGetProdutoDto>>
                    .Fail("Erro desconhecido na API");
            }
            var produtos = await response.Content
                .ReadFromJsonAsync<IEnumerable<RequestGetProdutoDto>>();
            if (produtos is null || !produtos.Any())
            {
                return OperationResult<IEnumerable<RequestGetProdutoDto>>
                    .Fail("Nenhum produto encontrado");
            }

            return OperationResult<IEnumerable<RequestGetProdutoDto>>.Ok(produtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter os produtos ou acessar a API");

            return OperationResult<IEnumerable<RequestGetProdutoDto>>
                .Fail("Erro inesperado ao chamar a API");
        }
    }

    public async Task<OperationResult<RequestGetProdutoDto>> GetItem(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"Produtos/{id}");
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return OperationResult<RequestGetProdutoDto>.Ok(new RequestGetProdutoDto());
                }
                var produto = await response.Content.ReadFromJsonAsync<RequestGetProdutoDto>();
                if (produto is not null)
                {
                    return OperationResult<RequestGetProdutoDto>.Ok(produto);
                }
                return OperationResult<RequestGetProdutoDto>.Fail("Produto não encontrado");

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
            return OperationResult<RequestGetProdutoDto>.Fail("ERRO INTERNO CONTATAR O SUPORTE, OU TENTE MAIS TARDE!"+ex.Message);
        }
    }
}
