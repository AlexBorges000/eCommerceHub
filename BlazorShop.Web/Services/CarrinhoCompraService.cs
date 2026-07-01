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

    public async Task<OperationResult<RequestGetCarrinhoItemDto>> AdicionaItem(RequestCarrinhoItemAdicionaDto carrinhoItemAdicionaDto)
    {
        try
        {
            var response = await _httpClient
                .PostAsJsonAsync<RequestCarrinhoItemAdicionaDto>("CarrinhoCompra", carrinhoItemAdicionaDto);
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return OperationResult<RequestGetCarrinhoItemDto>.Ok(new RequestGetCarrinhoItemDto());
                }
                var postProduto = await response.Content.ReadFromJsonAsync<RequestGetCarrinhoItemDto>();
                if (postProduto != null)
                {
                    return OperationResult<RequestGetCarrinhoItemDto>.Ok(new RequestGetCarrinhoItemDto());
                }
                return OperationResult<RequestGetCarrinhoItemDto>.Fail("NENHUM ITEM ENCONTRADO");
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http Status Code: {response.StatusCode}, Message: {message}");
            }
        }
        catch (Exception ex)
        {
            return OperationResult<RequestGetCarrinhoItemDto>.Fail("ERRO INTERNO! CONTATE O SUPORTE DE SISTEMA!" + ex.Message);
        }
    }

    public async Task<OperationResult<RequestGetCarrinhoItemDto>> DeleteItem(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"CarrinhoCompra/{id}");
            if (response.IsSuccessStatusCode)
            {
                return OperationResult<RequestGetCarrinhoItemDto>.Ok(new RequestGetCarrinhoItemDto());
            }
            return OperationResult<RequestGetCarrinhoItemDto>.Ok(new RequestGetCarrinhoItemDto());
        }
        catch (Exception ex)
        {
            return OperationResult<RequestGetCarrinhoItemDto>.Fail("ERRO INTERNO! CONTATE O SUPORTE DE SISTEMA!" + ex.Message);
        }

    }

    public async Task<OperationResult<List<RequestGetCarrinhoItemDto>>> GetItens(int usuarioId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"CarrinhoCompra/{usuarioId}/GetItens");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return OperationResult<List<RequestGetCarrinhoItemDto>>.Ok(new List<RequestGetCarrinhoItemDto>());
                }
                var carrinhos = await response.Content.ReadFromJsonAsync<List<RequestGetCarrinhoItemDto>>();
                if (carrinhos != null)
                {
                    return OperationResult<List<RequestGetCarrinhoItemDto>>.Ok(carrinhos);
                }
                return OperationResult<List<RequestGetCarrinhoItemDto>>.Fail("NÃO ENCONTRADO NENHUM VALOR");

            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http Status Code: {response.StatusCode}, Message: {message}");
            }

        }
        catch (Exception ex)
        {
            return OperationResult<List<RequestGetCarrinhoItemDto>>.Fail("ERRO INTERNO CONTATAR O SUPORTE" + ex.Message); ;
        }
    }

    public async Task<OperationResult<RequestGetCarrinhoItemDto>> AtualizaQuantidade(RequestCarrinhoItemAtualizaQuantidadeDto
                                                carrinhoItemAtualizaQuantidadeDto)
    {
        try
        {
            var jsonRequest = JsonSerializer.Serialize(carrinhoItemAtualizaQuantidadeDto);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");
            var response = await _httpClient.PatchAsync($"CarrinhoCompra/{carrinhoItemAtualizaQuantidadeDto.CarrinhoItemId}", content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<OperationResult<RequestGetCarrinhoItemDto>>()
                             ?? OperationResult<RequestGetCarrinhoItemDto>.Fail("não encotrado!");
            }
            return OperationResult<RequestGetCarrinhoItemDto>.Fail("Ocorreu Um Erro" + response.RequestMessage);
        }
        catch (Exception)
        {
            throw;
        }

    }
}






