using BlazorShop.Api.Entities;
using BlazorShop.Api.Mappings.Users;
using BlazorShop.Api.Repositories.Interfaces;
using BlazorShop.Api.Security.Authentication.Interfaces;
using BlazorShop.Api.Security.Password.Intefaces;
using BlazorShop.Api.Security.Security.Interfaces;
using BlazorShop.Api.Services.Auth.Interfaces;
using BlazorShop.Api.Services.Users.Interfaces;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.UserDtos.Registration;
using BlazorShop.Models.DTOs.UserDtos.Update;

namespace BlazorShop.Api.Services.Users;

public class BusinessUserService(IUserRepository userRepository,
                                    IHashService hashService,
                                    IAesService aesService,
                                    IRoleService roleService,
                                    IPasswordService passwordService,
                                    IAuthService authService) : IBusinessUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IHashService _hashService = hashService;
    private readonly IAesService _aesService = aesService;
    private readonly IRoleService _roleService = roleService;
    private readonly IPasswordService _passwordService = passwordService;
    private readonly IAuthService _authService = authService;

    public async Task<OperationResult<ResponseLoginDto>> InsertBusinessUserAsync(BusinessUserRegistrationRequestDto requestRegistrationBusinessUserDto)
    {
        if (requestRegistrationBusinessUserDto.Password != requestRegistrationBusinessUserDto.ConfirmPassword)
        {
            return OperationResult<ResponseLoginDto>.Fail("As senhas não coincidem");
        }

        var role = (await _roleService.GetClienteRoleAsync()).Value ??
            throw new InvalidOperationException("A role padrão 'Cliente' não foi encontrada."); ;

        if (await CnpjAlreadyExists(requestRegistrationBusinessUserDto.CNPJ))
        {
            return OperationResult<ResponseLoginDto>.Fail("CNPJ ja cadastrado!");
        }
        var userJuridico = requestRegistrationBusinessUserDto.ToEntity();

        var documento = ProtectDocumento(requestRegistrationBusinessUserDto.CNPJ);

        userJuridico.HashCnpj = documento.Hash;
        userJuridico.EncryptCnpj = documento.Encrypt;

        PrepareUser(userJuridico, role, requestRegistrationBusinessUserDto.Password); 

        await _userRepository.AddAsync(userJuridico);

        var response = await _authService.CreateSessionAsync(userJuridico);

        return OperationResult<ResponseLoginDto>
            .Ok(new ResponseLoginDto
            {
                RefreshToken = response.RefreshToken,
                Token = response.Token,
            });
    }

    private async Task<bool> CnpjAlreadyExists(string documento)
    {
        var hash = _hashService.GetHash(documento);

        return await _userRepository.GetByCnpjHashAsync(hash) != null;
    }

    private (string Hash, string Encrypt) ProtectDocumento(string documento)
    {
        var hash = _hashService.GetHash(documento);
        var encrypt = _aesService.Encrypt(documento);

        return (hash, encrypt);
    }

    private void PrepareUser(User user, Role role, string password)
    {
        user.Order = new Order();
        user.Role = role;
        user.Password = _passwordService.HashedPassword(user, password);
    }
    public async Task<OperationResult<BusinessUser>> UpdateBusinessUserAsync(int id, RequestUpdateBusinessUserDto updateRegistrationBusinessUser)
    {

        var user = await _userRepository.GetBusinessByIdAsync(id);
        if (user is null)
        {
            return OperationResult<BusinessUser>.Fail("Falha ao encontrar o Usuario");
        }
        updateRegistrationBusinessUser.UpdateEntity(user);

        await _userRepository.UpdateAsync(user);
        return OperationResult<BusinessUser>.Ok(user);
    }
}
