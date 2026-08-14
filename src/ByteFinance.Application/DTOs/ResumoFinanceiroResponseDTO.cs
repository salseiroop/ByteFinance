namespace ByteFinance.Application.DTOs;

public class ResumoFinanceiroResponseDTO
{
    public decimal TotalReceitas { get; set; }
    public decimal TotalDespesas { get; set; }
    public decimal SaldoTotal => TotalReceitas - TotalDespesas;
}