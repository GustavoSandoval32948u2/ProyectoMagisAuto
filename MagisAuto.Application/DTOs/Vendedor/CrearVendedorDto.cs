namespace MagisAuto.Application.DTOs.Vendedor;

public class CrearVendedorDto
{
    public string CodigoEmpleado { get; set; } = string.Empty;
    public string DPI { get; set; } = string.Empty;
    public string NombreCompleto { get; set; } = string.Empty;
    public int AgenciaId { get; set; }
}