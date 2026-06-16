using BlazorShop.Api.Mappings;
using BlazorShop.Api.Repositories.Interfaces;
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
    public async Task<ActionResult<IEnumerable<CarrinhoItemDto>>> GetItens(int usuarioId)
    {
        try
        {
            var carrinhoItens = await _carrinhoCompraRepository.GetItens(usuarioId);
            if (carrinhoItens == null)
            {
                return NoContent();
            }
            var produtos = await _produtoRepository.GetItens();
            if (produtos == null)
            {
                throw new Exception("Produtos não encontrados.");
            }
            var carrinhoItensDto = carrinhoItens.ConverterCarrinhoItemParaDto(produtos);
            return Ok(carrinhoItensDto);
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
            var produto = await _produtoRepository.GetItem(carrinhoItem.ProdutoId);
            if (produto == null)
            {
                return NotFound("Produto não encontrado.");
            }
            var carrinhoItemDto = carrinhoItem.ConverterCarrinhoItemParaDto(produto);
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
            var produto = await _produtoRepository.GetItem(novoCarrinhoItem.ProdutoId);
            if (produto == null)
            {
                throw new Exception($"Produto com ID {novoCarrinhoItem.ProdutoId} não encontrado.");
            }
            var carrinhoItemDto = novoCarrinhoItem.ConverterCarrinhoItemParaDto(produto);
            return CreatedAtAction(nameof(GetItem), new { id = carrinhoItemDto.Id }, carrinhoItemDto);
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
            var produto = await _produtoRepository.GetItem(carrinhoItem.ProdutoId);

            if (produto == null)
                return NotFound();
            await _carrinhoCompraRepository.DeleteItem(carrinhoItem.Id);
            var carrinhoItemDto = carrinhoItem.ConverterCarrinhoItemParaDto(produto);
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
            var produto = await _produtoRepository.GetItem(carrinhoItem.ProdutoId);
            var carrinhoItemDto = carrinhoItem.ConverterCarrinhoItemParaDto(produto);
            return Ok(carrinhoItemDto);

        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
