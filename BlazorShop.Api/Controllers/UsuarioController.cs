using BlazorShop.Api.Entities;
using BlazorShop.Api.Mappings;
using BlazorShop.Api.Repositories.Interfaces;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.UsuarioDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlazorShop.Api.Controllers;

[Route("api/Usuarios")]
[ApiController]
public class UsuarioController: ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<OperationResult<Usuario>>> GetUsuario(int id)
    {
        var usuario = await _usuarioRepository.GetUsuario(id);
        if (!usuario.Success)
        {
            return NotFound();
        }
        if (usuario.Value is not null)
        {
            return Ok(OperationResult<Usuario>.Ok(usuario.Value));
        }
        return NoContent();
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult<OperationResult<Usuario>>> UpdateUser(int id, UpdateCadastroUsuarioDto updateCadastroUsuarioDto)
    {
        var user = await _usuarioRepository.UpdateUsuario(id, updateCadastroUsuarioDto);
        if(!user.Success)
        {
            return BadRequest();
        }
        var userDto = user.Value.ConverterUsuarioParaDto();
        return Ok(userDto.Value);
    }
}
