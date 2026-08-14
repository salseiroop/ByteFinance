using System.Net;
using System.Net.Http.Json;
using ByteFinance.API; // <--- Adicione este using
using ByteFinance.Application.DTOs;
using ByteFinance.Domain.Enums;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace ByteFinance.IntegrationTests;

public class TransacoesControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public TransacoesControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CriarTransacao_SemCategoriaValida_DeveRetornarBadRequest()
    {
        var request = new TransacaoRequestDTO
        {
            Descricao = "Teste Integração",
            Valor = 150.00m,
            Data = DateTime.Now,
            Tipo = TipoTransacao.Despesa,
            CategoriaId = Guid.NewGuid()
        };

        var response = await _client.PostAsJsonAsync("/api/v1/transacoes", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}