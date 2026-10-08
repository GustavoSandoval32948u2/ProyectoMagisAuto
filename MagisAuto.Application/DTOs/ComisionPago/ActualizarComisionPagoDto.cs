namespace MagisAuto.Application.DTOs.ComisionPago;

public class ActualizarComisionPagoDto
{
    public int PagoId { get; set; }
    public decimal MontoComision { get; set; }
    public DateTime FechaGeneracion { get; set; }
}