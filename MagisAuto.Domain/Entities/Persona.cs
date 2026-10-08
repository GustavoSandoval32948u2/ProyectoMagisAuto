namespace MagisAuto.Domain.Entities;

public class Persona
{
    public int PersonaId { get; set; }
    public string NIT { get; set; } = string.Empty;
    public string DPI { get; set; } = string.Empty;
    public string NombreCompleto { get; set; } = string.Empty;
    public string CorreoElectronico { get; set; } = string.Empty;
    public DateOnly? FechaNacimiento { get; set; }
}