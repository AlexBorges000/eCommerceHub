using BlazorShop.Api.Entities;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.CarrinhoDtos;
using BlazorShop.Models.DTOs.ProdutoDtos;
using BlazorShop.Models.DTOs.UsuarioDtos.Cadastro;
using BlazorShop.Models.DTOs.UsuarioDtos.Login;
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

    public static OperationResult<IEnumerable<RequestGetProdutoDto>> ConverterProdutosParaDto(this IEnumerable<Produto> produtos)
    {
        var itens = (from produto in produtos
                     select new RequestGetProdutoDto
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
        return OperationResult<IEnumerable<RequestGetProdutoDto>>.Ok(itens);
    }

    public static OperationResult<RequestGetProdutoDto> ConverterProdutoParaDto(this Produto produto)
    {
        var item = new RequestGetProdutoDto
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
        return OperationResult<RequestGetProdutoDto>.Ok(item);
    }

    public static OperationResult<IEnumerable<RequestGetCarrinhoItemDto>> ConverterCarrinhoItemParaDto(this IEnumerable<CarrinhoItem> carrinhoItens, IEnumerable<Produto> produtos)
    {
        var carrinho = (from carrinhoItem in carrinhoItens
                        join produto in produtos
                        on carrinhoItem.ProdutoId equals produto.Id
                        select new RequestGetCarrinhoItemDto
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
            return OperationResult<IEnumerable<RequestGetCarrinhoItemDto>>.Ok(carrinho);
        }
        else
        {
            return OperationResult<IEnumerable<RequestGetCarrinhoItemDto>>.Fail("ERRO INTERNO AO MOSTRAR OS CARRINHOS");
        }
    }

    public static OperationResult<RequestGetCarrinhoItemDto> ConverterCarrinhoItemParaDto(this CarrinhoItem carrinhoItem, Produto produto)
    {
        var carrinho = new RequestGetCarrinhoItemDto
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
        if (carrinho is not null)
        {
            return OperationResult<RequestGetCarrinhoItemDto>.Ok(carrinho);
        }
        else
        {
            return OperationResult<RequestGetCarrinhoItemDto>.Fail("ERRO INTERNO AO MOSTRAR OS CARRINHOS");
        }
    }

    /*
    public static OperationResult<RequestCadastroUsuarioPjDto> ConverterUsuarioParaDto(this Usuario usuario)
    {
        var user = new RequestCadastroUsuarioPjDto
        {
            Email = usuario.Email,
            Endereco = usuario.Endereco,
            Telefone = usuario.Telefone,
            Senha = usuario.Senha,
            Nome = usuario.Nome,
            CPF = usuario.EncryptCpf,
            CNPJ = usuario.CNPJ,
            NomeFantasia = usuario.NomeFantasia,
            ResponsavelCompra = usuario.ResponsavelCompra,
            InscricaoEstadual = usuario.InscricaoEstadual,
            RazaoSocial = usuario.RazaoSocial,

        };
        if (usuario is not null)
        {
            return OperationResult<RequestCadastroUsuarioPjDto>.Ok(user);
        }
        return OperationResult<RequestCadastroUsuarioPjDto>.Fail("Erro ao cadastrar usuario");
    }
    

    public static OperationResult<ResponseGetUsuarioDto> GetUsuarioParaDto(this Usuario usuario)
    {
        var user = new ResponseGetUsuarioDto
        {
            Email = usuario.Email,
            Endereco = usuario.Endereco,
            Telefone = usuario.Telefone,
            Nome = usuario.Nome,
            CPF = usuario.EncryptCpf,
            CNPJ = usuario.CNPJ,
            NomeFantasia = usuario.NomeFantasia,
            ResponsavelCompra = usuario.ResponsavelCompra,
            InscricaoEstadual = usuario.InscricaoEstadual,
            RazaoSocial = usuario.RazaoSocial,

        };
        if (usuario is not null)
        {
            return OperationResult<ResponseGetUsuarioDto>.Ok(user);
        }
        return OperationResult<ResponseGetUsuarioDto>.Fail("Erro ao cadastrar usuario");
    }
    */
}
