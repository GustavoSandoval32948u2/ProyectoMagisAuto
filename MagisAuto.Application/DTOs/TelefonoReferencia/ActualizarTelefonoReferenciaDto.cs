namespace MagisAuto.Application.DTOs.TelefonoReferencia;

public class ActualizarTelefonoReferenciaDto
{
    public int ReferenciaId { get; set; }
    public int TipoTelefonoId { get; set; }
    public string NumeroTelefono { get; set; } = string.Empty;
}