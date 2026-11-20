using BlazorShop.Models.Commons;
using BlazorShop.Models.Config;
using BlazorShop.Models.DTOs.OrderDtos;
using BlazorShop.Web.Services.Interfaces;
using System.Net;
using System.Text;
using System.Text.Json;
namespace BlazorShop.Web.Services;

public class ShoppingOrderService : IShoppingOrderService
{
    private readonly HttpClient _httpClient;

    public ShoppingOrderService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient(HttpConfiguration.Compras);
    }

    public async Task<OperationResult<OrderItemDto>> AddItem(OrderItemAddRequestDto orderItemAddDto)
    {
        try
        {
            var response = await _httpClient
                .PostAsJsonAsync<OrderItemAddRequestDto>("ShoppingOrder", orderItemAddDto);
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return OperationResult<OrderItemDto>.Ok(new OrderItemDto());
                }
                var postProduct = await response.Content.ReadFromJsonAsync<OrderItemDto>();
                if (postProduct != null)
                {
                    return OperationResult<OrderItemDto>.Ok(new OrderItemDto());
                }
                return OperationResult<OrderItemDto>.Fail("NENHUM ITEM ENCONTRADO");
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http Status Code: {response.StatusCode}, Message: {message}");
            }
        }
        catch (Exception ex)
        {
            return OperationResult<OrderItemDto>.Fail("ERRO INTERNO! CONTATE O SUPORTE DE SISTEMA!" + ex.Message);
        }
    }

    public async Task<OperationResult<OrderItemDto>> DeleteItem(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"ShoppingOrder/{id}");
            if (response.IsSuccessStatusCode)
            {
                return OperationResult<OrderItemDto>.Ok(new OrderItemDto());
            }
            return OperationResult<OrderItemDto>.Ok(new OrderItemDto());
        }
        catch (Exception ex)
        {
            return OperationResult<OrderItemDto>.Fail("ERRO INTERNO! CONTATE O SUPORTE DE SISTEMA!" + ex.Message);
        }

    }

    public async Task<OperationResult<List<OrderItemDto>>> GetItems(int userId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"ShoppingOrder/{userId}/GetItens");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return OperationResult<List<OrderItemDto>>.Ok(new List<OrderItemDto>());
                }
                var orders = await response.Content.ReadFromJsonAsync<List<OrderItemDto>>();
                if (orders != null)
                {
                    return OperationResult<List<OrderItemDto>>.Ok(orders);
                }
                return OperationResult<List<OrderItemDto>>.Fail("NÃO ENCONTRADO NENHUM VALOR");

            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http Status Code: {response.StatusCode}, Message: {message}");
            }

        }
        catch (Exception ex)
        {
            return OperationResult<List<OrderItemDto>>.Fail("ERRO INTERNO CONTATAR O SUPORTE" + ex.Message); ;
        }
    }

    public async Task<OperationResult<OrderItemDto>> UpdateQuantity(OrderItemQuantityUpdateRequestDto
                                                orderItemUpdateQuantityDto)
    {
        try
        {
            var jsonRequest = JsonSerializer.Serialize(orderItemUpdateQuantityDto);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");
            var response = await _httpClient.PatchAsync($"ShoppingOrder/{orderItemUpdateQuantityDto.OrderItemId}", content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<OperationResult<OrderItemDto>>()
                             ?? OperationResult<OrderItemDto>.Fail("não encotrado!");
            }
            return OperationResult<OrderItemDto>.Fail("Ocorreu Um Erro" + response.RequestMessage);
        }
        catch (Exception)
        {
            throw;
        }

    }
}






