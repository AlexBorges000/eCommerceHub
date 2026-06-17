using BlazorShop.Api.Mappings;
using BlazorShop.Api.Repositories.Interfaces;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.CarrinhoDtos;
using Microsoft.AspNetCore.Mvc;

namespace BlazorShop.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
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
    public async Task<ActionResult<OperationResult<IEnumerable<CarrinhoItemDto>>>> GetItens(int usuarioId)
    {
        try
        {
            var carrinhoItens = await _carrinhoCompraRepository.GetItens(usuarioId);
            if (carrinhoItens == null)
            {
                return OperationResult<IEnumerable<CarrinhoItemDto>>.Fail("NENHUM ITEM ENCONTRADO");
            }
            var produtos = await _produtoRepository.GetItens();
            if (produtos == null)
            {
                return OperationResult<IEnumerable<CarrinhoItemDto>>.Fail("NENHUM ITEM ENCONTRADO");
            }
            var carrinhoItensDto = carrinhoItens.Value.ConverterCarrinhoItemParaDto(produtos);
            return Ok(OperationResult<IEnumerable<CarrinhoItemDto>>.Ok(carrinhoItensDto.Value));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter itens do carrinho para o usuário {UsuarioId}", usuarioId);
            return StatusCode(500, "Ocorreu um erro ao processar sua solicitação." + ex.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CarrinhoItemDto>> GetItem(int id)
    {
        try
        {
            var carrinhoItem = await _carrinhoCompraRepository.GetItem(id);
            if (carrinhoItem == null)
            {
                return NotFound("Item não encontrado.");
            }
            var produto = await _produtoRepository.GetItem(carrinhoItem.Value.ProdutoId);
            if (produto == null)
            {
                return NotFound("Produto não encontrado.");
            }
            var carrinhoItemDto = carrinhoItem.Value.ConverterCarrinhoItemParaDto(produto);
            return Ok(carrinhoItemDto);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter item do carrinho com ID {Id}", id);
            return StatusCode(500, "Ocorreu um erro ao processar sua solicitação." + ex.Message);

        }
    }

    [HttpPost]
    public async Task<ActionResult<CarrinhoItemDto>> PostItem([FromBody] CarrinhoItemAdicionaDto carrinhoItemAdicionaDto)
    {
        try
        {
            var novoCarrinhoItem = await _carrinhoCompraRepository.AdicionaItem(carrinhoItemAdicionaDto);
            if (novoCarrinhoItem == null)
            {
                return NoContent();
            }
            var produto = await _produtoRepository.GetItem(novoCarrinhoItem.Value.ProdutoId);
            if (produto == null)
            {
                throw new Exception($"Produto com ID {novoCarrinhoItem.Value.ProdutoId} não encontrado.");
            }
            var carrinhoItemDto = novoCarrinhoItem.Value.ConverterCarrinhoItemParaDto(produto);
            return CreatedAtAction(nameof(GetItem), new { id = carrinhoItemDto.Value.Id }, carrinhoItemDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao adicionar item ao carrinho para o usuário {UsuarioId}", carrinhoItemAdicionaDto.CarrinhoId);
            return StatusCode(500, "Ocorreu um erro ao processar sua solicitação." + ex.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<CarrinhoItemDto>> DeleteItem(int id)
    {
        try
        {
            var carrinhoItem = await _carrinhoCompraRepository.GetItem(id);
            if (carrinhoItem == null)
            {
                return NotFound();
            }
            var produto = await _produtoRepository.GetItem(carrinhoItem.Value.ProdutoId);

            if (produto == null)
                return NotFound();
            await _carrinhoCompraRepository.DeleteItem(carrinhoItem.Value.Id);
            var carrinhoItemDto = carrinhoItem.Value.ConverterCarrinhoItemParaDto(produto);
            return Ok(carrinhoItemDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro ao processar sua solicitação." + ex.Message);
        }
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult<CarrinhoItemDto>> AtualizaQuantidade(int id, CarrinhoItemAtualizaQuantidadeDto carrinhoItemAtualizaQuantidadeDto)
    {

        try
        {

            var carrinhoItem = await _carrinhoCompraRepository.AtualizaQuantidade(id,
                                   carrinhoItemAtualizaQuantidadeDto);

            if (carrinhoItem == null)
            {
                return NotFound();
            }
            var produto = await _produtoRepository.GetItem(carrinhoItem.Value.ProdutoId);
            var carrinhoItemDto = carrinhoItem.Value.ConverterCarrinhoItemParaDto(produto);
            return Ok(carrinhoItemDto);

        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
