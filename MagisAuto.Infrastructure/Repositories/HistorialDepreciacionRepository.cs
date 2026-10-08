using System.Data;
using Microsoft.Data.SqlClient;
using MagisAuto.Application.DTOs.HistorialDepreciacion;
using MagisAuto.Application.Interfaces.HistorialDepreciacion;
using MagisAuto.Infrastructure.Data;

namespace MagisAuto.Infrastructure.Repositories;

public class HistorialDepreciacionRepository
    : IHistorialDepreciacionRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public HistorialDepreciacionRepository(
        SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<HistorialDepreciacionDto>>
        ObtenerTodosAsync()
    {
        var lista = new List<HistorialDepreciacionDto>();

        using var connection = _connectionFactory.CreateConnection();

        using var command = new SqlCommand(
            "sp_HistorialDepreciacion_ObtenerTodos",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            lista.Add(MapearHistorialDepreciacion(reader));
        }

        return lista;
    }

    public async Task<IEnumerable<HistorialDepreciacionDto>>
        BuscarAsync(string? criterio)
    {
        var lista = new List<HistorialDepreciacionDto>();

        using var connection = _connectionFactory.CreateConnection();

        using var command = new SqlCommand(
            "sp_HistorialDepreciacion_Buscar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@Texto", SqlDbType.VarChar, 250)
            .Value = string.IsNullOrWhiteSpace(criterio)
                ? DBNull.Value
                : criterio;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            lista.Add(MapearHistorialDepreciacion(reader));
        }

        return lista;
    }

    public async Task<int> CrearAsync(
        CrearHistorialDepreciacionDto dto)
    {
        using var connection = _connectionFactory.CreateConnection();

        using var command = new SqlCommand(
            "sp_HistorialDepreciacion_Insertar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue(
            "@VehiculoId",
            dto.VehiculoId);

        command.Parameters.Add(
            "@FechaRegistro",
            SqlDbType.Date).Value =
            dto.FechaRegistro.ToDateTime(TimeOnly.MinValue);

        command.Parameters.AddWithValue(
            "@PorcentajeDepreciacion",
            dto.PorcentajeDepreciacion);

        await connection.OpenAsync();

        var resultado = await command.ExecuteScalarAsync();

        return Convert.ToInt32(resultado);
    }

    public async Task<bool> ActualizarAsync(
        int historialDepreciacionId,
        ActualizarHistorialDepreciacionDto dto)
    {
        using var connection = _connectionFactory.CreateConnection();

        using var command = new SqlCommand(
            "sp_HistorialDepreciacion_Actualizar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue(
            "@HistorialDepreciacionId",
            historialDepreciacionId);

        command.Parameters.AddWithValue(
            "@VehiculoId",
            dto.VehiculoId);

        command.Parameters.Add(
            "@FechaRegistro",
            SqlDbType.Date).Value =
            dto.FechaRegistro.ToDateTime(TimeOnly.MinValue);

        command.Parameters.AddWithValue(
            "@PorcentajeDepreciacion",
            dto.PorcentajeDepreciacion);

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }

    public async Task<bool> EliminarAsync(
        int historialDepreciacionId)
    {
        using var connection = _connectionFactory.CreateConnection();

        using var command = new SqlCommand(
            "sp_HistorialDepreciacion_Eliminar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue(
            "@HistorialDepreciacionId",
            historialDepreciacionId);

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }

    private static HistorialDepreciacionDto
        MapearHistorialDepreciacion(SqlDataReader reader)
    {
        return new HistorialDepreciacionDto
        {
            HistorialDepreciacionId = reader.GetInt32(
                reader.GetOrdinal("HistorialDepreciacionId")),

            VehiculoId = reader.GetInt32(
                reader.GetOrdinal("VehiculoId")),

            FechaRegistro = DateOnly.FromDateTime(
                reader.GetDateTime(
                    reader.GetOrdinal("FechaRegistro"))),

            PorcentajeDepreciacion = reader.GetDecimal(
                reader.GetOrdinal("PorcentajeDepreciacion"))
        };
    }
}