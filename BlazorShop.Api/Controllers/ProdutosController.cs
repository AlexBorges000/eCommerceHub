using BlazorShop.Api.Mappings;
using BlazorShop.Api.Repositories.Interfaces;
using BlazorShop.Models.DTOs.ProdutoDtos;
using Microsoft.AspNetCore.Mvc;

namespace BlazorShop.Api.Controllers;

[Route("api/Produtos")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutosController(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProdutoDto>>> GetItems()
    {
        try
        {
            var produtos = await _produtoRepository.GetItens();
            if (produtos is null)
            {
                return NotFound();
            }
            else
            {
                var produtosDto = produtos.ConverterProdutosParaDto();
                return Ok(produtosDto);
            }
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acessar o banco de dados");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProdutoDto>> GetItem(int id)
    {
        try
        {
            var produto = await _produtoRepository.GetItem(id);
            if (produto is null)
            {
                return NotFound();
            }
            else
            {
                var produtosDto = produto.ConverterProdutoParaDto();
                return Ok(produtosDto);
            }
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acessar o banco de dados");
        }
    }

    [HttpGet]
    [Route("GetItemByCategoria/{categoriaId:int}")]
    public async Task<ActionResult<IEnumerable<ProdutoDto>>> GetItemByCategoria(int categoriaId)
    {
        try
        {
            var produtos = await _produtoRepository.GetItensPorCategoria(categoriaId);
            var produtosDto = produtos.ConverterProdutosParaDto();
            return Ok(produtosDto);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acessar o banco de dados");
        }
    }
}
