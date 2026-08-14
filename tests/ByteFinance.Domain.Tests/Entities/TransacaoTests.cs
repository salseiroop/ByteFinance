using ByteFinance.Domain.Entities;
using ByteFinance.Domain.Enums;
using ByteFinance.Domain.Exceptions;

namespace ByteFinance.Domain.Tests.Entities;

public class TransacaoTests
{
    [Fact]
    public void CriarTransacao_ComDadosValidos_DeveInstanciarComSucesso()
    {
        var descricao = "Salário";
        var valor = 3500.00m;
        var data = DateTime.Now;
        var tipo = TipoTransacao.Receita;
        var categoriaId = Guid.NewGuid();

        var transacao = new Transacao(descricao, valor, data, tipo, categoriaId);

        Assert.NotNull(transacao);
        Assert.Equal(descricao, transacao.Descricao);
        Assert.Equal(valor, transacao.Valor);
        Assert.Equal(tipo, transacao.Tipo);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10.5)]
    public void CriarTransacao_ComValorInvalido_DeveLancarDomainException(decimal valorInvalido)
    {
        var exception = Assert.Throws<DomainException>(() =>
            new Transacao("Teste", valorInvalido, DateTime.Now, TipoTransacao.Despesa, Guid.NewGuid())
        );

        Assert.Equal("O valor da transação deve ser estritamente maior que zero.", exception.Message);
    }
}