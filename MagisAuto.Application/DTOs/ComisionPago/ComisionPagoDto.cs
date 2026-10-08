namespace MagisAuto.Application.DTOs.ComisionPago;

public class ComisionPagoDto
{
    public int ComisionPagoId { get; set; }
    public int PagoId { get; set; }
    public decimal MontoComision { get; set; }
    public DateTime FechaGeneracion { get; set; }
}