namespace MagisAuto.Application.DTOs.ComisionPago;

public class CrearComisionPagoDto
{
    public int PagoId { get; set; }
    public decimal MontoComision { get; set; }
    public DateTime FechaGeneracion { get; set; }
}