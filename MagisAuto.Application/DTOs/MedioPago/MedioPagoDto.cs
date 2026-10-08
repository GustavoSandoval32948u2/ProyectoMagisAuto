namespace MagisAuto.Application.DTOs.MedioPago;

public class MedioPagoDto
{
    public int MedioPagoId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public bool RequiereReferencia { get; set; }
}