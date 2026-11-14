using BlazorShop.Api.Context;
using BlazorShop.Api.Entities;
using BlazorShop.Api.Repositories.Interfaces;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.CartDtos;
using Microsoft.EntityFrameworkCore;

namespace BlazorShop.Api.Repositories;

public class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly AppDbContext _context;

    public ShoppingCartRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationResult<CartItem>> AddItem(CartItemAddRequestDto cartItemAddDto)
    {
        if (await CartItemAlreadyExists(cartItemAddDto.CartId, cartItemAddDto.ProductId) == false)
        {
            var item = await (from product in _context.Products
                              where product.Id == cartItemAddDto.ProductId
                              select new CartItem
                              {
                                  CartId = cartItemAddDto.CartId,
                                  ProductId = cartItemAddDto.ProductId,
                                  Quantity = cartItemAddDto.Quantity
                              }).SingleOrDefaultAsync();
            if (item is not null)
            {
                var resultado = await _context.CartItems.AddAsync(item);
                await _context.SaveChangesAsync();
                return OperationResult<CartItem>.Ok(resultado.Entity);
            }
        }
        else
        {
            var cart = await _context.CartItems.Where(c => c.CartId == cartItemAddDto.CartId &&
                c.ProductId == cartItemAddDto.ProductId).FirstOrDefaultAsync();
            if (cart is not null)
            {
                var cartItemUpdateQuantityDto = new CartItemQuantityUpdateRequestDto()
                {
                    CartItemId = cart.Id,
                    Quantity = (cart.Quantity + cartItemAddDto.Quantity)
                };
                var atualizaCart = await UpdateQuantity(cartItemUpdateQuantityDto.CartItemId, cartItemUpdateQuantityDto);
                return (OperationResult<CartItem>.Ok(atualizaCart.Value));
            }
            return (OperationResult<CartItem>.Fail("Item não encontrado!"));
        }
        return (OperationResult<CartItem>.Fail("Item não encontrado!"));

    }

    private async Task<bool> CartItemAlreadyExists(int cartId, int productId)
    {
        return await _context.CartItems.AnyAsync(c => c.CartId == cartId && c.ProductId == productId);
    }

    public async Task<OperationResult<CartItem>> UpdateQuantity(int id, CartItemQuantityUpdateRequestDto cartItemAtulizaQuantityDto)
    {
        var cartItem = await _context.CartItems.FindAsync(id);

        if (cartItem is not null)
        {

            cartItem.Quantity = cartItemAtulizaQuantityDto.Quantity;
            await _context.SaveChangesAsync();
            return OperationResult<CartItem>.Ok(cartItem);
        }
        return OperationResult<CartItem>.Fail("Item não ENCONTRADO PARA ADICIONAR AO CARRINHO");
    }

    public async Task<OperationResult<CartItem>> DeleteItem(int id)
    {
        var item = await _context.CartItems.FindAsync(id);
        if (item is not null)
        {
            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
            return OperationResult<CartItem>.Ok(item);
        }
        return OperationResult<CartItem>.Fail("Item não encontrado para deletar");
    }

    public async Task<OperationResult<CartItem>> GetItem(int id)
    {

        var item = await (from cart in _context.Carts
                          join cartItem in _context.CartItems
                          on cart.Id equals cartItem.CartId
                          where cartItem.Id == id
                          select new CartItem
                          {
                              Id = cartItem.Id,
                              CartId = cartItem.CartId,
                              ProductId = cartItem.ProductId,
                              Quantity = cartItem.Quantity
                          }).SingleOrDefaultAsync();
        if (item is not null)
        {
            return OperationResult<CartItem>.Ok(item);
        }

        return OperationResult<CartItem>.Fail("Item não encontado!");

    }

    public async Task<OperationResult<IEnumerable<CartItem>>> GetItems(int userId)
    {
        var item = await _context.CartItems
                                  .Include(ci => ci.Product)
                                  .Include(ci => ci.Cart)
                                  .Where(ci => ci.Cart.UserId == userId)
                                  .ToListAsync();
        if (item.Any())
        {
            return OperationResult<IEnumerable<CartItem>>.Ok(item);
        }

        return OperationResult<IEnumerable<CartItem>>.Fail("ITENS NÃO ENCONTRADOS");

    }
}