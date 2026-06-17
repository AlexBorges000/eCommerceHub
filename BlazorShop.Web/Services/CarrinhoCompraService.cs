using BlazorShop.Models.Commons;
using BlazorShop.Models.Config;
using BlazorShop.Models.DTOs.CarrinhoDtos;
using BlazorShop.Web.Services.Interfaces;
using System.Net;
using System.Text;
using System.Text.Json;
namespace BlazorShop.Web.Services;

public class CarrinhoCompraService : ICarrinhoCompraService
{
    private readonly HttpClient _httpClient;

    public CarrinhoCompraService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient(HttpConfiguration.Compras);
    }

    public async Task<OperationResult<CarrinhoItemDto>> AdicionaItem(CarrinhoItemAdicionaDto carrinhoItemAdicionaDto)
    {
        try
        {
            var response = await _httpClient
                .PostAsJsonAsync<CarrinhoItemAdicionaDto>("CarrinhoCompra", carrinhoItemAdicionaDto);
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return OperationResult<CarrinhoItemDto>.Ok(new CarrinhoItemDto());
                }
                var postProduto = await response.Content.ReadFromJsonAsync<CarrinhoItemDto>();
                if (postProduto != null)
                {
                    return OperationResult<CarrinhoItemDto>.Ok(new CarrinhoItemDto());
                }
                return OperationResult<CarrinhoItemDto>.Fail("NENHUM ITEM ENCONTRADO");
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http Status Code: {response.StatusCode}, Message: {message}");
            }
        }
        catch (Exception ex)
        {
            return OperationResult<CarrinhoItemDto>.Fail("ERRO INTERNO! CONTATE O SUPORTE DE SISTEMA!" + ex.Message);
        }
    }

    public async Task<OperationResult<CarrinhoItemDto>> DeleteItem(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"CarrinhoCompra/{id}");
            if (response.IsSuccessStatusCode)
            {
                return OperationResult<CarrinhoItemDto>.Ok(new CarrinhoItemDto());
            }
            return OperationResult<CarrinhoItemDto>.Ok(new CarrinhoItemDto());
        }
        catch (Exception ex)
        {
            return OperationResult<CarrinhoItemDto>.Fail("ERRO INTERNO! CONTATE O SUPORTE DE SISTEMA!" + ex.Message);
        }

    }

    public async Task<OperationResult<List<CarrinhoItemDto>>> GetItensReturnsResult(int usuarioId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"CarrinhoCompra/{usuarioId}/GetItens");
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return OperationResult<List<CarrinhoItemDto>>.Ok(new List<CarrinhoItemDto>());
                }
                var carrinhos = await response.Content.ReadFromJsonAsync<OperationResult<List<CarrinhoItemDto>>>();
                if (carrinhos is not null)
                {
                    return OperationResult<List<CarrinhoItemDto>>.Ok(carrinhos.Value);
                }
                return OperationResult<List<CarrinhoItemDto>>.Fail("NÃO ENCONTRADO NENHUM VALOR");
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http Status Code: {response.StatusCode}, Message: {message}");
            }
        }
        catch (Exception ex)
        {
            return OperationResult<List<CarrinhoItemDto>>.Fail("ERRO INTERNO! CONTATE O SUPORTE DE SISTEMA!" + ex.Message);
        }
    }

    public async Task<OperationResult<List<CarrinhoItemDto>>> GetItens(int usuarioId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"CarrinhoCompra/{usuarioId}/GetItens");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return OperationResult<List<CarrinhoItemDto>>.Ok(new List<CarrinhoItemDto>());
                }
                var carrinhos = await response.Content.ReadFromJsonAsync<List<CarrinhoItemDto>>();
                if (carrinhos != null)
                {
                    return OperationResult<List<CarrinhoItemDto>>.Ok(new List<CarrinhoItemDto>());
                }
                return OperationResult<List<CarrinhoItemDto>>.Fail("NÃO ENCONTRADO NENHUM VALOR");

            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http Status Code: {response.StatusCode}, Message: {message}");
            }

        }
        catch (Exception ex)
        {
            return OperationResult<List<CarrinhoItemDto>>.Fail("ERRO INTERNO CONTATAR O SUPORTE" + ex.Message); ;
        }
    }

    public async Task<OperationResult<CarrinhoItemDto>> AtualizaQuantidade(CarrinhoItemAtualizaQuantidadeDto
                                                carrinhoItemAtualizaQuantidadeDto)
    {
        try
        {
            var jsonRequest = JsonSerializer.Serialize(carrinhoItemAtualizaQuantidadeDto);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");
            var response = await _httpClient.PatchAsync($"CarrinhoCompra/{carrinhoItemAtualizaQuantidadeDto.CarrinhoItemId}", content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<OperationResult<CarrinhoItemDto>>();
            }
            return OperationResult<CarrinhoItemDto>.Fail("Ocorreu Um Erro" + response.RequestMessage);
        }
        catch (Exception)
        {
            throw;
        }

    }
}






