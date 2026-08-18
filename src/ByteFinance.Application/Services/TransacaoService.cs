using ByteFinance.Application.DTOs;
using ByteFinance.Domain.Entities;
using ByteFinance.Domain.Enums;
using ByteFinance.Domain.Exceptions;
using ByteFinance.Domain.Interfaces;

namespace ByteFinance.Application.Services;

public class TransacaoService
{
    private readonly ITransacaoRepository _transacaoRepository;
    private readonly ICategoriaRepository _categoriaRepository;

    public TransacaoService(ITransacaoRepository transacaoRepository, ICategoriaRepository categoriaRepository)
    {
        _transacaoRepository = transacaoRepository;
        _categoriaRepository = categoriaRepository;
    }

    public async Task<TransacaoResponseDTO> CriarAsync(TransacaoRequestDTO request)
    {
        var categoria = await _categoriaRepository.ObterPorIdAsync(request.CategoriaId);
        if (categoria == null)
            throw new DomainException("A categoria informada não existe.");

        var transacao = new Transacao(
            request.Descricao,
            request.Valor,
            request.Data,
            request.Tipo,
            request.CategoriaId
        );

        await _transacaoRepository.AdicionarAsync(transacao);

        return new TransacaoResponseDTO
        {
            Id = transacao.Id,
            Descricao = transacao.Descricao,
            Valor = transacao.Valor,
            Data = transacao.Data,
            Tipo = transacao.Tipo,
            CategoriaNome = categoria.Nome
        };
    }

    public async Task<(IEnumerable<TransacaoResponseDTO> Itens, int TotalRegistros)> ObterPaginadoAsync(
        int pagina, int tamanhoPagina, int? mes, int? ano, Guid? categoriaId)
    {
        var (itens, total) = await _transacaoRepository.ObterPaginadoEFiltradoAsync(pagina, tamanhoPagina, mes, ano, categoriaId);

        var dtos = itens.Select(t => new TransacaoResponseDTO
        {
            Id = t.Id,
            Descricao = t.Descricao,
            Valor = t.Valor,
            Data = t.Data,
            Tipo = t.Tipo,
            CategoriaNome = t.Categoria?.Nome ?? string.Empty
        });

        return (dtos, total);
    }

    public async Task<ResumoFinanceiroResponseDTO> ObterResumoMensalAsync(int mes, int ano)
    {
        var transacoes = await _transacaoRepository.ObterPorPeriodoAsync(mes, ano);

        decimal totalReceitas = transacoes.Where(t => t.Tipo == TipoTransacao.Receita).Sum(t => t.Valor);
        decimal totalDespesas = transacoes.Where(t => t.Tipo == TipoTransacao.Despesa).Sum(t => t.Valor);

        return new ResumoFinanceiroResponseDTO
        {
            TotalReceitas = totalReceitas,
            TotalDespesas = totalDespesas
        };
    }

    public async Task RemoverAsync(Guid id)
    {
        var transacao = await _transacaoRepository.ObterPorIdAsync(id);
        if (transacao == null)
            throw new KeyNotFoundException("Transação não encontrada.");

        await _transacaoRepository.RemoverAsync(transacao);
    }
}