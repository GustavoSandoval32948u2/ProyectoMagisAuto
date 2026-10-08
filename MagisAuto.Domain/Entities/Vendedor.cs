namespace MagisAuto.Domain.Entities;

public class Vendedor
{
    public int VendedorId { get; set; }
    public string CodigoEmpleado { get; set; } = string.Empty;
    public string DPI { get; set; } = string.Empty;
    public string NombreCompleto { get; set; } = string.Empty;
    public int AgenciaId { get; set; }
}