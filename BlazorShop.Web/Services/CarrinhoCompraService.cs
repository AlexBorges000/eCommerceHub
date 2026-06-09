using BlazorShop.Models.Commons;
using BlazorShop.Models.Config;
using BlazorShop.Models.DTOs;
using BlazorShop.Web.Services.Interfaces;
using System.Net;

namespace BlazorShop.Web.Services;

public class CarrinhoCompraService : ICarrinhoCompraService
{
    private readonly HttpClient _httpClient;

    public CarrinhoCompraService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient(HttpConfiguration.Compras);
    }

    public async Task<CarrinhoItemDto> AdicionaItem(CarrinhoItemAdicionaDto carrinhoItemAdicionaDto)
    {
        try
        {
            var response = await _httpClient
                .PostAsJsonAsync<CarrinhoItemAdicionaDto>("api/CarrinhoCompra", carrinhoItemAdicionaDto);
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return default(CarrinhoItemDto);
                }
                return await response.Content.ReadFromJsonAsync<CarrinhoItemDto>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http Status Code: {response.StatusCode}, Message: {message}");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<CarrinhoItemDto> DeleteItem(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/CarrinhoCompra/{id}");
            if (response.IsSuccessStatusCode)
            {
                
                return await response.Content.ReadFromJsonAsync<CarrinhoItemDto>();
            }
            return default(CarrinhoItemDto);
        }
        catch (Exception)
        {
            throw;
        }
        
    }

    public async Task<OperationResult<List<CarrinhoItemDto>>> GetItensReturnsResult(int usuarioId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/CarrinhoCompra/{usuarioId}/GetItens");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return OperationResult<List<CarrinhoItemDto>>.Ok(new List<CarrinhoItemDto>());
                }
                var carrinhos = await response.Content.ReadFromJsonAsync<List<CarrinhoItemDto>>();
                if (carrinhos != null)
                {
                    return OperationResult<List<CarrinhoItemDto>>.Ok(carrinhos);
                }
                return OperationResult<List<CarrinhoItemDto>>.Fail("NÃO ENCONTRADO NENHUM VALOR");
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http Status Code: {response.StatusCode}, Message: {message}");
            }
        }
        catch (Exception)
        {
            return OperationResult<List<CarrinhoItemDto>>.Fail("ERRO INTERNO! CONTATE O SUPORTE DE SISTEMA!");
        }
    }

    public async Task<List<CarrinhoItemDto>> GetItens(int usuarioId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/CarrinhoCompra/{usuarioId}/GetItens");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    
                }
                var carrinhos = await response.Content.ReadFromJsonAsync<List<CarrinhoItemDto>>();
                if (carrinhos != null)
                {
                    return carrinhos;
                }
                throw new Exception("NÃO ENCONTRADO");
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http Status Code: {response.StatusCode}, Message: {message}");
            }

        }
        catch (Exception)
        {
            throw;
        }
    }
}
