namespace MagisAuto.Application.DTOs.TelefonoPersona;

public class TelefonoPersonaDto
{
    public int TelefonoPersonaId { get; set; }
    public int PersonaId { get; set; }
    public int TipoTelefonoId { get; set; }
    public string NumeroTelefono { get; set; } = string.Empty;
}