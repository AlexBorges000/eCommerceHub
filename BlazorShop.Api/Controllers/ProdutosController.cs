using BlazorShop.Api.Mappings;
using BlazorShop.Api.Repositories.Interfaces;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.ProdutoDtos;
using Microsoft.AspNetCore.Mvc;

namespace BlazorShop.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutosController(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    [HttpGet]
    public async Task<ActionResult<OperationResult<IEnumerable<ProdutoDto>>>> GetItens()
    {


        var produtos = await _produtoRepository.GetItens();
        if (!produtos.Success)
        {
            return NotFound(OperationResult<IEnumerable<ProdutoDto>>.Fail("FALHA AO ENCONTRAR OS PRODUTOS"));
        }
        else
        {
            var produtosDto = produtos.Value.ConverterProdutosParaDto();
            return Ok(produtosDto.Value);
        }
    }


    [HttpGet("{id:int}")]
    public async Task<ActionResult<OperationResult<ProdutoDto>>> GetItem(int id)
    {

        var produto = await _produtoRepository.GetItem(id);
        if (!produto.Success)
        {
            return NotFound(OperationResult<IEnumerable<ProdutoDto>>.Fail($"FALHA AO ENCONTRAR O PRODUTO DE ID: {id} "));
        }
        else
        {
            var produtosDto = produto.Value.ConverterProdutoParaDto();
            return Ok(produtosDto.Value);
        }
    }

    [HttpGet]
    [Route("GetItemByCategoria/{categoriaId:int}")]
    public async Task<ActionResult<IEnumerable<ProdutoDto>>> GetItemByCategoria(int categoriaId)
    {

        var produtos = await _produtoRepository.GetItensPorCategoria(categoriaId);
        var produtosDto = produtos.Value.ConverterProdutosParaDto();
        return Ok(OperationResult<IEnumerable<ProdutoDto>>.Ok(produtosDto.Value));
    }

}
