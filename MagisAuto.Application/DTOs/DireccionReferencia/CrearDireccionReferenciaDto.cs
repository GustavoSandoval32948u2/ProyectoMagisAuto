namespace MagisAuto.Application.DTOs.DireccionReferencia;

public class CrearDireccionReferenciaDto
{
    public int ReferenciaId { get; set; }
    public int MunicipioId { get; set; }
    public string? Zona { get; set; }
    public string Direccion { get; set; } = string.Empty;
}