using Azure;
using BlazorShop.Api.Context;
using BlazorShop.Api.Entities;
using BlazorShop.Api.Repositories.Interfaces;
using BlazorShop.Models.Commons;
using Microsoft.EntityFrameworkCore;

namespace BlazorShop.Api.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly AppDbContext _context;
    public ProdutoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationResult<Produto>> GetItem(int id)
    {
        var produto = await _context.Produtos
            .Include(c => c.Categoria)
            .SingleOrDefaultAsync(c => c.Id == id);

        return OperationResult<Produto>.Ok(produto);
    }

    public async Task<OperationResult<IEnumerable<Produto>>> GetItens()
    {
        var produtos = await _context.Produtos
             .Include(c => c.Categoria)
             .ToListAsync();

        return OperationResult<IEnumerable<Produto>>.Ok(produtos);
    }

    public async Task<OperationResult<IEnumerable<Produto>>> GetItensPorCategoria(int id)
    {
        var produtos = await _context.Produtos
             .Include(c => c.Categoria)
             .Where(c => c.CategoriaId == id)
             .ToListAsync();

        return OperationResult<IEnumerable<Produto>>.Ok(produtos);
    }
}
