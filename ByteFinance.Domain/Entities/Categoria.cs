using ByteFinance.Domain.Exceptions;

namespace ByteFinance.Domain.Entities;

public class Categoria
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;

    // Construtor privado exigido pelo EF Core para materializar objetos do banco
    private Categoria() { }

    public Categoria(string nome)
    {
        Id = Guid.NewGuid();
        SetNome(nome);
    }

    public void SetNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new DomainException("O nome da categoria é obrigatório.");

        if (nome.Length > 50)
            throw new DomainException("O nome da categoria não pode ter mais de 50 caracteres.");

        Nome = nome.Trim();
    }
}