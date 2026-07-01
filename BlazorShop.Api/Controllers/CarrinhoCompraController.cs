using BlazorShop.Api.Mappings;
using BlazorShop.Api.Repositories.Interfaces;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.CarrinhoDtos;
using Microsoft.AspNetCore.Mvc;

namespace BlazorShop.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Tags("CarrinhoCompras")]
public class CarrinhoCompraController : ControllerBase
{
    private readonly ICarrinhoCompraRepository _carrinhoCompraRepository;
    private readonly IProdutoRepository _produtoRepository;
    private ILogger<CarrinhoCompraController> _logger;

    public CarrinhoCompraController(ICarrinhoCompraRepository carrinhoCompraRepository
        , IProdutoRepository produtoRepository,
        ILogger<CarrinhoCompraController> logger)
    {
        _carrinhoCompraRepository = carrinhoCompraRepository;
        _produtoRepository = produtoRepository;
        _logger = logger;
    }


    [HttpGet]
    [Route("{usuarioId}/GetItens")]
    public async Task<ActionResult<OperationResult<IEnumerable<RequestGetCarrinhoItemDto>>>> GetItens(int usuarioId)
    {
        var carrinhoItens = await _carrinhoCompraRepository.GetItens(usuarioId);
        if (!carrinhoItens.Success)
        {
            return NotFound(OperationResult<IEnumerable<RequestGetCarrinhoItemDto>>.Fail("NENHUM ITEM ENCONTRADO"));
        }
        var produtos = await _produtoRepository.GetItens();
        if (!produtos.Success)
        {
            return NotFound(OperationResult<IEnumerable<RequestGetCarrinhoItemDto>>.Fail("NENHUM ITEM ENCONTRADO"));
        }
        var carrinhoItensDto = carrinhoItens.Value.ConverterCarrinhoItemParaDto(produtos.Value);
        return Ok(carrinhoItensDto.Value);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<OperationResult<RequestGetCarrinhoItemDto>>> GetItem(int id)
    {
        var carrinhoItem = await _carrinhoCompraRepository.GetItem(id);
        if (!carrinhoItem.Success)
        {
            return NotFound(OperationResult<RequestGetCarrinhoItemDto>.Fail($"Erro ao encontrar o carrinho do ID: {id}"));
        }
        var produto = await _produtoRepository.GetItem(carrinhoItem.Value.ProdutoId);
        if (produto is null)
        {
            return NotFound(OperationResult<RequestGetCarrinhoItemDto>.Fail("NENHUM PRODUTO ENCONTRADO"));
        }
        var carrinhoItemDto = carrinhoItem.Value.ConverterCarrinhoItemParaDto(produto.Value);
        return Ok(carrinhoItemDto.Value);
    }

    [HttpPost]
    public async Task<ActionResult<OperationResult<RequestGetCarrinhoItemDto>>> PostItem([FromBody] RequestCarrinhoItemAdicionaDto carrinhoItemAdicionaDto)
    {
        var novoCarrinhoItem = await _carrinhoCompraRepository.AdicionaItem(carrinhoItemAdicionaDto);
        if (!novoCarrinhoItem.Success)
        {
            return NoContent();
        }
        var produto = await _produtoRepository.GetItem(novoCarrinhoItem.Value.ProdutoId);
        if (produto is null)
        {
            return NotFound(OperationResult<RequestGetCarrinhoItemDto>.Fail($"Produto com ID {novoCarrinhoItem.Value.ProdutoId} não encontrado."));
        }
        var carrinhoItemDto = novoCarrinhoItem.Value.ConverterCarrinhoItemParaDto(produto.Value);
        return CreatedAtAction(nameof(GetItem), new { id = carrinhoItemDto.Value.Id }, carrinhoItemDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<OperationResult<RequestGetCarrinhoItemDto>>> DeleteItem(int id)
    {
        var carrinhoItem = await _carrinhoCompraRepository.GetItem(id);
        if (!carrinhoItem.Success)
        {
            return NotFound(OperationResult<RequestGetCarrinhoItemDto>.Fail("PRODUTO PARA DELETAR NÃO ENCONTRADO"));
        }
        var produto = await _produtoRepository.GetItem(carrinhoItem.Value.ProdutoId);

        if (produto is null)
            return NotFound(OperationResult<RequestGetCarrinhoItemDto>.Fail("PRODUTO PARA DELETAR NÃO ENCONTRADO"));

        await _carrinhoCompraRepository.DeleteItem(carrinhoItem.Value.Id);
        var carrinhoItemDto = carrinhoItem.Value.ConverterCarrinhoItemParaDto(produto.Value);
        return Ok(carrinhoItemDto.Value);
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult<OperationResult<RequestGetCarrinhoItemDto>>> AtualizaQuantidade(int id, RequestCarrinhoItemAtualizaQuantidadeDto carrinhoItemAtualizaQuantidadeDto)
    {
        var carrinhoItem = await _carrinhoCompraRepository.AtualizaQuantidade(id,
                               carrinhoItemAtualizaQuantidadeDto);
        if (!carrinhoItem.Success)
        {
            return NotFound(OperationResult<RequestGetCarrinhoItemDto>.Fail("PRODUTO NÃO ENCONTRADO PARA ATULIZAR A QUANTIDADE NO CARRINHO"));
        }
        var produto = await _produtoRepository.GetItem(carrinhoItem.Value.ProdutoId);
        var carrinhoItemDto = carrinhoItem.Value.ConverterCarrinhoItemParaDto(produto.Value);
        return Ok(carrinhoItemDto.Value);
    }

}
