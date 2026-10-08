namespace MagisAuto.Application.DTOs.TelefonoPersona;

public class CrearTelefonoPersonaDto
{
    public int PersonaId { get; set; }
    public int TipoTelefonoId { get; set; }
    public string NumeroTelefono { get; set; } = string.Empty;
}