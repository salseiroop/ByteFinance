using ByteFinance.Application.DTOs;

namespace ByteFinance.Application.Interfaces;

public interface ICategoriaService
{
    Task<IEnumerable<CategoriaResponseDTO>> ObterTodasAsync();
    Task<CategoriaResponseDTO> CriarAsync(CategoriaRequestDTO dto);
}