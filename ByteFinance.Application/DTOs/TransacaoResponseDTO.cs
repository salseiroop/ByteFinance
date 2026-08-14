using ByteFinance.Domain.Enums;

namespace ByteFinance.Application.DTOs;

public class TransacaoResponseDTO
{
    public Guid Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public DateTime Data { get; set; }
    public TipoTransacao Tipo { get; set; }
    public string CategoriaNome { get; set; } = string.Empty;
}