using MinhaPrimeiraApi.Data;
using Microsoft.EntityFrameworkCore;
using MinhaPrimeiraApi.Repositories;
using MinhaPrimeiraApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Adicionando o Swagger
builder.Services.AddSwaggerGen();
// Adicionando o serviços para trabalhar com o DB, o DbContext
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=minhaapi.db"));

// INTERFACE, REPOSITORY, SERVICE
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();


//
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod();
    }); 
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    // Instação do Swagger e SwaggerUI
    // -> Terminal: dotnet add package Swashbuckle.AspNetCore
    // Adicionar linhas:
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("PermitirAngular");

app.MapControllers();

app.Run();
