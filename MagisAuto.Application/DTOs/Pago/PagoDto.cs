namespace MagisAuto.Application.DTOs.Pago;

public class PagoDto
{
    public int PagoId { get; set; }
    public int CuotaId { get; set; }
    public string NumeroComprobante { get; set; } = string.Empty;
    public DateTime FechaHoraPago { get; set; }
    public decimal MontoPagado { get; set; }
    public int MedioPagoId { get; set; }
    public int? BancoId { get; set; }
    public string? ReferenciaPago { get; set; }
}