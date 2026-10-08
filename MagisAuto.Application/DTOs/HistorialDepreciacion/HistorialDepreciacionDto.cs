namespace MagisAuto.Application.DTOs.HistorialDepreciacion;

public class HistorialDepreciacionDto
{
    public int HistorialDepreciacionId { get; set; }
    public int VehiculoId { get; set; }
    public DateOnly FechaRegistro { get; set; }
    public decimal PorcentajeDepreciacion { get; set; }
}