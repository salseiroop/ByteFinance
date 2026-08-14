using ByteFinance.Domain.Enums;

namespace ByteFinance.Application.DTOs;

public class TransacaoRequestDTO
{
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public DateTime Data { get; set; }
    public TipoTransacao Tipo { get; set; }
    public Guid CategoriaId { get; set; }
}