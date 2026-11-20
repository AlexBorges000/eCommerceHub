using BlazorShop.Api.Security.Authentication.Interfaces;
using BlazorShop.Api.Services.Address.Interfaces;
using BlazorShop.Models.DTOs.Address;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorShop.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Tags("Endereco")]
public class AddressController(IAddressService addressService,
                                ICurrentUser currentUser) : ControllerBase
{
    private readonly IAddressService _addressService = addressService;
    private readonly ICurrentUser _currentUser = currentUser;

    [Authorize]
    [HttpPost()]
    public async Task<ActionResult> InsertAddressAsync(AddressAddRequestDto requestAddAddressDto)
    {

        var userId = _currentUser.UserId;
        var address = await _addressService.AddAsync(userId, requestAddAddressDto);
        if (!address.Success)
        {
            return BadRequest(address.Message);
        }
        return Ok(address.Value);
    }

    [Authorize]
    [HttpPatch("{id:int}")]
    public async Task<ActionResult> UpdateAddressAsync(int id, AddressUpdateRequestDto requestUpdateAddressDto)
    {

        var userId = _currentUser.UserId;
        var address = await _addressService.UpdateAsync(id, userId, requestUpdateAddressDto);
        if (!address.Success)
        {
            return BadRequest(address.Message);
        }
        return Ok(address.Value);
    }

    [Authorize]
    [HttpGet()]
    public async Task<ActionResult> AllAddressAsync()
    {
        var userId = _currentUser.UserId;
        var address = await _addressService.GetAllByUserIdAsync(userId);
        if (!address.Success)
        {
            return BadRequest(address.Message);
        }
        return Ok(address.Value);
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAddressAsync(int id)
    {
        var userId = _currentUser.UserId;
        var response = await _addressService.DeleteAsync(id, userId);
        if (!response.Success)
        {
            return BadRequest(response.Message);
        }
        return Ok();
    }
}
