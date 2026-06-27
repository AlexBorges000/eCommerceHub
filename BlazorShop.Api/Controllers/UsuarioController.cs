using BlazorShop.Api.Mappings;
using BlazorShop.Api.Repositories.Interfaces;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.UsuarioDtos;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet("{id:int}")]
    public async Task<ActionResult<OperationResult<ResponseGetUsuarioDto>>> GetUsuario(int id)
    {
        var usuario = await _usuarioRepository.GetUsuario(id);
        if (!usuario.Success)
        {
            return NotFound();
        }
        if (usuario.Value is not null)
        {
            var usuarioDto = usuario.Value.GetUsuarioParaDto();
            return Ok(OperationResult<ResponseGetUsuarioDto>.Ok(usuarioDto.Value));
        }
        return NoContent();
    }

    [HttpPatch("{id:int}/senha")]
    public async Task<ActionResult<OperationResult<UpdateSenhaUsuarioDto>>> ChangePassword(int id, UpdateSenhaUsuarioDto updateSenhaUsuarioDto)
    {
        var response = await _usuarioRepository.ChangePassword(id, updateSenhaUsuarioDto);
        if (!response.Success)
        {
            return BadRequest(OperationResult<UpdateCadastroUsuarioDto>.Fail(response.Message));
        }
        return Ok(response);
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult<OperationResult<UpdateCadastroUsuarioDto>>> UpdateUser(int id, UpdateCadastroUsuarioDto updateCadastroUsuarioDto)
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
    public async Task<ActionResult<OperationResult<CadastroUsuarioDto>>> InsertUsuario(CadastroUsuarioDto cadastroUsuarioDto)
    { 
        var usuario = await _usuarioRepository.InsertUsuario(cadastroUsuarioDto);
        if (cadastroUsuarioDto is null)
        {
            return BadRequest(OperationResult<CadastroUsuarioDto>.Fail("Dados inválidos"));
        }
        if (!usuario.Success)
        {
            return BadRequest(OperationResult<CadastroUsuarioDto>.Fail(usuario.Message));
        }
        return Ok(OperationResult<CadastroUsuarioDto>.Ok(cadastroUsuarioDto));
    }
}
