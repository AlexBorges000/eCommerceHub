using BlazorShop.Api.Mappings.CarrinhoItens;
using BlazorShop.Api.Repositories.Interfaces;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.CarrinhoDtos;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize]
    [HttpGet]
    [Route("{usuarioId}/GetItens")]
    public async Task<ActionResult<IEnumerable<RequestGetCarrinhoItemDto>>> GetItens(int usuarioId)
    {
        var carrinhoItens = await _carrinhoCompraRepository.GetItens(usuarioId);
        if (!carrinhoItens.Success)
        {
            return BadRequest("NENHUM ITEM ENCONTRADO");
        }
        var produtos = await _produtoRepository.GetItens();
        if (!produtos.Success)
        {
            return BadRequest(OperationResult<IEnumerable<RequestGetCarrinhoItemDto>>.Fail("NENHUM ITEM ENCONTRADO"));
        }
        if(produtos.Value is null)
        {
            return NotFound("Produtos não encontrados");
        }
        var carrinhoItensDto = carrinhoItens.Value.ToDto();
        return Ok(carrinhoItensDto);
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<RequestGetCarrinhoItemDto>> GetItem(int id)
    {
        var carrinhoItem = await _carrinhoCompraRepository.GetItem(id);
        if (!carrinhoItem.Success)
        {
            return NotFound($"Erro ao encontrar o carrinho");
        }
        if (carrinhoItem.Value is null)
        {
            return NotFound("Carrinho não encontrados");
        }
        var produto = await _produtoRepository.GetItem(carrinhoItem.Value.ProdutoId);
        if (produto.Value is null)
        {
            return NotFound(OperationResult<RequestGetCarrinhoItemDto>.Fail("NENHUM PRODUTO ENCONTRADO"));
        }
        var carrinhoItemDto = carrinhoItem.Value.ToDto();
        return Ok(carrinhoItemDto);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestGetCarrinhoItemDto>> PostItem([FromBody] RequestCarrinhoItemAdicionaDto carrinhoItemAdicionaDto)
    {
        var novoCarrinhoItem = await _carrinhoCompraRepository.AdicionaItem(carrinhoItemAdicionaDto);
        if (!novoCarrinhoItem.Success)
        {
            return NoContent();
        }
        if (novoCarrinhoItem.Value is null)
        {
            return NotFound(OperationResult<RequestGetCarrinhoItemDto>.Fail("NENHUM PRODUTO PARA ADICIONAR ENCONTRADO"));
        }
        var produto = await _produtoRepository.GetItem(novoCarrinhoItem.Value.ProdutoId);
        if (produto.Value is null)
        {
            return NotFound($"Produto com ID {novoCarrinhoItem.Value.ProdutoId} não encontrado.");
        }
        var carrinhoItemDto = novoCarrinhoItem.Value.ToDto();
        if (carrinhoItemDto is null)
        {
            return NotFound("Nada encontrado no carrinho");
        }
        return CreatedAtAction(nameof(GetItem), new { id = carrinhoItemDto.Id }, carrinhoItemDto);
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<RequestGetCarrinhoItemDto>> DeleteItem(int id)
    {
        var carrinhoItem = await _carrinhoCompraRepository.GetItem(id);
        if (!carrinhoItem.Success)
        {
            return BadRequest("PRODUTO PARA DELETAR NÃO ENCONTRADO");
        }
        if (carrinhoItem.Value is null)
        {
            return NotFound($"NENHUM PRODUTO ENCONTRADO NO CARRINHO PARA DELETAR");
        }
        var produto = await _produtoRepository.GetItem(carrinhoItem.Value.ProdutoId);

        if (produto.Value is null)
            return NotFound("PRODUTO PARA DELETAR NÃO ENCONTRADO");

        await _carrinhoCompraRepository.DeleteItem(carrinhoItem.Value.Id);
        var carrinhoItemDto = carrinhoItem.Value.ToDto();
        return Ok(carrinhoItemDto);
    }

    [Authorize]
    [HttpPatch("{id:int}")]
    public async Task<ActionResult<RequestGetCarrinhoItemDto>> AtualizaQuantidade(int id, RequestCarrinhoItemAtualizaQuantidadeDto carrinhoItemAtualizaQuantidadeDto)
    {
        var carrinhoItem = await _carrinhoCompraRepository.AtualizaQuantidade(id,
                               carrinhoItemAtualizaQuantidadeDto);
        if (!carrinhoItem.Success)
        {
            return NotFound("PRODUTO NÃO ENCONTRADO PARA ATULIZAR A QUANTIDADE NO CARRINHO");
        }
        if (carrinhoItem.Value is null)
            return NotFound("PRODUTO PARA DELETAR NÃO ENCONTRADO");
        var produto = await _produtoRepository.GetItem(carrinhoItem.Value.ProdutoId);
        if (produto.Value is null)
            return NotFound("PRODUTO PARA DELETAR NÃO ENCONTRADO");
        var carrinhoItemDto = carrinhoItem.Value.ToDto();
        return Ok(carrinhoItemDto);
    }

}
