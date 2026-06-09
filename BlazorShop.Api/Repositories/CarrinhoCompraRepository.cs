using BlazorShop.Api.Context;
using BlazorShop.Api.Entities;
using BlazorShop.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.Security.AccessControl;

namespace BlazorShop.Api.Repositories;

public class CarrinhoCompraRepository : ICarrinhoCompraRepository
{
    private readonly AppDbContext _context;

    public CarrinhoCompraRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<CarrinhoItem> AdicionaItem(CarrinhoItemAdicionaDto carrinhoItemAdicionaDto)
    {
        if (await CarrinhoItemJaExiste(carrinhoItemAdicionaDto.CarrinhoId, carrinhoItemAdicionaDto.ProdutoId) == false)
        {
           var item =  await (from produto in _context.Produtos
                          where produto.Id == carrinhoItemAdicionaDto.ProdutoId
                          select new CarrinhoItem
                          {
                              CarrinhoId = carrinhoItemAdicionaDto.CarrinhoId,
                              ProdutoId = carrinhoItemAdicionaDto.ProdutoId,
                              Quantidade = carrinhoItemAdicionaDto.Quantidade
                          }).SingleOrDefaultAsync();
            if(item is not null)
            {
                var resultado = await _context.CarrinhoItem.AddAsync(item);
                await _context.SaveChangesAsync();
                return resultado.Entity;
            }
        }
        return null;
        
    }

    private async Task<bool> CarrinhoItemJaExiste(int carrinhoId, int produtoId)
    {
        return await _context.CarrinhoItem.AnyAsync(c=> c.CarrinhoId == carrinhoId && c.ProdutoId == produtoId);
    }
    public async Task<CarrinhoItem> AtualizaQuantidade(int id, CarrinhoItemAtualizaQuantidadeDto carrinhoItemAtulizaQuantidadeDto)
    {
        /*var item = await(from produto in _context.CarrinhoItem
                     where produto.ProdutoId == id
                     select new CarrinhoItem
                     {
                         ProdutoId = carrinhoItemAtulizaQuantidadeDto.CarrinhoItemId,
                         Quantidade = carrinhoItemAtulizaQuantidadeDto.Quantidade
                     }).SingleOrDefaultAsync();
        var resultado = _context.Update(item);
        await _context.SaveChangesAsync();
        return resultado.Entity;*/
        throw new NotImplementedException();
    }

    public async Task<CarrinhoItem> DeleteItem(int id)
    {
        var item = await _context.CarrinhoItem.FindAsync(id);
        if (item is not null)
        {
            _context.CarrinhoItem.Remove(item);
            await _context.SaveChangesAsync();
        }
        return item;
    }

    public async Task<CarrinhoItem?> GetItem(int id)
    {

        return await (from carrinho in _context.Carrinho
                      join carrinhoItem in _context.CarrinhoItem
                      on carrinho.Id equals carrinhoItem.CarrinhoId
                      where carrinhoItem.Id == id
                      select new CarrinhoItem
                      {
                          Id = carrinhoItem.Id,
                          CarrinhoId = carrinhoItem.CarrinhoId,
                          ProdutoId = carrinhoItem.ProdutoId,
                          Quantidade = carrinhoItem.Quantidade
                      }).SingleOrDefaultAsync();

    }




    public async Task<IEnumerable<CarrinhoItem>> GetItens(int usuarioId)
    {
        return await (from carrinho in _context.Carrinho
                      join carrinhoItem in _context.CarrinhoItem
                      on carrinho.Id equals carrinhoItem.CarrinhoId
                      where carrinho.UsuarioId == usuarioId
                      select new CarrinhoItem
                      {
                          Id = carrinhoItem.Id,
                          CarrinhoId = carrinhoItem.CarrinhoId,
                          ProdutoId = carrinhoItem.ProdutoId,
                          Quantidade = carrinhoItem.Quantidade
                      }).ToListAsync();
    }
}
