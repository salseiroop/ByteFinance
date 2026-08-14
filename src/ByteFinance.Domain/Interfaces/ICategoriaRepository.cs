using ByteFinance.Domain.Entities;

namespace ByteFinance.Domain.Interfaces;

public interface ICategoriaRepository
{
    Task<Categoria?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<Categoria>> ObterTodasAsync();
    Task AdicionarAsync(Categoria categoria);
}