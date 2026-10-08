namespace MagisAuto.Application.DTOs.Cliente
{
    public class ClienteDto
    {
        public int ClienteId { get; set; }
        public string Nit { get; set; } = string.Empty;
        public string Dpi { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string CorreoElectronico { get; set; } = string.Empty;
        public DateTime? FechaNacimiento { get; set; }
    }
}
