using ByteFinance.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ByteFinance.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Transacao> Transacoes => Set<Transacao>();
    public DbSet<Categoria> Categorias => Set<Categoria>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Aplica automaticamente todas as configurações Fluent API criadas na pasta Mappings
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}