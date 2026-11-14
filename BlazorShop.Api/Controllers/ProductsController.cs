using BlazorShop.Api.Mappings;
using BlazorShop.Api.Mappings.Products;
using BlazorShop.Api.Repositories.Interfaces;
using BlazorShop.Models.DTOs.ProductDtos;
using Microsoft.AspNetCore.Mvc;

namespace BlazorShop.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Tags("Produtos")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductsController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductRequestDto>>> GetItems()
    {
        var products = await _productRepository.GetItems();
        if (!products.Success)
        {
            return BadRequest("FALHA AO ENCONTRAR OS PRODUTOS");
        }
        if (products.Value is null)
        {
            return NotFound("Produto não encontrado");
        }
        else
        {
            var productsDto = products.Value.ToDto();
            return Ok(productsDto);
        }
    }


    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductRequestDto>> GetItem(int id)
    {
        var product = await _productRepository.GetItem(id);
        if (!product.Success)
        {
            return BadRequest("FALHA AO ENCONTRAR O PRODUTO");
        }
        if (product.Value is null)
        {
            return NotFound("Produto não encontrado");
        }
        var productsDto = product.Value.ToDto();
        return Ok(productsDto);

    }

    [HttpGet]
    [Route("GetItemByCategoria/{categoriaId:int}")]
    public async Task<ActionResult<IEnumerable<ProductRequestDto>>> GetItemByCategory(int categoryId)
    {
        var products = await _productRepository.GetItemsPorCategory(categoryId);
        if (!products.Success)
        {
            return BadRequest("FALHA AO ENCONTRAR OS PRODUTOS");
        }
        if (products.Value is null)
        {
            return NotFound("Produtos não encontrados");
        }
        var productsDto = products.Value.ToDto();
        return Ok(productsDto);
    }

}
