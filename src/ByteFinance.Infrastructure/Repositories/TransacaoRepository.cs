using ByteFinance.Domain.Entities;
using ByteFinance.Domain.Interfaces;
using ByteFinance.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ByteFinance.Infrastructure.Repositories;

public class TransacaoRepository : ITransacaoRepository
{
    private readonly AppDbContext _context;

    public TransacaoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Transacao?> ObterPorIdAsync(Guid id)
    {
        return await _context.Transacoes
            .Include(t => t.Categoria)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<(IEnumerable<Transacao> Itens, int TotalRegistros)> ObterPaginadoEFiltradoAsync(
        int pagina, int tamanhoPagina, int? mes, int? ano, Guid? categoriaId)
    {
        var query = _context.Transacoes
            .Include(t => t.Categoria)
            .AsNoTracking()
            .AsQueryable();

        if (mes.HasValue)
            query = query.Where(t => t.Data.Month == mes.Value);

        if (ano.HasValue)
            query = query.Where(t => t.Data.Year == ano.Value);

        if (categoriaId.HasValue && categoriaId != Guid.Empty)
            query = query.Where(t => t.CategoriaId == categoriaId.Value);

        var totalRegistros = await query.CountAsync();

        var itens = await query
            .OrderByDescending(t => t.Data)
            .Skip((pagina - 1) * tamanhoPagina)
            .Take(tamanhoPagina)
            .ToListAsync();

        return (itens, totalRegistros);
    }

    public async Task<IEnumerable<Transacao>> ObterPorPeriodoAsync(int mes, int ano)
    {
        return await _context.Transacoes
            .AsNoTracking()
            .Where(t => t.Data.Month == mes && t.Data.Year == ano)
            .ToListAsync();
    }

    public async Task AdicionarAsync(Transacao transacao)
    {
        await _context.Transacoes.AddAsync(transacao);
        await _context.SaveChangesAsync();
    }

    public async Task RemoverAsync(Transacao transacao)
    {
        _context.Transacoes.Remove(transacao);
        await _context.SaveChangesAsync();
    }
}