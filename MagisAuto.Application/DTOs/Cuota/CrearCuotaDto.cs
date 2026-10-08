namespace MagisAuto.Application.DTOs.Cuota;

public class CrearCuotaDto
{
    public int CreditoId { get; set; }
    public int NumeroCuota { get; set; }
    public DateOnly FechaVencimiento { get; set; }
    public decimal MontoCapital { get; set; }
    public decimal MontoInteres { get; set; }
    public decimal MontoManejoCuenta { get; set; }
    public decimal MontoSeguroVehiculo { get; set; }
    public decimal MontoSeguroVida { get; set; }
    public int EstadoCuotaId { get; set; }
}