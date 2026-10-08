namespace MagisAuto.Application.DTOs.Pago;

public class ActualizarPagoDto
{
    public int CuotaId { get; set; }
    public string NumeroComprobante { get; set; } = string.Empty;
    public decimal MontoPagado { get; set; }
    public int MedioPagoId { get; set; }
    public int? BancoId { get; set; }
    public string? ReferenciaPago { get; set; }
    public DateTime? FechaHoraPago { get; set; }
}