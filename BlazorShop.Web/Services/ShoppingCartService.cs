using BlazorShop.Models.Commons;
using BlazorShop.Models.Config;
using BlazorShop.Models.DTOs.CartDtos;
using BlazorShop.Web.Services.Interfaces;
using System.Net;
using System.Text;
using System.Text.Json;
namespace BlazorShop.Web.Services;

public class ShoppingCartService : IShoppingCartService
{
    private readonly HttpClient _httpClient;

    public ShoppingCartService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient(HttpConfiguration.Compras);
    }

    public async Task<OperationResult<CartItemDto>> AddItem(CartItemAddRequestDto cartItemAddDto)
    {
        try
        {
            var response = await _httpClient
                .PostAsJsonAsync<CartItemAddRequestDto>("CarrinhoCompra", cartItemAddDto);
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return OperationResult<CartItemDto>.Ok(new CartItemDto());
                }
                var postProduct = await response.Content.ReadFromJsonAsync<CartItemDto>();
                if (postProduct != null)
                {
                    return OperationResult<CartItemDto>.Ok(new CartItemDto());
                }
                return OperationResult<CartItemDto>.Fail("NENHUM ITEM ENCONTRADO");
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http Status Code: {response.StatusCode}, Message: {message}");
            }
        }
        catch (Exception ex)
        {
            return OperationResult<CartItemDto>.Fail("ERRO INTERNO! CONTATE O SUPORTE DE SISTEMA!" + ex.Message);
        }
    }

    public async Task<OperationResult<CartItemDto>> DeleteItem(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"CarrinhoCompra/{id}");
            if (response.IsSuccessStatusCode)
            {
                return OperationResult<CartItemDto>.Ok(new CartItemDto());
            }
            return OperationResult<CartItemDto>.Ok(new CartItemDto());
        }
        catch (Exception ex)
        {
            return OperationResult<CartItemDto>.Fail("ERRO INTERNO! CONTATE O SUPORTE DE SISTEMA!" + ex.Message);
        }

    }

    public async Task<OperationResult<List<CartItemDto>>> GetItems(int userId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"CarrinhoCompra/{userId}/GetItens");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return OperationResult<List<CartItemDto>>.Ok(new List<CartItemDto>());
                }
                var carts = await response.Content.ReadFromJsonAsync<List<CartItemDto>>();
                if (carts != null)
                {
                    return OperationResult<List<CartItemDto>>.Ok(carts);
                }
                return OperationResult<List<CartItemDto>>.Fail("NÃO ENCONTRADO NENHUM VALOR");

            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http Status Code: {response.StatusCode}, Message: {message}");
            }

        }
        catch (Exception ex)
        {
            return OperationResult<List<CartItemDto>>.Fail("ERRO INTERNO CONTATAR O SUPORTE" + ex.Message); ;
        }
    }

    public async Task<OperationResult<CartItemDto>> UpdateQuantity(CartItemQuantityUpdateRequestDto
                                                cartItemUpdateQuantityDto)
    {
        try
        {
            var jsonRequest = JsonSerializer.Serialize(cartItemUpdateQuantityDto);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");
            var response = await _httpClient.PatchAsync($"CarrinhoCompra/{cartItemUpdateQuantityDto.CartItemId}", content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<OperationResult<CartItemDto>>()
                             ?? OperationResult<CartItemDto>.Fail("não encotrado!");
            }
            return OperationResult<CartItemDto>.Fail("Ocorreu Um Erro" + response.RequestMessage);
        }
        catch (Exception)
        {
            throw;
        }

    }
}






