using BlazorShop.Api.Entities;
using BlazorShop.Api.Mappings.Usuarios;
using BlazorShop.Api.Repositories.Interfaces;
using BlazorShop.Api.Security.Authentication.Interfaces;
using BlazorShop.Api.Security.Password.Intefaces;
using BlazorShop.Api.Security.Security.Interfaces;
using BlazorShop.Api.Services.Auth.Interfaces;
using BlazorShop.Api.Services.Usuarios.Interfaces;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.UsuarioDtos.Cadastro;
using BlazorShop.Models.DTOs.UsuarioDtos.Update;

namespace BlazorShop.Api.Services.Usuarios;

public class UsuarioJuridicoService(IUsuarioRepository usuarioRepository,
                                    IHashService hashService,
                                    IAesService aesService,
                                    IRoleService roleService,
                                    IPasswordService passwordService,
                                    IAuthService authService) : IUsuarioJuridicoService
{
    private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
    private readonly IHashService _hashService = hashService;
    private readonly IAesService _aesService = aesService;
    private readonly IRoleService _roleService = roleService;
    private readonly IPasswordService _passwordService = passwordService;
    private readonly IAuthService _authService = authService;

    public async Task<OperationResult<ResponseLoginDto>> InsertUsuarioPjAsync(RequestCadastroUsuarioPjDto requestCadastroUsuarioPjDto)
    {
        if (requestCadastroUsuarioPjDto.Senha != requestCadastroUsuarioPjDto.ConfirmaSenha)
        {
            return OperationResult<ResponseLoginDto>.Fail("As senhas não coincidem");
        }

        var role = (await _roleService.GetClienteRoleAsync()).Value ??
            throw new InvalidOperationException("A role padrão 'Cliente' não foi encontrada."); ;

        if (await CnpjJaExiste(requestCadastroUsuarioPjDto.CNPJ))
        {
            return OperationResult<ResponseLoginDto>.Fail("CNPJ ja cadastrado!");
        }
        var usuarioJuridico = requestCadastroUsuarioPjDto.ToEntity();

        var documento = ProtectDocumento(requestCadastroUsuarioPjDto.CNPJ);

        usuarioJuridico.HashCNPJ = documento.Hash;
        usuarioJuridico.EncryptCNPJ = documento.Encrypt;

        PrepararUsuario(usuarioJuridico, role, requestCadastroUsuarioPjDto.Senha); 

        await _usuarioRepository.AddAsync(usuarioJuridico);

        var response = await _authService.CreateSessionAsync(usuarioJuridico);

        return OperationResult<ResponseLoginDto>
            .Ok(new ResponseLoginDto
            {
                RefreshToken = response.RefreshToken,
                Token = response.Token,
            });
    }

    private async Task<bool> CnpjJaExiste(string documento)
    {
        var hash = _hashService.GetHash(documento);

        return await _usuarioRepository.GetByCnpjHashAsync(hash) != null;
    }

    private (string Hash, string Encrypt) ProtectDocumento(string documento)
    {
        var hash = _hashService.GetHash(documento);
        var encrypt = _aesService.Encrypt(documento);

        return (hash, encrypt);
    }

    private UsuarioJuridico CriarUsuarioJuridico(RequestCadastroUsuarioPjDto dto)
    {
        return new UsuarioJuridico
        {
            Email = dto.Email,
            InscricaoEstadual = dto.InscricaoEstadual,
            NomeFantasia = dto.NomeFantasia,
            RazaoSocial = dto.RazaoSocial,
            ResponsavelCompra = dto.ResponsavelCompra,
            Telefone = dto.Telefone
        };
    }
    private void PrepararUsuario(Usuario usuario, Role role, string senha)
    {
        usuario.Carrinho = new Carrinho();
        usuario.Role = role;
        usuario.Senha = _passwordService.HashedPassword(usuario, senha);
    }
    public async Task<OperationResult<UsuarioJuridico>> UpdateUsuarioPjAsync(int id, RequestUpdateUsuarioPjDto updateCadastroPjUsuario)
    {

        var usuario = await _usuarioRepository.GetPjByIdAsync(id);
        if (usuario is null)
        {
            return OperationResult<UsuarioJuridico>.Fail("Falha ao encontrar o Usuario");
        }

        usuario.NomeFantasia = updateCadastroPjUsuario.NomeFantasia ?? usuario.NomeFantasia;
        usuario.RazaoSocial = updateCadastroPjUsuario.RazaoSocial ?? usuario.RazaoSocial;
        usuario.ResponsavelCompra = updateCadastroPjUsuario.ResponsavelCompra ?? usuario.ResponsavelCompra;
        usuario.InscricaoEstadual = updateCadastroPjUsuario.InscricaoEstadual ?? usuario.InscricaoEstadual;
        //usuario.Endereco = updateCadastroPjUsuario.Endereco ?? usuario.Endereco;
        usuario.Telefone = updateCadastroPjUsuario.Telefone ?? usuario.Telefone;
        usuario.Email = updateCadastroPjUsuario.Email ?? usuario.Email;

        await _usuarioRepository.UpdateAsync(usuario);
        return OperationResult<UsuarioJuridico>.Ok(usuario);
    }
}
