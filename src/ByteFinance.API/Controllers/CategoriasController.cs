using ByteFinance.Application.DTOs;
using ByteFinance.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ByteFinance.API.Controllers;

[ApiController]
[Route("api/v1/categorias")]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaService _categoriaService;

    public CategoriasController(ICategoriaService categoriaService)
    {
        _categoriaService = categoriaService;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var categorias = await _categoriaService.ObterTodasAsync();
        return Ok(categorias);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CategoriaRequestDTO dto)
    {
        var categoria = await _categoriaService.CriarAsync(dto);
        return CreatedAtAction(nameof(Listar), new { id = categoria.Id }, categoria);
    }
}