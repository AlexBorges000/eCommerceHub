using BlazorShop.Api.Security.Authentication;
using BlazorShop.Api.Security.Authentication.Interfaces;
using BlazorShop.Api.Services.Address.Interfaces;
using BlazorShop.Api.Services.Users.Interfaces;
using BlazorShop.Api.Validations;
using BlazorShop.Api.Validations.Users.Registration;
using BlazorShop.Models.DTOs.UserDtos.Login;
using BlazorShop.Models.DTOs.UserDtos.Registration;
using BlazorShop.Models.DTOs.UserDtos.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BlazorShop.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Tags("Usuarios")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IIndividualUserService _userFisicoService;
    private readonly IBusinessUserService _userJuridicoService;
    private readonly IAddressService _addressService;
    private readonly ICurrentUser _currentUser;
    public UserController(IUserService userService
                           , IIndividualUserService userFisicoService
                           , IBusinessUserService userJuridicoService
                           , IAddressService addressService
                           , ICurrentUser currentUser)
    {
        _userService = userService;
        _userFisicoService = userFisicoService;
        _userJuridicoService = userJuridicoService;
        _addressService = addressService;
        _currentUser = currentUser;
    }

    [Authorize]
    [HttpPatch("me/senha")]
    public async Task<ActionResult> ChangePassword(int id, UserPasswordUpdateRequestDto updatePasswordUserDto)
    {
        id = _currentUser.UserId;
        var response = await _userService.ChangePassword(id, updatePasswordUserDto);
        if (!response.Success)
        {
            return BadRequest(response.Message);
        }
        return Ok();
    }

    [Authorize]
    [HttpPatch("pj/me")]
    public async Task<ActionResult> UpdateBusinessUser(int id, RequestUpdateBusinessUserDto updateRegistrationUserDto)
    {
        id = _currentUser.UserId;
        var user = await _userJuridicoService.UpdateBusinessUserAsync(id, updateRegistrationUserDto);
        if (!user.Success)
        {
            return BadRequest(user.Message);
        }
        return Ok(user.Value);
    }

    [Authorize]
    [HttpPatch("pf/me")]
    public async Task<ActionResult> UpdateIndividualUser(int id, IndividualUserUpdateRequestDto updateRegistrationUserDto)
    {
        id = _currentUser.UserId;
        var user = await _userFisicoService.UpdateIndividualUserAsync(id, updateRegistrationUserDto);
        if (!user.Success)
        {
            return BadRequest(user.Message);
        }
        return Ok();
    }

    [HttpPost("pj")]
    public async Task<ActionResult> InsertBusinessUser(BusinessUserRegistrationRequestDto cadastroUserDto)
    {
        var user = await _userJuridicoService.InsertBusinessUserAsync(cadastroUserDto);
        if (!user.Success)
        {
            return BadRequest(user.Message);
        }
        return Ok();
    }

    [HttpPost("pf")]
    public async Task<ActionResult> InsertIndividualUser(IndividualUserRegistrationRequestDto cadastroUserDto)
    {
        var user = await _userFisicoService.InsertIndividualUserAsync(cadastroUserDto);
        if (!user.Success)
        {
            return BadRequest(user.Message);
        }
        return Ok(user.Value);
    }

    [Authorize]
    [HttpDelete("me")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        id = _currentUser.UserId;
        var user = await _userService.DeleteUserAsync(id);
        if (!user.Success)
        {
            return BadRequest(user.Message);
        }
        return Ok(user.Message);
    }
}
