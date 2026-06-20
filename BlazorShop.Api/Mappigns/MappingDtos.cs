using BlazorShop.Api.Entities;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.CarrinhoDtos;
using BlazorShop.Models.DTOs.ProdutoDtos;
using BlazorShop.Models.DTOs.UsuarioDtos;
namespace BlazorShop.Api.Mappings;

public static class MappingDtos
{
    public static IEnumerable<ResponseProdutoCategoriaDto> ConverterCategoriaParaDto(this IEnumerable<Categoria> categorias)
    {

        return (from categoria in categorias
                select new ResponseProdutoCategoriaDto
                {
                    Id = categoria.Id,
                    Nome = categoria.Nome,
                    IconCSS = categoria.IconCSS
                }).ToList();
    }

    public static OperationResult<IEnumerable<ProdutoDto>> ConverterProdutosParaDto(this IEnumerable<Produto> produtos)
    {
        var itens = (from produto in produtos
                select new ProdutoDto
                {
                    Id = produto.Id,
                    Nome = produto.Nome,
                    Descricao = produto.Descricao,
                    Preco = produto.Preco,
                    ImagemUrl = produto.ImagemUrl,
                    Quantidade = produto.Quantidade,
                    CategoriaId = produto.CategoriaId,
                    CategoriaNome = produto.Categoria.Nome
                }).ToList();
        return OperationResult<IEnumerable<ProdutoDto>>.Ok(itens);
    }

    public static OperationResult<ProdutoDto> ConverterProdutoParaDto(this Produto produto)
    {
        var item = new ProdutoDto
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Descricao = produto.Descricao,
            Preco = produto.Preco,
            ImagemUrl = produto.ImagemUrl,
            Quantidade = produto.Quantidade,
            CategoriaId = produto.CategoriaId,
            CategoriaNome = produto.Categoria.Nome
        };
        return OperationResult<ProdutoDto>.Ok(item);
    }

    public static OperationResult<IEnumerable<CarrinhoItemDto>> ConverterCarrinhoItemParaDto(this IEnumerable<CarrinhoItem> carrinhoItens, IEnumerable<Produto> produtos)
    {  
        var carrinho =  (from carrinhoItem in carrinhoItens 
                join produto in produtos 
                on carrinhoItem.ProdutoId equals produto.Id
                select new CarrinhoItemDto
                {
                    Id = carrinhoItem.Id,
                    CarrinhoId = carrinhoItem.CarrinhoId,
                    ProdutoId = carrinhoItem.ProdutoId,
                    Quantidade = carrinhoItem.Quantidade,
                    ProdutoNome = produto.Nome,
                    ProdutoDescricao = produto.Descricao,
                    ProdutoImagemURL = produto.ImagemUrl,
                    Preco = produto.Preco,
                    PrecoTotal = produto.Preco * carrinhoItem.Quantidade
                }).ToList();
        if (carrinho is not null)
        {
            return OperationResult<IEnumerable<CarrinhoItemDto>>.Ok(carrinho);
        }
        else
        {
            return OperationResult<IEnumerable<CarrinhoItemDto>>.Fail("ERRO INTERNO AO MOSTRAR OS CARRINHOS");
        }
    }

    public static OperationResult<CarrinhoItemDto> ConverterCarrinhoItemParaDto(this CarrinhoItem carrinhoItem, Produto produto)
    {
        var carrinho =  new CarrinhoItemDto
        {
            Id = carrinhoItem.Id,
            ProdutoId = carrinhoItem.ProdutoId,
            ProdutoNome = produto.Nome,
            ProdutoDescricao = produto.Descricao,
            ProdutoImagemURL = produto.ImagemUrl,
            Preco = produto.Preco,
            CarrinhoId = carrinhoItem.CarrinhoId,
            Quantidade = carrinhoItem.Quantidade,
            PrecoTotal = produto.Preco * carrinhoItem.Quantidade
        };
        if(carrinho is not null)
        {
            return OperationResult<CarrinhoItemDto>.Ok(carrinho);
        }
        else
        {
            return OperationResult<CarrinhoItemDto>.Fail("ERRO INTERNO AO MOSTRAR OS CARRINHOS");
        }
    }

    public static CadastroUsuarioDto ConverterUsuarioParaDto(this Usuario usuario)
    {
        return new CadastroUsuarioDto
        {
            Email = usuario.Email,
            Endereco = usuario.Endereco,
            Telefone = usuario.Telefone,
            Senha = usuario.Senha,
            Nome = usuario.Nome,
            CPF = usuario.CPF,
            CNPJ = usuario.CNPJ,
            NomeFantasia = usuario.NomeFantasia,
            ResponsavelCompra = usuario.ResponsavelCompra,
            InscricaoEstadual = usuario.InscricaoEstadual,
            RazaoSocial = usuario.RazaoSocial,

        };
    }

}
