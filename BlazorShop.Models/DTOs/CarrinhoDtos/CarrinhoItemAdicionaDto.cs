using System.ComponentModel.DataAnnotations;

namespace BlazorShop.Models.DTOs.CarrinhoDtos;

public class CarrinhoItemAdicionaDto
{
    [Required]
    public int CarrinhoId { get; set; }
    [Required]
    public int ProdutoId { get; set; }
    [Required]
    public int Quantidade { get; set; }
}
