using BlazorShop.Api.Context;
using BlazorShop.Api.Entities;
using BlazorShop.Api.Entities.Enums;
using BlazorShop.Api.Repositories.Interfaces;
using BlazorShop.Api.Services.Interfaces;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.UsuarioDtos;
using Microsoft.EntityFrameworkCore;

namespace BlazorShop.Api.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;
    private readonly IPasswordService _passwordHasher;
    public UsuarioRepository(AppDbContext context, IPasswordService passwordRepository)
    {
        _context = context;
        _passwordHasher = passwordRepository;
    }

    public async Task<OperationResult<Usuario>> ChangePassword(int id, RequestUpdateSenhaUsuarioDto updateSenhaUsuarioDto)
    {
        if (updateSenhaUsuarioDto.ConfirmedPassword != updateSenhaUsuarioDto.NewPassword)
        {
            return OperationResult<Usuario>.Fail("As senhas não coincidem");
        }

        var response = await _context.Usuarios.FindAsync(id);

        if (response is null)
        {
            return OperationResult<Usuario>.Fail("Usuario Não encontrado");
        }
        if (_passwordHasher.IsInvalidPassword(response,
            hashedPassword: response.Senha,
            password:updateSenhaUsuarioDto.OldPassword))
        {
            var hashedPassword = _passwordHasher.HashedPassword(response, updateSenhaUsuarioDto.NewPassword);
            response.Senha = hashedPassword;
            await _context.SaveChangesAsync();
            return OperationResult<Usuario>.Ok(response);
        }
        return OperationResult<Usuario>.Fail("A senha antiga esta errada");
    }

    public async Task<Usuario?> GetUsuario(string email)
    { 
        return await _context.Usuarios
                              .SingleOrDefaultAsync(u => u.Email == email);
    }

    public async Task<OperationResult<Usuario>> InsertUsuario(RequestCadastroUsuarioDto cadastroUsuario)
    {
        var tipoPessoa = (TipoPessoa)cadastroUsuario.TipoPessoa;
        if (cadastroUsuario.Senha != cadastroUsuario.ConfirmaSenha)
        {
            return OperationResult<Usuario>.Fail("Senhas não conhecidem");
        }
        var usuario = new Usuario
        {
            CNPJ = cadastroUsuario.CNPJ,
            CPF = cadastroUsuario.CPF,
            Email = cadastroUsuario.Email,
            Endereco = cadastroUsuario.Endereco,
            InscricaoEstadual = cadastroUsuario.InscricaoEstadual,
            Nome = cadastroUsuario.Nome,
            NomeFantasia = cadastroUsuario.NomeFantasia,
            RazaoSocial = cadastroUsuario.RazaoSocial,
            ResponsavelCompra = cadastroUsuario.ResponsavelCompra,
            Senha = cadastroUsuario.Senha,
            Telefone = cadastroUsuario.Telefone,
            TipoPessoa = tipoPessoa,

        };
        usuario.Carrinho = new Carrinho();
        var hashPassword = _passwordHasher.HashedPassword(usuario, cadastroUsuario.Senha);
        usuario.Senha = hashPassword;
        var resultado = await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();
        return OperationResult<Usuario>.Ok(resultado.Entity);
    }

    public async Task<OperationResult<Usuario>> UpdateUsuario(int id, RequestUpdateCadastroUsuarioDto updateCadastroUsuario)
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
        usuario.Email = updateCadastroUsuario.Email ?? usuario.Email;
        usuario.Nome = updateCadastroUsuario.Nome ?? usuario.Nome;

        await _context.SaveChangesAsync();
        return (OperationResult<Usuario>.Ok(usuario));
    }

}