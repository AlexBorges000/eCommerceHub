using BlazorShop.Api.Mappings;
using BlazorShop.Api.Mappings.Produtos;
using BlazorShop.Api.Repositories.Interfaces;
using BlazorShop.Models.DTOs.ProdutoDtos;
using Microsoft.AspNetCore.Mvc;

namespace BlazorShop.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Tags("Produtos")]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutosController(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RequestGetProdutoDto>>> GetItens()
    {
        var produtos = await _produtoRepository.GetItens();
        if (!produtos.Success)
        {
            return BadRequest("FALHA AO ENCONTRAR OS PRODUTOS");
        }
        if (produtos.Value is null)
        {
            return NotFound("Produto não encontrado");
        }
        else
        {
            var produtosDto = produtos.Value.ToDto();
            return Ok(produtosDto);
        }
    }


    [HttpGet("{id:int}")]
    public async Task<ActionResult<RequestGetProdutoDto>> GetItem(int id)
    {
        var produto = await _produtoRepository.GetItem(id);
        if (!produto.Success)
        {
            return BadRequest("FALHA AO ENCONTRAR O PRODUTO");
        }
        if (produto.Value is null)
        {
            return NotFound("Produto não encontrado");
        }
        var produtosDto = produto.Value.ToDto();
        return Ok(produtosDto);

    }

    [HttpGet]
    [Route("GetItemByCategoria/{categoriaId:int}")]
    public async Task<ActionResult<IEnumerable<RequestGetProdutoDto>>> GetItemByCategoria(int categoriaId)
    {
        var produtos = await _produtoRepository.GetItensPorCategoria(categoriaId);
        if (!produtos.Success)
        {
            return BadRequest("FALHA AO ENCONTRAR OS PRODUTOS");
        }
        if (produtos.Value is null)
        {
            return NotFound("Produtos não encontrados");
        }
        var produtosDto = produtos.Value.ToDto();
        return Ok(produtosDto);
    }

}
