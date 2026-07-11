using BlazorShop.Api.Services;
using BlazorShop.Api.Services.Interfaces;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.UsuarioDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorShop.Api.Controllers;

[Route("api/Usuarios")]
[ApiController]
[Tags("Usuarios")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<RequestLoginDto>> GetUsuario([FromQuery] string email)
    {
        var usuario = await _usuarioService.GetAsync(email);
        if (!usuario.Success)
        {
            return NotFound(usuario.Message);
        }
        return Ok(usuario.Value);
    }

    [Authorize]
    [HttpPatch("{id:int}/senha")]
    public async Task<ActionResult> ChangePassword(int id, RequestUpdateSenhaUsuarioDto updateSenhaUsuarioDto)
    {
        var response = await _usuarioService.ChangePassword(id, updateSenhaUsuarioDto);
        if (!response.Success)
        {
            return BadRequest(response.Message);
        }
        return Ok();
    }

    [Authorize]
    [HttpPatch("{id:int}")]
    public async Task<ActionResult> UpdateUser(int id, RequestUpdateCadastroUsuarioDto updateCadastroUsuarioDto)
    {
        var user = await _usuarioService.UpdateAsync(id, updateCadastroUsuarioDto);
        if (!user.Success)
        {
            return BadRequest();
        }
        return Ok(user.Value);
    }

    [HttpPost]
    public async Task<ActionResult> InsertUsuario(RequestCadastroUsuarioDto cadastroUsuarioDto)
    {
        var usuario = await _usuarioService.InsertAsync(cadastroUsuarioDto);
        if (!usuario.Success)
        {
            return BadRequest(usuario.Message);
        }
        return Ok(cadastroUsuarioDto);
    }
    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteUsuario(int id) 
    {
        var usuario = await _usuarioService.DeleteUsuarioAsync(id);
        if (!usuario.Success)
        {
            return BadRequest(usuario.Message);
        }
        return Ok(usuario.Message);
    }

}
