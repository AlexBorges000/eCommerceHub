using BlazorShop.Api.Entities;
using BlazorShop.Api.Repositories.Interfaces;
using BlazorShop.Api.Services.Usuarios.Interfaces;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.Endereco;

namespace BlazorShop.Api.Services.Usuarios
{
    public class EnderecoService(IEnderecoRepository enderecoRepository) : IEnderecoService
    {
        private readonly IEnderecoRepository _enderecoRepository = enderecoRepository;

        public async Task<OperationResult<Endereco>> AddAsync(int userId, RequestAddEnderecoDto requestAddEnderecoDto)
        {
            var endereco = new Endereco
            {
                Logradouro = requestAddEnderecoDto.Logradouro,
                Bairro = requestAddEnderecoDto.Bairro,
                Numero = requestAddEnderecoDto.Numero,
                CEP = requestAddEnderecoDto.CEP,
                Cidade = requestAddEnderecoDto.Cidade,
                Complemento = requestAddEnderecoDto.Complemento,
                Principal = requestAddEnderecoDto.Principal,
                UF = requestAddEnderecoDto.UF,
                UsuarioId = userId,
            };

            if (await _enderecoRepository.ExisteEndereco(endereco))
            {
                return OperationResult<Endereco>.Fail("Endereço ja cadastrado");
            }
            await _enderecoRepository.AddAsync(endereco);
            return OperationResult<Endereco>.Ok(endereco);
        }

        public async Task<OperationResult<Endereco>> DeleteAsync(int id, int userId)
        {
            var endereco = await _enderecoRepository.GetByUserIdAndIdAsync(userId, id);
            if (endereco is null)
            {
                return OperationResult<Endereco>.Fail("Endereço não encontrado");
            }

            if (endereco.Principal)
            {
                return OperationResult<Endereco>.Fail("Defina outro endereço como principal para excluir esse");
            }

            var enderecos = await _enderecoRepository.CountByUserIdAsync(userId);
            if (enderecos == 1)
            {
                return OperationResult<Endereco>.Fail("Adicione mais um endereço para excluir este!");
            }

            await _enderecoRepository.DeleteAsync(endereco);
            return OperationResult<Endereco>.Ok(endereco);
        }

        public async Task<OperationResult<IEnumerable<Endereco>>> GetAllByUserIdAsync(int userId)
        {
            var enderecos = await _enderecoRepository.GetAllByUserIdAsync(userId);
            if (enderecos is null)
            {
                return OperationResult<IEnumerable<Endereco>>.Fail("Nenhum Endereço encontrado");
            }
            
            return OperationResult<IEnumerable<Endereco>>.Ok(enderecos);
        }

        public async Task<OperationResult<Endereco>> GetEnderecoByUserIdAndIdAsync(int userId, int id)
        {
            var endereco = await _enderecoRepository.GetByUserIdAndIdAsync(userId, id);
            if (endereco is null)
            {
                return OperationResult<Endereco>.Fail("Endereço não encontrado");
            }

            return OperationResult<Endereco>.Ok(endereco);
        }

        public async Task<OperationResult<Endereco>> UpdateAsync(int id, int userId, RequestUpdateEnderecoDto requestUpdateEnderecoDto)
        {
            var endereco = await _enderecoRepository.GetByUserIdAndIdAsync(userId, id);
            if (endereco is null)
            {
                return OperationResult<Endereco>.Fail("Endereço não encontrado");
            }
            endereco.Logradouro = requestUpdateEnderecoDto.Logradouro ?? endereco.Logradouro;
            endereco.Numero = requestUpdateEnderecoDto.Numero ?? endereco.Numero;
            endereco.Complemento = requestUpdateEnderecoDto.Complemento ?? endereco.Complemento;
            endereco.Cidade = requestUpdateEnderecoDto.Cidade ?? endereco.Cidade;
            endereco.Bairro = requestUpdateEnderecoDto.Bairro ?? endereco.Bairro;
            endereco.CEP = requestUpdateEnderecoDto.CEP ?? endereco.CEP;
            endereco.Principal = requestUpdateEnderecoDto.Principal ?? endereco.Principal;

            await _enderecoRepository.UpdateAsync(endereco);
            return OperationResult<Endereco>.Ok(endereco);
        }
    }
}
