namespace MagisAuto.Application.DTOs.DireccionPersona;

public class ActualizarDireccionPersonaDto
{
    public int PersonaId { get; set; }
    public int MunicipioId { get; set; }
    public string? Zona { get; set; }
    public string Direccion { get; set; } = string.Empty;
}