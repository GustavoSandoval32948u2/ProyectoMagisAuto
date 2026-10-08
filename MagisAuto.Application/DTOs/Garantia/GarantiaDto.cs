namespace MagisAuto.Application.DTOs.Garantia;

public class GarantiaDto
{
    public int GarantiaId { get; set; }
    public int CreditoId { get; set; }
    public int TipoGarantiaId { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public decimal ValorGarantia { get; set; }
}