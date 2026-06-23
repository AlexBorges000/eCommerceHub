using BlazorShop.Api.Context;
using BlazorShop.Api.Middlewares;
using BlazorShop.Api.Repositories;
using BlazorShop.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddCors();

    
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<ICarrinhoCompraRepository, CarrinhoCompraRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseCors(policy => policy.WithOrigins("https://localhost:7279")
                            .AllowAnyMethod()
                            .AllowAnyHeader());
app.UseAuthorization();
app.MapControllers();
app.Run();
