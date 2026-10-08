namespace MagisAuto.Application.DTOs.Credito;

public class CreditoDto
{
    public int CreditoId { get; set; }
    public string NumeroObligacion { get; set; } = string.Empty;
    public int ClienteId { get; set; }
    public int VehiculoId { get; set; }
    public int VendedorId { get; set; }
    public int NivelRiesgoId { get; set; }
    public DateOnly FechaAprobacion { get; set; }
    public int PlazoMeses { get; set; }
    public decimal MontoEnganche { get; set; }
    public decimal MontoNetoFinanciado { get; set; }
    public decimal TasaInteresAnual { get; set; }
    public decimal MontoTotalIntereses { get; set; }
    public decimal MontoTotalNetoRecaudable { get; set; }
    public decimal PorcentajeComision { get; set; }
    public int EstadoCreditoId { get; set; }
}