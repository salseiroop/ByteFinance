using ByteFinance.Domain.Entities;
using ByteFinance.Domain.Interfaces;
using ByteFinance.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ByteFinance.Infrastructure.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext _context;

    public CategoriaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Categoria?> ObterPorIdAsync(Guid id)
    {
        return await _context.Categorias.FindAsync(id);
    }

    public async Task<IEnumerable<Categoria>> ObterTodasAsync()
    {
        return await _context.Categorias.AsNoTracking().ToListAsync();
    }

    public async Task AdicionarAsync(Categoria categoria)
    {
        await _context.Categorias.AddAsync(categoria);
        await _context.SaveChangesAsync();
    }
}