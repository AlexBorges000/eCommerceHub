using BlazorShop.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorShop.Api.Seeds;

public class ProductSeed
{
    public static readonly Product[] Products = CreateProducts();

    private static Product[] CreateProducts()
    {
        return
        [
            new Product
            {
                Id = 1,
                Name = "Glossier - Beleza Kit",
                Description = "Um kit fornecido pela Natura, contendo produtos para cuidados com a pele",
                ImageUrl = "/images/Beauty/Beauty1.png",
                Price = 100,
                Quantity = 100,
                CategoryId = 1

            },
            new Product
            {
                Id = 54,
                Name = "Teste",
                Description = "Teste",
                ImageUrl = "Teste",
                Price = 100,
                Quantity = 100,
                CategoryId = 1

            },
            new Product
            {
                Id = 2,
                Name = "Curology - Kit para Pele",
                Description = "Um kit fornecido pela Curology, contendo produtos para cuidados com a pele",
                ImageUrl = "/images/Beauty/Beauty2.png",
                Price = 50,
                Quantity = 45,
                CategoryId = 1

            },
            new Product
            {
                Id = 3,
                Name = "Óleo de Coco Orgânico",
                Description = "Um kit fornecido pela Glossier, contendo produtos para cuidados com a pele",
                ImageUrl = "/images/Beauty/Beauty3.png",
                Price = 20,
                Quantity = 30,
                CategoryId = 1

            },
            new Product
            {
                Id = 4,
                Name = "Schwarzkopf - Kit de cuidados com a pele e cabelo",
                Description = "Um kit fornecido pela Curology, contendo produtos para cuidados com a pele",
                ImageUrl = "/images/Beauty/Beauty4.png",
                Price = 50,
                Quantity = 60,
                CategoryId = 1

            },
            new Product
            {
                Id = 5,
                Name = "Kit de cuidados com a pele",
                Description = "Kit de cuidados com a pele, contendo produtos para cuidados com a pele e cabelos",
                ImageUrl = "/images/Beauty/Beauty5.png",
                Price = 30,
                Quantity = 85,
                CategoryId = 1

            },
            //eletronico Category
            new Product
            {
                Id = 6,
                Name = "Fones de ouvidos",
                Description = "Air Pods - fones de ouvido sem fio intra-auriculares",
                ImageUrl = "/images/Electronics/electronics1.png",
                Price = 100,
                Quantity = 120,
                CategoryId = 3

            },
            new Product
            {
                Id = 7,
                Name = "Fones de ouvido dourados",
                Description = "Fones de ouvido dourados na orelha - esses fones de ouvido não são sem fio",
                ImageUrl = "/images/Electronics/electronics2.png",
                Price = 40,
                Quantity = 200,
                CategoryId = 3

            },
            new Product
            {
                Id = 8,
                Name = "Fones de ouvido pretos",
                Description = "Fones de ouvido pretos na orelha - esses fones de ouvido não são sem fio",
                ImageUrl = "/images/Electronics/electronics3.png",
                Price = 40,
                Quantity = 300,
                CategoryId = 3

            },
            new Product
            {
                Id = 9,
                Name = "Câmera digital Sennheiser com tripé",
                Description = "Câmera Digital Sennheiser - Câmera digital de alta qualidade fornecida pela Sennheiser - inclui tripé",
                ImageUrl = "/images/Electronics/electronics4.png",
                Price = 600,
                Quantity = 20,
                CategoryId = 3

            },
            new Product
            {
                Id = 10,
                Name = "Câmera Digital Canon",
                Description = "Canon Digital Camera - Câmera digital de alta qualidade fornecida pela Canon",
                ImageUrl = "/images/Electronics/electronics5.png",
                Price = 500,
                Quantity = 15,
                CategoryId = 3

            },
            new Product
            {
                Id = 11,
                Name = "Nintendo Gameboy",
                Description = "Gameboy - Fornecido por Nintendo",
                ImageUrl = "/images/Electronics/technology6.png",
                Price = 100,
                Quantity = 60,
                CategoryId = 3
            },
            //Moveis Category
            new Product
            {
                Id = 12,
                Name = "Cadeira de escritório de couro preto",
                Description = "Cadeira de escritório em couro preto muito confortável",
                ImageUrl = "/images/Furniture/furniture1.png",
                Price = 50,
                Quantity = 212,
                CategoryId = 2
            },

            new Product
            {
                Id = 13,
                Name = "Cadeira de escritório de couro rosa",
                Description = "Cadeira de escritório em couro rosa muito confortável",
                ImageUrl = "/images/Furniture/furniture2.png",
                Price = 50,
                Quantity = 112,
                CategoryId = 2
            },
            new Product
            {
                Id = 14,
                Name = "Espreguiçadeira",
                Description = "Poltrona muito confortável",
                ImageUrl = "/images/Furniture/furniture3.png",
                Price = 70,
                Quantity = 90,
                CategoryId = 2
            },
            new Product
            {
                Id = 15,
                Name = "Silver Lounge Chair",
                Description = "Poltrona prateada muito confortável",
                ImageUrl = "/images/Furniture/furniture4.png",
                Price = 120,
                Quantity = 95,
                CategoryId = 2
            },
            new Product
            {
                Id = 16,
                Name = "Luminária de mesa de porcelana",
                Description = "Abajur de mesa de porcelana branco e azul",
                ImageUrl = "/images/Furniture/furniture6.png",
                Price = 15,
                Quantity = 100,
                CategoryId = 2
            },
            new Product
            {
                Id = 17,
                Name = "Office Table Lamp",
                Description = "Abajur de mesa de escritório",
                ImageUrl = "/images/Furniture/furniture7.png",
                Price = 20,
                Quantity = 73,
                CategoryId = 2
            },
            //Calcados Category
            new Product
            {
                Id = 18,
                Name = "Tênis Puma",
                Description = "Tênis Puma confortáveis na maioria dos tamanhos",
                ImageUrl = "/images/Shoes/shoe1.png",
                Price = 100,
                Quantity = 50,
                CategoryId = 4
            },
            new Product
            {
                Id = 19,
                Name = "Tênis Colodiros",
                Description = "Tênis coloridos - disponíveis na maioria dos tamanhos",
                ImageUrl = "/images/Shoes/shoe2.png",
                Price = 150,
                Quantity = 60,
                CategoryId = 4
            },
            new Product
            {
                Id = 20,
                Name = "Tênis Nike Azul",
                Description = "Tênis Nike azul - disponível na maioria dos tamanhos",
                ImageUrl = "/images/Shoes/shoe3.png",
                Price = 200,
                Quantity = 70,
                CategoryId = 4
            },
            new Product
            {
                Id = 21,
                Name = "Tênis Hummel Coloridos",
                Description = "Treinadores Hummel coloridos - disponíveis na maioria dos tamanhos",
                ImageUrl = "/images/Shoes/shoe4.png",
                Price = 120,
                Quantity = 120,
                CategoryId = 4
            },
            new Product
            {
                Id = 22,
                Name = "Tênis Nike Vermelho",
                Description = "Tênis Nike vermelho - disponível na maioria dos tamanhos",
                ImageUrl = "/images/Shoes/shoe5.png",
                Price = 200,
                Quantity = 100,
                CategoryId = 4
            },
            new Product
            {
                Id = 23,
                Name = "Sandálidas Birkenstock",
                Description = "Sandálias Birkenstock - disponíveis na maioria dos tamanhos",
                ImageUrl = "/images/Shoes/shoe6.png",
                Price = 50,
                Quantity = 150,
                CategoryId = 4
            }
        ];
    }
}
