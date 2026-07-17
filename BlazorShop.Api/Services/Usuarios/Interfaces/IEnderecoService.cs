using BlazorShop.Api.Entities;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.Endereco;

namespace BlazorShop.Api.Services.Usuarios.Interfaces;

public interface IEnderecoService
{
    Task<OperationResult<IEnumerable<Endereco>>> GetAllByUserIdAsync(int userId);
    Task<OperationResult<Endereco>> AddAsync(int userId, RequestAddEnderecoDto requestAddEnderecoDto);
    Task<OperationResult<Endereco>> UpdateAsync(int id, int userId, RequestUpdateEnderecoDto requestUpdateEnderecoDto);
    Task<OperationResult<Endereco>> DeleteAsync(int id, int userId);
    Task<OperationResult<Endereco>> GetEnderecoByUserIdAndIdAsync(int userId, int id);

}
