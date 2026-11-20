using BlazorShop.Api.Context;
using BlazorShop.Api.Entities;
using BlazorShop.Api.Repositories.Interfaces;
using BlazorShop.Models.Commons;
using Microsoft.EntityFrameworkCore;

namespace BlazorShop.Api.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;
    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationResult<Product>> GetItem(int id)
    {
        var product = await _context.Products
            .Include(c => c.Category)
            .SingleOrDefaultAsync(c => c.Id == id);

        if (product is null)
        {
            return OperationResult<Product>.Fail("PRODUTO NÃO ENCONTRADO");
        }
        return OperationResult<Product>.Ok(product);
    }

    public async Task<OperationResult<IEnumerable<Product>>> GetItems()
    {
        var products = await _context.Products
             .Include(c => c.Category)
             .ToListAsync();

        if (!products.Any())
        {
            return OperationResult<IEnumerable<Product>>.Fail("PRODUTOS NÃO ENCONTRADO");
        }
        return OperationResult<IEnumerable<Product>>.Ok(products);
    }

    public async Task<OperationResult<IEnumerable<Product>>> GetItemsPorCategory(int id)
    {
        var products = await _context.Products
             .Include(c => c.Category)
             .Where(c => c.CategoryId == id)
             .ToListAsync();

        if (!products.Any())
        {
            return OperationResult<IEnumerable<Product>>.Fail("PRODUTOS NÃO ENCONTRADO");
        }
        return OperationResult<IEnumerable<Product>>.Ok(products);
    }
}
