namespace MagisAuto.Domain.Entities;

public class DireccionReferencia
{
    public int DireccionReferenciaId { get; set; }
    public int ReferenciaId { get; set; }
    public int MunicipioId { get; set; }
    public string? Zona { get; set; }
    public string Direccion { get; set; } = string.Empty;
}