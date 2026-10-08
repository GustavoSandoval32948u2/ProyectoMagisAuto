namespace MagisAuto.Application.DTOs.HistorialDepreciacion;

public class ActualizarHistorialDepreciacionDto
{
    public int VehiculoId { get; set; }
    public DateOnly FechaRegistro { get; set; }
    public decimal PorcentajeDepreciacion { get; set; }
}