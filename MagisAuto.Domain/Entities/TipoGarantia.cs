namespace MagisAuto.Domain.Entities;

public class TipoGarantia
{
    public int TipoGarantiaId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
}