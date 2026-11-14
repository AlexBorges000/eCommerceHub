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

public class IndividualUserService(IUserRepository userRepository,
                                IHashService hashService,
                                IAesService aesService,
                                IRoleService roleService,
                                IPasswordService passwordService,
                                IAuthService authService) : IIndividualUserService
{

    private readonly IUserRepository _userRepository = userRepository;
    private readonly IHashService _hashService = hashService;
    private readonly IAesService _aesService = aesService;
    private readonly IRoleService _roleService = roleService;
    private readonly IPasswordService _passwordService = passwordService;
    private readonly IAuthService _authService = authService;

    public async Task<OperationResult<ResponseLoginDto>> InsertIndividualUserAsync(IndividualUserRegistrationRequestDto requestRegistrationIndividualUserDto)
    {
        var role = (await _roleService.GetClienteRoleAsync()).Value ??
            throw new InvalidOperationException("A role padrão 'Cliente' não foi encontrada."); ;

        if (requestRegistrationIndividualUserDto.Password != requestRegistrationIndividualUserDto.ConfirmaPassword)
        {
            return OperationResult<ResponseLoginDto>.Fail("As senhas não coincidem");
        }

        if (await TaxIdAlreadyExists(requestRegistrationIndividualUserDto.TaxId))
        {
            return OperationResult<ResponseLoginDto>.Fail("CPF ja cadastrado!");
        }

        var userFisico = requestRegistrationIndividualUserDto.ToEntity();

        var documento = ProtectDocumento(requestRegistrationIndividualUserDto.TaxId);

        userFisico.HashTaxId = documento.Hash;
        userFisico.EncryptTaxId = documento.Encrypt;

        PrepareUser(userFisico, role, requestRegistrationIndividualUserDto.Password);

        await _userRepository.AddAsync(userFisico);

        var response = await _authService.CreateSessionAsync(userFisico);

        return OperationResult<ResponseLoginDto>
            .Ok(new ResponseLoginDto
            {
                RefreshToken = response.RefreshToken,
                Token = response.Token,
            });
    }

    private (string Hash, string Encrypt) ProtectDocumento(string documento)
    {
        var hash = _hashService.GetHash(documento);
        var encrypt = _aesService.Encrypt(documento);

        return (hash, encrypt);
    }

    private async Task<bool> TaxIdAlreadyExists(string documento)
    {
        var hash = _hashService.GetHash(documento);

        return await _userRepository.GetByTaxIdHashAsync(hash) != null;
    }

    private void PrepareUser(User user, Role role, string password)
    {
        user.Cart = new Cart();
        user.Role = role;
        user.Password = _passwordService.HashedPassword(user, password);
    }

    public async Task<OperationResult<IndividualUser>> UpdateIndividualUserAsync(int id, IndividualUserUpdateRequestDto updateRegistrationIndividualUser)
    {
        if (string.IsNullOrWhiteSpace(updateRegistrationIndividualUser.Phone))
            return OperationResult<IndividualUser>.Fail("Necessario Telefone para atulizar");

        var user = await _userRepository.GetIndividualByIdAsync(id);
        if (user is null)
        {
            return OperationResult<IndividualUser>.Fail("Falha ao encontrar o Usuario");
        }

        updateRegistrationIndividualUser.UpdateEntity(user);

        await _userRepository.UpdateAsync(user);
        return OperationResult<IndividualUser>.Ok(user);
    }

}
