# BlazorShop

Uma aplicação de e-commerce desenvolvida com **Blazor** e **ASP.NET Core**, projetada utilizando arquitetura em camadas, separação de responsabilidades e comunicação através de APIs REST.

O projeto foi criado com foco em escalabilidade, manutenibilidade e aplicação de boas práticas do ecossistema .NET, simulando cenários encontrados em sistemas corporativos reais.

---

## Visão Geral

BlazorShop é uma plataforma de comércio eletrônico que permite a navegação por produtos, consulta de detalhes, gerenciamento de carrinho de compras e integração com uma API dedicada.

A solução foi estruturada para demonstrar conhecimentos em:

* Desenvolvimento Full Stack com .NET
* Blazor Web Applications
* ASP.NET Core Web API
* Entity Framework Core
* Arquitetura em Camadas
* Injeção de Dependência
* Boas práticas de desenvolvimento

---

## Arquitetura

```text
BlazorShop
│
├── BlazorShop.Web
│   └── Interface do usuário desenvolvida com Blazor
│
├── BlazorShop.Api
│   └── API REST responsável pelas regras de negócio e acesso aos dados
│
└── BlazorShop.Models
    └── Modelos compartilhados entre cliente e servidor
```

### Princípios adotados

* Separação de responsabilidades
* Baixo acoplamento
* Reutilização de modelos compartilhados
* Consumo de APIs REST
* Organização orientada à escalabilidade

---

## Tecnologias

### Backend

* ASP.NET Core
* Entity Framework Core
* SQL Server
* REST API

### Frontend

* Blazor
* Razor Components
* Bootstrap

### Ferramentas

* Visual Studio
* Git
* GitHub

---

## Funcionalidades

### Implementadas

* Catálogo de produtos
* Detalhamento de produtos
* Navegação por categorias
* Carrinho de compras
* Consumo de API REST
* Persistência de dados

### Em Desenvolvimento

* Sistema de autenticação
* Cadastro de usuários
* Checkout de pedidos
* Controle de estoque
* Histórico de compras
* Painel administrativo

---

## Objetivos do Projeto

Este projeto tem como objetivo consolidar conhecimentos em desenvolvimento de aplicações modernas com .NET, simulando a estrutura de um sistema de e-commerce utilizado em ambiente corporativo.

Além das funcionalidades de negócio, o foco está na aplicação de conceitos como:

* Arquitetura limpa
* Boas práticas de desenvolvimento
* Escalabilidade
* Manutenibilidade
* Experiência do usuário

---

## Roadmap

* [x] Estrutura inicial da solução
* [x] Catálogo de produtos
* [x] Integração Front-end e API
* [x] Carrinho de compras
* [ ] Autenticação e autorização
* [ ] Processamento de pedidos
* [ ] Dashboard administrativo
---

## Executando Localmente

### Clonar o repositório

```bash
git clone https://github.com/seu-usuario/BlazorShop.git
```

### Restaurar dependências

```bash
dotnet restore
```

### Executar a API

```bash
cd BlazorShop.Api
dotnet run
```

### Executar o Front-end

```bash
cd BlazorShop.Web
dotnet run
```

---

## Status do Projeto

🚧 Projeto em desenvolvimento ativo.

Novas funcionalidades estão sendo implementadas continuamente com foco em arquitetura, qualidade de código e evolução do domínio da aplicação.

---

## Autor

Desenvolvido por Alex Borges.
