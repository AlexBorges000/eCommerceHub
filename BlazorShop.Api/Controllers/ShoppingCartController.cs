using BlazorShop.Api.Mappings.CartItems;
using BlazorShop.Api.Repositories.Interfaces;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.CartDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorShop.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Tags("CarrinhoCompras")]
public class ShoppingCartController : ControllerBase
{
    private readonly IShoppingCartRepository _cartCompraRepository;
    private readonly IProductRepository _productRepository;
    private ILogger<ShoppingCartController> _logger;

    public ShoppingCartController(IShoppingCartRepository cartCompraRepository
        , IProductRepository productRepository,
        ILogger<ShoppingCartController> logger)
    {
        _cartCompraRepository = cartCompraRepository;
        _productRepository = productRepository;
        _logger = logger;
    }

    [Authorize]
    [HttpGet]
    [Route("{usuarioId}/GetItens")]
    public async Task<ActionResult<IEnumerable<CartItemDto>>> GetItems(int userId)
    {
        var cartItems = await _cartCompraRepository.GetItems(userId);
        if (!cartItems.Success)
        {
            return BadRequest("NENHUM ITEM ENCONTRADO");
        }
        var products = await _productRepository.GetItems();
        if (!products.Success)
        {
            return BadRequest(OperationResult<IEnumerable<CartItemDto>>.Fail("NENHUM ITEM ENCONTRADO"));
        }
        if(products.Value is null)
        {
            return NotFound("Produtos não encontrados");
        }
        var cartItemsDto = cartItems.Value.ToDto();
        return Ok(cartItemsDto);
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CartItemDto>> GetItem(int id)
    {
        var cartItem = await _cartCompraRepository.GetItem(id);
        if (!cartItem.Success)
        {
            return NotFound($"Erro ao encontrar o carrinho");
        }
        if (cartItem.Value is null)
        {
            return NotFound("Carrinho não encontrados");
        }
        var product = await _productRepository.GetItem(cartItem.Value.ProductId);
        if (product.Value is null)
        {
            return NotFound(OperationResult<CartItemDto>.Fail("NENHUM PRODUTO ENCONTRADO"));
        }
        var cartItemDto = cartItem.Value.ToDto();
        return Ok(cartItemDto);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<CartItemDto>> PostItem([FromBody] CartItemAddRequestDto cartItemAddDto)
    {
        var newCartItem = await _cartCompraRepository.AddItem(cartItemAddDto);
        if (!newCartItem.Success)
        {
            return NoContent();
        }
        if (newCartItem.Value is null)
        {
            return NotFound(OperationResult<CartItemDto>.Fail("NENHUM PRODUTO PARA ADICIONAR ENCONTRADO"));
        }
        var product = await _productRepository.GetItem(newCartItem.Value.ProductId);
        if (product.Value is null)
        {
            return NotFound($"Produto com ID {newCartItem.Value.ProductId} não encontrado.");
        }
        var cartItemDto = newCartItem.Value.ToDto();
        if (cartItemDto is null)
        {
            return NotFound("Nada encontrado no carrinho");
        }
        return CreatedAtAction(nameof(GetItem), new { id = cartItemDto.Id }, cartItemDto);
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<CartItemDto>> DeleteItem(int id)
    {
        var cartItem = await _cartCompraRepository.GetItem(id);
        if (!cartItem.Success)
        {
            return BadRequest("PRODUTO PARA DELETAR NÃO ENCONTRADO");
        }
        if (cartItem.Value is null)
        {
            return NotFound($"NENHUM PRODUTO ENCONTRADO NO CARRINHO PARA DELETAR");
        }
        var product = await _productRepository.GetItem(cartItem.Value.ProductId);

        if (product.Value is null)
            return NotFound("PRODUTO PARA DELETAR NÃO ENCONTRADO");

        await _cartCompraRepository.DeleteItem(cartItem.Value.Id);
        var cartItemDto = cartItem.Value.ToDto();
        return Ok(cartItemDto);
    }

    [Authorize]
    [HttpPatch("{id:int}")]
    public async Task<ActionResult<CartItemDto>> UpdateQuantity(int id, CartItemQuantityUpdateRequestDto cartItemUpdateQuantityDto)
    {
        var cartItem = await _cartCompraRepository.UpdateQuantity(id,
                               cartItemUpdateQuantityDto);
        if (!cartItem.Success)
        {
            return NotFound("PRODUTO NÃO ENCONTRADO PARA ATULIZAR A QUANTIDADE NO CARRINHO");
        }
        if (cartItem.Value is null)
            return NotFound("PRODUTO PARA DELETAR NÃO ENCONTRADO");
        var product = await _productRepository.GetItem(cartItem.Value.ProductId);
        if (product.Value is null)
            return NotFound("PRODUTO PARA DELETAR NÃO ENCONTRADO");
        var cartItemDto = cartItem.Value.ToDto();
        return Ok(cartItemDto);
    }

}
