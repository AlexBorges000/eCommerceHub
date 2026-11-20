using BlazorShop.Api.Mappings.OrderItems;
using BlazorShop.Api.Repositories.Interfaces;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.OrderDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorShop.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Tags("CarrinhoCompras")]
public class ShoppingOrderController : ControllerBase
{
    private readonly IShoppingOrderRepository _orderCompraRepository;
    private readonly IProductRepository _productRepository;
    private ILogger<ShoppingOrderController> _logger;

    public ShoppingOrderController(IShoppingOrderRepository orderCompraRepository
        , IProductRepository productRepository,
        ILogger<ShoppingOrderController> logger)
    {
        _orderCompraRepository = orderCompraRepository;
        _productRepository = productRepository;
        _logger = logger;
    }

    [Authorize]
    [HttpGet]
    [Route("{usuarioId}/GetItens")]
    public async Task<ActionResult<IEnumerable<OrderItemDto>>> GetItems(int userId)
    {
        var orderItems = await _orderCompraRepository.GetItems(userId);
        if (!orderItems.Success || orderItems.Value is null)
        {
            return BadRequest("NENHUM ITEM ENCONTRADO");
        }
        var products = await _productRepository.GetItems();
        if (!products.Success)
        {
            return BadRequest(OperationResult<IEnumerable<OrderItemDto>>.Fail("NENHUM ITEM ENCONTRADO"));
        }
        if (products.Value is null)
        {
            return NotFound("Produtos não encontrados");
        }
        var orderItemsDto = orderItems.Value.ToDto();
        return Ok(orderItemsDto);
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<OrderItemDto>> GetItem(int id)
    {
        var orderItem = await _orderCompraRepository.GetItem(id);
        if (!orderItem.Success)
        {
            return NotFound($"Erro ao encontrar o carrinho");
        }
        if (orderItem.Value is null)
        {
            return NotFound("Carrinho não encontrados");
        }
        var product = await _productRepository.GetItem(orderItem.Value.ProductId);
        if (product.Value is null)
        {
            return NotFound(OperationResult<OrderItemDto>.Fail("NENHUM PRODUTO ENCONTRADO"));
        }
        var orderItemDto = orderItem.Value.ToDto();
        return Ok(orderItemDto);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<OrderItemDto>> PostItem([FromBody] OrderItemAddRequestDto orderItemAddDto)
    {

        var newOrderItem = await _orderCompraRepository.AddItem(orderItemAddDto);
        if (!newOrderItem.Success)
        {
            return NoContent();
        }
        if (newOrderItem.Value is null)
        {
            return NotFound(OperationResult<OrderItemDto>.Fail("NENHUM PRODUTO PARA ADICIONAR ENCONTRADO"));
        }
        var product = await _productRepository.GetItem(newOrderItem.Value.ProductId);
        if (product.Value is null)
        {
            return NotFound($"Produto com ID {newOrderItem.Value.ProductId} não encontrado.");
        }
        var orderItemDto = newOrderItem.Value.ToDto();
        if (orderItemDto is null)
        {
            return NotFound("Nada encontrado no carrinho");
        }
        return CreatedAtAction(nameof(GetItem), new { id = orderItemDto.Id }, orderItemDto);
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<OrderItemDto>> DeleteItem(int id)
    {
        var orderItem = await _orderCompraRepository.GetItem(id);
        if (!orderItem.Success)
        {
            return BadRequest("PRODUTO PARA DELETAR NÃO ENCONTRADO");
        }
        if (orderItem.Value is null)
        {
            return NotFound($"NENHUM PRODUTO ENCONTRADO NO CARRINHO PARA DELETAR");
        }
        var product = await _productRepository.GetItem(orderItem.Value.ProductId);

        if (product.Value is null)
            return NotFound("PRODUTO PARA DELETAR NÃO ENCONTRADO");

        await _orderCompraRepository.DeleteItem(orderItem.Value.Id);
        var orderItemDto = orderItem.Value.ToDto();
        return Ok(orderItemDto);
    }

    [Authorize]
    [HttpPatch("{id:int}")]
    public async Task<ActionResult<OrderItemDto>> UpdateQuantity(int id, OrderItemQuantityUpdateRequestDto orderItemUpdateQuantityDto)
    {

        var orderItem = await _orderCompraRepository.UpdateQuantity(id,
                               orderItemUpdateQuantityDto);
        if (!orderItem.Success)
        {
            return NotFound("PRODUTO NÃO ENCONTRADO");
        }
        if (orderItem.Value is null)
            return NotFound("PRODUTO PARA DELETAR NÃO ENCONTRADO");
        var product = await _productRepository.GetItem(orderItem.Value.ProductId);
        if (product.Value is null)
            return NotFound("PRODUTO PARA DELETAR NÃO ENCONTRADO");
        var orderItemDto = orderItem.Value.ToDto();
        return Ok(orderItemDto);
    }

}
