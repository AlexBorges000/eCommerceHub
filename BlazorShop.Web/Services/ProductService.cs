using BlazorShop.Models.Commons;
using BlazorShop.Models.Config;
using BlazorShop.Models.DTOs.ProductDtos;
using BlazorShop.Web.Services.Interfaces;
using System.Net;

namespace BlazorShop.Web.Services;

public class ProductService : IProductService
{
    public HttpClient _httpClient;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IHttpClientFactory factory, ILogger<ProductService> logger)
    {
        _httpClient = factory.CreateClient(HttpConfiguration.Products);
        _logger = logger;
    }

    public async Task<OperationResult<IEnumerable<ProductRequestDto>>> GetItems()
    {
        try
        {
            var response = await _httpClient.GetAsync("Produtos");

            if (!response.IsSuccessStatusCode)
            {
                var erro = await response.Content
                    .ReadFromJsonAsync<OperationResult<IEnumerable<ProductRequestDto>>>();

                return erro ?? OperationResult<IEnumerable<ProductRequestDto>>
                    .Fail("Erro desconhecido na API");
            }
            var products = await response.Content
                .ReadFromJsonAsync<IEnumerable<ProductRequestDto>>();
            if (products is null || !products.Any())
            {
                return OperationResult<IEnumerable<ProductRequestDto>>
                    .Fail("Nenhum produto encontrado");
            }

            return OperationResult<IEnumerable<ProductRequestDto>>.Ok(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter os produtos ou acessar a API");

            return OperationResult<IEnumerable<ProductRequestDto>>
                .Fail("Erro inesperado ao chamar a API");
        }
    }

    public async Task<OperationResult<ProductRequestDto>> GetItem(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"Produtos/{id}");
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return OperationResult<ProductRequestDto>.Ok(new ProductRequestDto());
                }
                var product = await response.Content.ReadFromJsonAsync<ProductRequestDto>();
                if (product is not null)
                {
                    return OperationResult<ProductRequestDto>.Ok(product);
                }
                return OperationResult<ProductRequestDto>.Fail("Produto não encontrado");

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
            return OperationResult<ProductRequestDto>.Fail("ERRO INTERNO CONTATAR O SUPORTE, OU TENTE MAIS TARDE!"+ex.Message);
        }
    }
}
