using ByteFinance.Application.DTOs;
using ByteFinance.Application.Interfaces;
using ByteFinance.Domain.Entities;
using ByteFinance.Domain.Interfaces;

namespace ByteFinance.Application.Services;

public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaRepository _repository;

    public CategoriaService(ICategoriaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CategoriaResponseDTO>> ObterTodasAsync()
    {
        var categorias = await _repository.ObterTodasAsync();
        return categorias.Select(c => new CategoriaResponseDTO
        {
            Id = c.Id,
            Nome = c.Nome
        });
    }

    public async Task<CategoriaResponseDTO> CriarAsync(CategoriaRequestDTO dto)
    {
        var entity = new Categoria(dto.Nome);

        await _repository.AdicionarAsync(entity);

        return new CategoriaResponseDTO
        {
            Id = entity.Id,
            Nome = entity.Nome
        };
    }
}