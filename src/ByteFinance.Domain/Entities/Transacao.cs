using ByteFinance.Domain.Enums;
using ByteFinance.Domain.Exceptions;

namespace ByteFinance.Domain.Entities;

public class Transacao
{
    public Guid Id { get; private set; }
    public string Descricao { get; private set; } = string.Empty;
    public decimal Valor { get; private set; }
    public DateTime Data { get; private set; }
    public TipoTransacao Tipo { get; private set; }
    public Guid CategoriaId { get; private set; }

    public Categoria? Categoria { get; private set; }

    private Transacao() { }

    public Transacao(string descricao, decimal valor, DateTime data, TipoTransacao tipo, Guid categoriaId)
    {
        Id = Guid.NewGuid();
        ValidarEDefinirCampos(descricao, valor, data, tipo, categoriaId);
    }

    private void ValidarEDefinirCampos(string descricao, decimal valor, DateTime data, TipoTransacao tipo, Guid categoriaId)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            throw new DomainException("A descrição da transação é obrigatória.");

        if (descricao.Length > 100)
            throw new DomainException("A descrição não pode ter mais de 100 caracteres.");

        if (valor <= 0)
            throw new DomainException("O valor da transação deve ser estritamente maior que zero.");

        if (categoriaId == Guid.Empty)
            throw new DomainException("Uma categoria válida deve ser associada à transação.");

        Descricao = descricao.Trim();
        Valor = valor;
        Data = data;
        Tipo = tipo;
        CategoriaId = categoriaId;
    }
}