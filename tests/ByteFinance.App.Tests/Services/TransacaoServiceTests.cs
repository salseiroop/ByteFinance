using ByteFinance.Application.DTOs;
using ByteFinance.Application.Services;
using ByteFinance.Domain.Entities;
using ByteFinance.Domain.Enums;
using ByteFinance.Domain.Exceptions;
using ByteFinance.Domain.Interfaces;
using Moq;
using Xunit;

namespace ByteFinance.App.Tests.Services;

public class TransacaoServiceTests
{
    private readonly Mock<ITransacaoRepository> _transacaoRepoMock;
    private readonly Mock<ICategoriaRepository> _categoriaRepoMock;
    private readonly TransacaoService _service;

    public TransacaoServiceTests()
    {
        _transacaoRepoMock = new Mock<ITransacaoRepository>();
        _categoriaRepoMock = new Mock<ICategoriaRepository>();
        _service = new TransacaoService(_transacaoRepoMock.Object, _categoriaRepoMock.Object);
    }

    [Fact]
    async Task CriarAsync_ComCategoriaInexistente_DeveLancarDomainException()
    {
        var request = new TransacaoRequestDTO
        {
            Descricao = "Teste",
            Valor = 100,
            Data = DateTime.Now,
            Tipo = TipoTransacao.Receita,
            CategoriaId = Guid.NewGuid()
        };

        _categoriaRepoMock.Setup(r => r.ObterPorIdAsync(request.CategoriaId))
            .ReturnsAsync((Categoria?)null);

        var exception = await Assert.ThrowsAsync<DomainException>(() =>
            _service.CriarAsync(request));

        Assert.Equal("A categoria informada não existe.", exception.Message);
    }
}