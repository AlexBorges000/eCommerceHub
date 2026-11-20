using BlazorShop.Api.Context;
using BlazorShop.Api.Entities;
using BlazorShop.Api.Repositories.Interfaces;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.OrderDtos;
using Microsoft.EntityFrameworkCore;

namespace BlazorShop.Api.Repositories;

public class ShoppingOrderRepository : IShoppingOrderRepository
{
    private readonly AppDbContext _context;

    public ShoppingOrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationResult<OrderItem>> AddItem(OrderItemAddRequestDto orderItemAddDto)
    {
        if (await OrderItemAlreadyExists(orderItemAddDto.OrderId, orderItemAddDto.ProductId) == false)
        {
            var item = await (from product in _context.Products
                              where product.Id == orderItemAddDto.ProductId
                              select new OrderItem
                              {
                                  OrderId = orderItemAddDto.OrderId,
                                  ProductId = orderItemAddDto.ProductId,
                                  Quantity = orderItemAddDto.Quantity
                              }).SingleOrDefaultAsync();
            if (item is not null)
            {
                var resultado = await _context.OrderItems.AddAsync(item);
                await _context.SaveChangesAsync();
                return OperationResult<OrderItem>.Ok(resultado.Entity);
            }
        }
        else
        {
            var order = await _context.OrderItems.Where(c => c.OrderId == orderItemAddDto.OrderId &&
                c.ProductId == orderItemAddDto.ProductId).FirstOrDefaultAsync();
            if (order is not null)
            {
                var orderItemUpdateQuantityDto = new OrderItemQuantityUpdateRequestDto()
                {
                    OrderItemId = order.Id,
                    Quantity = (order.Quantity + orderItemAddDto.Quantity)
                };
                var atualizaOrder = await UpdateQuantity(orderItemUpdateQuantityDto.OrderItemId, orderItemUpdateQuantityDto);
                return atualizaOrder;
            }
            return (OperationResult<OrderItem>.Fail("Item não encontrado!"));
        }
        return (OperationResult<OrderItem>.Fail("Item não encontrado!"));

    }

    private async Task<bool> OrderItemAlreadyExists(int orderId, int productId)
    {
        return await _context.OrderItems.AnyAsync(c => c.OrderId == orderId && c.ProductId == productId);
    }

    public async Task<OperationResult<OrderItem>> UpdateQuantity(int id, OrderItemQuantityUpdateRequestDto orderItemAtulizaQuantityDto)
    {
        var orderItem = await _context.OrderItems.FindAsync(id);

        if (orderItem is not null)
        {

            orderItem.Quantity = orderItemAtulizaQuantityDto.Quantity;
            await _context.SaveChangesAsync();
            return OperationResult<OrderItem>.Ok(orderItem);
        }
        return OperationResult<OrderItem>.Fail("Item não ENCONTRADO PARA ADICIONAR AO CARRINHO");
    }

    public async Task<OperationResult<OrderItem>> DeleteItem(int id)
    {
        var item = await _context.OrderItems.FindAsync(id);
        if (item is not null)
        {
            _context.OrderItems.Remove(item);
            await _context.SaveChangesAsync();
            return OperationResult<OrderItem>.Ok(item);
        }
        return OperationResult<OrderItem>.Fail("Item não encontrado para deletar");
    }

    public async Task<OperationResult<OrderItem>> GetItem(int id)
    {

        var item = await (from order in _context.Orders
                          join orderItem in _context.OrderItems
                          on order.Id equals orderItem.OrderId
                          where orderItem.Id == id
                          select new OrderItem
                          {
                              Id = orderItem.Id,
                              OrderId = orderItem.OrderId,
                              ProductId = orderItem.ProductId,
                              Quantity = orderItem.Quantity
                          }).SingleOrDefaultAsync();
        if (item is not null)
        {
            return OperationResult<OrderItem>.Ok(item);
        }

        return OperationResult<OrderItem>.Fail("Item não encontado!");

    }

    public async Task<OperationResult<IEnumerable<OrderItem>>> GetItems(int userId)
    {
        var item = await _context.OrderItems
                                  .Include(ci => ci.Product)
                                  .Include(ci => ci.Order)
                                  .Where(ci => ci.Order.UserId == userId)
                                  .ToListAsync();
        if (item.Any())
        {
            return OperationResult<IEnumerable<OrderItem>>.Ok(item);
        }

        return OperationResult<IEnumerable<OrderItem>>.Fail("ITENS NÃO ENCONTRADOS");

    }
}
