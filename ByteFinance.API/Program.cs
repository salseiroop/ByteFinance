using ByteFinance.API.Middleware;
using ByteFinance.Application.Services;
using ByteFinance.Domain.Interfaces;
using ByteFinance.Infrastructure.Data;
using ByteFinance.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração dos Serviços (Injeção de Dependência)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=bytefinance.db"));

builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ITransacaoRepository, TransacaoRepository>();
builder.Services.AddScoped<TransacaoService>();

var app = builder.Build();

// Garantir criação automática do banco e tabelas no startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Middlewares para o ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Redireciona a raiz da URL (https://localhost:7224/) direto para o Swagger
    app.MapGet("/", () => Results.Redirect("/swagger"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

// Visibilidade da classe Program para os Testes de Integração (WebApplicationFactory)
namespace ByteFinance.API
{
    public partial class Program { }
}