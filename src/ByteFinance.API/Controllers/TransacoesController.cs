using ByteFinance.Application.DTOs;
using ByteFinance.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ByteFinance.API.Controllers;

[ApiController]
[Route("api/v1/transacoes")]
public class TransacoesController : ControllerBase
{
    private readonly TransacaoService _transacaoService;
    private readonly ILogger<TransacoesController> _logger;

    public TransacoesController(TransacaoService transacaoService, ILogger<TransacoesController> logger)
    {
        _transacaoService = transacaoService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] TransacaoRequestDTO request)
    {
        _logger.LogInformation("Iniciando criação de transação: {Descricao}", request.Descricao);
        var resultado = await _transacaoService.CriarAsync(request);
        return CreatedAtAction(nameof(Criar), new { id = resultado.Id }, resultado);
    }

    [HttpGet]
    public async Task<IActionResult> Listar(
        [FromQuery] int pagina = 1,
        [FromQuery] int tamanhoPagina = 10,
        [FromQuery] int? mes = null,
        [FromQuery] int? ano = null,
        [FromQuery] Guid? categoriaId = null)
    {
        var (itens, total) = await _transacaoService.ObterPaginadoAsync(pagina, tamanhoPagina, mes, ano, categoriaId);
        return Ok(new { Total = total, Pagina = pagina, TamanhoPagina = tamanhoPagina, Dados = itens });
    }

    [HttpGet("resumo")]
    public async Task<IActionResult> ObterResumo([FromQuery] int mes, [FromQuery] int ano)
    {
        var resumo = await _transacaoService.ObterResumoMensalAsync(mes, ano);
        return Ok(resumo);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Remover(Guid id)
    {
        await _transacaoService.RemoverAsync(id);
        return NoContent();
    }
}