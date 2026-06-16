using BlazorShop.Api.Context;
using BlazorShop.Api.Entities;
using BlazorShop.Api.Repositories.Interfaces;
using BlazorShop.Models.DTOs.CarrinhoDtos;
using Microsoft.EntityFrameworkCore;

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
            var item = await (from produto in _context.Produtos
                              where produto.Id == carrinhoItemAdicionaDto.ProdutoId
                              select new CarrinhoItem
                              {
                                  Id = carrinhoItemAdicionaDto.Id,
                                  CarrinhoId = carrinhoItemAdicionaDto.CarrinhoId,
                                  ProdutoId = carrinhoItemAdicionaDto.ProdutoId,
                                  Quantidade = carrinhoItemAdicionaDto.Quantidade
                              }).SingleOrDefaultAsync();
            if (item is not null)
            {
                var resultado = await _context.CarrinhoItem.AddAsync(item);
                await _context.SaveChangesAsync();
                return resultado.Entity;
            }
        }
        else
        {

            var carrinho = await _context.CarrinhoItem.Where(c => c.CarrinhoId == carrinhoItemAdicionaDto.CarrinhoId &&
                c.ProdutoId == carrinhoItemAdicionaDto.ProdutoId).FirstOrDefaultAsync();
            if (carrinho != null)
            {
                var carrinhoItemAtualizaQuantidadeDto = new CarrinhoItemAtualizaQuantidadeDto()
                {
                    CarrinhoItemId = carrinho.Id,
                    Quantidade = (carrinho.Quantidade + carrinhoItemAdicionaDto.Quantidade)
                };
                return await AtualizaQuantidade(carrinhoItemAtualizaQuantidadeDto.CarrinhoItemId, carrinhoItemAtualizaQuantidadeDto);
            }
            throw new KeyNotFoundException("NÃO FOI POSSIVEL ENCONTRAR O CARRINHO");
        }
        throw new KeyNotFoundException("NÃO FOI POSSIVEL ADICIONAR AO CARRINHO");

    }

    private async Task<bool> CarrinhoItemJaExiste(int carrinhoId, int produtoId)
    {
        return await _context.CarrinhoItem.AnyAsync(c => c.CarrinhoId == carrinhoId && c.ProdutoId == produtoId);
    }

    public async Task<CarrinhoItem> AtualizaQuantidade(int id, CarrinhoItemAtualizaQuantidadeDto carrinhoItemAtulizaQuantidadeDto)
    {
        var carrinhoItem = await _context.CarrinhoItem.FindAsync(id);

        if (carrinhoItem is not null)
        {

            carrinhoItem.Quantidade = carrinhoItemAtulizaQuantidadeDto.Quantidade;
            await _context.SaveChangesAsync();
            return carrinhoItem;
        }
        throw new KeyNotFoundException("Erro");
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