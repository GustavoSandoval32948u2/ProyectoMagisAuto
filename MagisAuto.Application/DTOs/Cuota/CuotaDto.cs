namespace MagisAuto.Application.DTOs.Cuota;
public class CuotaDto
{
    public int CuotaId { get; set; }
    public int CreditoId { get; set; }
    public int NumeroCuota { get; set; }

    public DateOnly FechaVencimiento { get; set; }

    public decimal MontoCapital { get; set; }
    public decimal MontoInteres { get; set; }
    public decimal MontoManejoCuenta { get; set; }
    public decimal MontoSeguroVehiculo { get; set; }
    public decimal MontoSeguroVida { get; set; }
    public decimal MontoTotal { get; set; }

    public int EstadoCuotaId { get; set; }
    public string EstadoCuota { get; set; } = string.Empty;

    public decimal TotalPagado { get; set; }
}
