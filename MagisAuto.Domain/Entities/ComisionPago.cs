namespace MagisAuto.Domain.Entities;

public class ComisionPago
{
    public int ComisionPagoId { get; set; }
    public int PagoId { get; set; }
    public decimal MontoComision { get; set; }
    public DateTime FechaGeneracion { get; set; }
}