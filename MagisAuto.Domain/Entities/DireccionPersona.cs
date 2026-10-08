namespace MagisAuto.Domain.Entities;

public class DireccionPersona
{
    public int DireccionPersonaId { get; set; }
    public int PersonaId { get; set; }
    public int MunicipioId { get; set; }
    public string? Zona { get; set; }
    public string Direccion { get; set; } = string.Empty;
}