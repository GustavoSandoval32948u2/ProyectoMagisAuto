namespace MagisAuto.Domain.Entities;

public class MedioPago
{
    public int MedioPagoId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public bool RequiereReferencia { get; set; }
}