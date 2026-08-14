using ByteFinance.Domain.Entities;

namespace ByteFinance.Domain.Interfaces;

public interface ITransacaoRepository
{
    Task<Transacao?> ObterPorIdAsync(Guid id);
    Task<(IEnumerable<Transacao> Itens, int TotalRegistros)> ObterPaginadoEFiltradoAsync(
        int pagina, int tamanhoPagina, int? mes, int? ano, Guid? categoriaId);
    Task<IEnumerable<Transacao>> ObterPorPeriodoAsync(int mes, int ano);
    Task AdicionarAsync(Transacao transacao);
    Task RemoverAsync(Transacao transacao);
}