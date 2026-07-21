using BlazorShop.Api.Security.Authentication.Interfaces;
using BlazorShop.Api.Services.Address.Interfaces;
using BlazorShop.Models.DTOs.Endereco;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorShop.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Tags("Endereco")]
public class EnderecoController(IEnderecoService enderecoService,
                                ICurrentUser currentUser) : ControllerBase
{
    private readonly IEnderecoService _enderecoService = enderecoService;
    private readonly ICurrentUser _currentUser = currentUser;

    [Authorize]
    [HttpPost()]
    public async Task<ActionResult> InsertEnderecoAsync(RequestAddEnderecoDto requestAddEnderecoDto)
    {
        var userId = _currentUser.UserId;
        var endereco = await _enderecoService.AddAsync(userId, requestAddEnderecoDto);
        if (!endereco.Success)
        {
            return BadRequest(endereco.Message);
        }
        return Ok(endereco.Value);
    }

    [Authorize]
    [HttpPatch("{id:int}")]
    public async Task<ActionResult> UpdateEnderecoAsync(int id, RequestUpdateEnderecoDto requestUpdateEnderecoDto)
    {
        var userId = _currentUser.UserId;
        var endereco = await _enderecoService.UpdateAsync(id, userId, requestUpdateEnderecoDto);
        if (!endereco.Success)
        {
            return BadRequest(endereco.Message);
        }
        return Ok(endereco.Value);
    }

    [Authorize]
    [HttpGet()]
    public async Task<ActionResult> AllEnderecoAsync()
    {
        var userId = _currentUser.UserId;
        var endereco = await _enderecoService.GetAllByUserIdAsync(userId);
        if (!endereco.Success)
        {
            return BadRequest(endereco.Message);
        }
        return Ok(endereco.Value);
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteEnderecoAsync(int id)
    {
        var userId = _currentUser.UserId;
        var response = await _enderecoService.DeleteAsync(id, userId);
        if (!response.Success)
        {
            return BadRequest(response.Message);
        }
        return Ok();
    }
}
