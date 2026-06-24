using Azure;
using BlazorShop.Api.Context;
using BlazorShop.Api.Entities;
using BlazorShop.Api.Repositories.Interfaces;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.UsuarioDtos;

namespace BlazorShop.Api.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationResult<Usuario>> GetUsuario(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario is null)
        {
            return OperationResult<Usuario>.Fail("Usuario não encontrado");
        }

        return OperationResult<Usuario>.Ok(usuario);
    }

    public Task<OperationResult<Usuario>> InsertUsuario(CadastroUsuarioDto cadastroUsuario)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<Usuario>> UpdateUsuario(int id, UpdateCadastroUsuarioDto updateCadastroUsuario)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario is null)
        {
            return OperationResult<Usuario>.Fail("Falha ao encontrar o Usuario");
        }

        usuario.NomeFantasia = updateCadastroUsuario.NomeFantasia ?? usuario.NomeFantasia;
        usuario.RazaoSocial = updateCadastroUsuario.RazaoSocial ?? usuario.RazaoSocial;
        usuario.ResponsavelCompra = updateCadastroUsuario.ResponsavelCompra ?? usuario.ResponsavelCompra;
        usuario.InscricaoEstadual = updateCadastroUsuario.InscricaoEstadual ?? usuario.InscricaoEstadual;
        usuario.Endereco = updateCadastroUsuario.Endereco ?? usuario.Endereco;
        usuario.Telefone = updateCadastroUsuario.Telefone ?? usuario.Telefone;
        usuario.Senha = updateCadastroUsuario.Senha ?? usuario.Senha;
        usuario.Email = updateCadastroUsuario.Email ?? usuario.Email;
        usuario.Nome = updateCadastroUsuario.Nome ?? usuario.Nome;

        await _context.SaveChangesAsync();
        return (OperationResult<Usuario>.Ok(usuario));
    }
}