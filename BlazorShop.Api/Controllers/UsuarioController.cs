using BlazorShop.Api.Security.Authentication;
using BlazorShop.Api.Security.Authentication.Interfaces;
using BlazorShop.Api.Services.Address.Interfaces;
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
    private readonly ICurrentUser _currentUser;
    public UsuarioController(IUsuarioService usuarioService
                           , IUsuarioFisicoService usuarioFisicoService
                           , IUsuarioJuridicoService usuarioJuridicoService
                           , IEnderecoService enderecoService
                           , ICurrentUser currentUser)
    {
        _usuarioService = usuarioService;
        _usuarioFisicoService = usuarioFisicoService;
        _usuarioJuridicoService = usuarioJuridicoService;
        _enderecoService = enderecoService;
        _currentUser = currentUser;
    }

    [Authorize]
    [HttpPatch("me/senha")]
    public async Task<ActionResult> ChangePassword(int id, RequestUpdateSenhaUsuarioDto updateSenhaUsuarioDto)
    {
        id = _currentUser.UserId;
        var response = await _usuarioService.ChangePassword(id, updateSenhaUsuarioDto);
        if (!response.Success)
        {
            return BadRequest(response.Message);
        }
        return Ok();
    }

    [Authorize]
    [HttpPatch("pj/me")]
    public async Task<ActionResult> UpdateUsuarioPj(int id, RequestUpdateUsuarioPjDto updateCadastroUsuarioDto)
    {
        id = _currentUser.UserId;
        var user = await _usuarioJuridicoService.UpdateUsuarioPjAsync(id, updateCadastroUsuarioDto);
        if (!user.Success)
        {
            return BadRequest(user.Message);
        }
        return Ok(user.Value);
    }

    [Authorize]
    [HttpPatch("pf/me")]
    public async Task<ActionResult> UpdateUsuarioPf(int id, RequestUpdateUsuarioPfDto updateCadastroUsuarioDto)
    {
        id = _currentUser.UserId;
        var user = await _usuarioFisicoService.UpdateUsuarioPfAsync(id, updateCadastroUsuarioDto);
        if (!user.Success)
        {
            return BadRequest(user.Message);
        }
        return Ok();
    }

    [HttpPost("pj")]
    public async Task<ActionResult> InsertPjUsuario(RequestCadastroUsuarioPjDto cadastroUsuarioDto)
    {
        var usuario = await _usuarioJuridicoService.InsertUsuarioPjAsync(cadastroUsuarioDto);
        if (!usuario.Success)
        {
            return BadRequest(usuario.Message);
        }
        return Ok();
    }

    [HttpPost("pf")]
    public async Task<ActionResult> InsertPfUsuario(RequestCadastroUsuarioPfDto cadastroUsuarioDto)
    {
        var usuario = await _usuarioFisicoService.InsertUsuarioPfAsync(cadastroUsuarioDto);
        if (!usuario.Success)
        {
            return BadRequest(usuario.Message);
        }
        return Ok(usuario.Value);
    }

    [Authorize]
    [HttpDelete("me")]
    public async Task<ActionResult> DeleteUsuario(int id)
    {
        id = _currentUser.UserId;
        var usuario = await _usuarioService.DeleteUsuarioAsync(id);
        if (!usuario.Success)
        {
            return BadRequest(usuario.Message);
        }
        return Ok(usuario.Message);
    }
}
