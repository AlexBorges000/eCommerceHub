using BlazorShop.Api.Services.Usuarios.Interfaces;
using BlazorShop.Models.DTOs.UsuarioDtos.Cadastro;
using BlazorShop.Models.DTOs.UsuarioDtos.Login;
using BlazorShop.Models.DTOs.UsuarioDtos.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorShop.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Tags("Usuarios")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;
    private readonly IUsuarioFisicoService _usuarioFisicoService;
    private readonly IUsuarioJuridicoService _usuarioJuridicoService;
    private readonly IEnderecoService _enderecoService;

    public UsuarioController(IUsuarioService usuarioService
                           , IUsuarioFisicoService usuarioFisicoService
                           , IUsuarioJuridicoService usuarioJuridicoService
                           , IEnderecoService enderecoService)
    {
        _usuarioService = usuarioService;
        _usuarioFisicoService = usuarioFisicoService;
        _usuarioJuridicoService = usuarioJuridicoService;
        _enderecoService = enderecoService;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<RequestLoginDto>> GetUsuario([FromQuery] string email)
    {
        var usuario = await _usuarioService.GetByEmailAsync(email);
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
    [HttpPatch("pj/{id:int}")]
    public async Task<ActionResult> UpdateUsuarioPj(int id, RequestUpdateUsuarioPjDto updateCadastroUsuarioDto)
    {
        var user = await _usuarioJuridicoService.UpdateUsuarioPjAsync(id, updateCadastroUsuarioDto);
        if (!user.Success)
        {
            return BadRequest();
        }
        return Ok(user.Value);
    }

    [Authorize]
    [HttpPatch("pf/{id:int}")]
    public async Task<ActionResult> UpdateUsuarioPf(int id, RequestUpdateUsuarioPfDto updateCadastroUsuarioDto)
    {
        var user = await _usuarioFisicoService.UpdateUsuarioPfAsync(id, updateCadastroUsuarioDto);
        if (!user.Success)
        {
            return BadRequest();
        }
        return Ok(user.Value);
    }

    [HttpPost("pj")]
    public async Task<ActionResult> InsertPjUsuario(RequestCadastroUsuarioPjDto cadastroUsuarioDto)
    {
        var usuario = await _usuarioJuridicoService.InsertUsuarioPjAsync(cadastroUsuarioDto);
        if (!usuario.Success)
        {
            return BadRequest(usuario.Message);
        }
        return Ok(cadastroUsuarioDto);
    }

    [HttpPost("pf")]
    public async Task<ActionResult> InsertPfUsuario(RequestCadastroUsuarioPfDto cadastroUsuarioDto)
    {
        var usuario = await _usuarioFisicoService.InsertUsuarioPfAsync(cadastroUsuarioDto);
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
