using BlazorShop.Api.Mappings;
using BlazorShop.Api.Repositories.Interfaces;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.UsuarioDtos;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BlazorShop.Api.Controllers;

[Route("api/Usuarios")]
[ApiController]
[Tags("Usuarios")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;
    private ILogger<UsuarioController> _logger;

    public UsuarioController(IUsuarioRepository usuarioRepository, ILogger<UsuarioController> logger)
    {
        _usuarioRepository = usuarioRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<OperationResult<ResponseGetUsuarioDto>>> GetUsuario([FromQuery] string email)
    {
        var usuario = await _usuarioRepository.GetUsuario(email);
        if (!usuario.Success)
        {
            return NotFound();
        }
        if (usuario.Value is not null)
        {
            return Ok(usuario.Value);
        }
        return NoContent();
    }

    [HttpPatch("{id:int}/senha")]
    public async Task<ActionResult<OperationResult<RequestUpdateSenhaUsuarioDto>>> ChangePassword(int id, RequestUpdateSenhaUsuarioDto updateSenhaUsuarioDto)
    {
        var response = await _usuarioRepository.ChangePassword(id, updateSenhaUsuarioDto);
        if (!response.Success)
        {
            return BadRequest(OperationResult<RequestUpdateCadastroUsuarioDto>.Fail(response.Message));
        }
        return Ok(response);
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult<OperationResult<RequestUpdateCadastroUsuarioDto>>> UpdateUser(int id, RequestUpdateCadastroUsuarioDto updateCadastroUsuarioDto)
    {
        var user = await _usuarioRepository.UpdateUsuario(id, updateCadastroUsuarioDto);
        if (!user.Success)
        {
            return BadRequest();
        }
        var userDto = user.Value.ConverterUsuarioParaDto();
        return Ok(userDto.Value);
    }

    [HttpPost]
    public async Task<ActionResult<OperationResult<RequestCadastroUsuarioDto>>> InsertUsuario(RequestCadastroUsuarioDto cadastroUsuarioDto)
    { 
        var usuario = await _usuarioRepository.InsertUsuario(cadastroUsuarioDto);
        if (cadastroUsuarioDto is null)
        {
            return BadRequest(OperationResult<RequestCadastroUsuarioDto>.Fail("Dados inválidos"));
        }
        if (!usuario.Success)
        {
            return BadRequest(OperationResult<RequestCadastroUsuarioDto>.Fail(usuario.Message));
        }
        return Ok(OperationResult<RequestCadastroUsuarioDto>.Ok(cadastroUsuarioDto));
    }
}
