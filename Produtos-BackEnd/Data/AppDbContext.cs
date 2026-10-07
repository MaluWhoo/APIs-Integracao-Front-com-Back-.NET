using Microsoft.EntityFrameworkCore;
using MinhaPrimeiraApi.Models;

namespace MinhaPrimeiraApi.Data;

public class AppDbContext : DbContext
{
    // Representação da conexão com o Banco de Dados
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<Produto> Produtos {get; set;}
}