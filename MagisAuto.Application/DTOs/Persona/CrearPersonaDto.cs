namespace MagisAuto.Application.DTOs.Persona;

public class CrearPersonaDto
{
    public string NIT { get; set; } = string.Empty;
    public string DPI { get; set; } = string.Empty;
    public string NombreCompleto { get; set; } = string.Empty;
    public string CorreoElectronico { get; set; } = string.Empty;
    public DateOnly? FechaNacimiento { get; set; }
}