using BlazorShop.Api.Entities;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.CarrinhoDtos;
using System.ClientModel.Primitives;

namespace BlazorShop.Api.Repositories.Interfaces;

public interface ICarrinhoCompraRepository
{
    Task<OperationResult<CarrinhoItem>> AdicionaItem(CarrinhoItemAdicionaDto carrinhoItemAdicionaDto);
    Task<OperationResult<CarrinhoItem>> AtualizaQuantidade(int id, CarrinhoItemAtualizaQuantidadeDto carrinhoItemAtulizaQuantidadeDto);
    Task<OperationResult<CarrinhoItem>> DeleteItem(int id);
    Task<OperationResult<CarrinhoItem>> GetItem(int id);
    Task<OperationResult<IEnumerable<CarrinhoItem>>> GetItens(int usuarioId);
}