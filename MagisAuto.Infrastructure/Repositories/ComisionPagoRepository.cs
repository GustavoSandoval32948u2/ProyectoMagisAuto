using MagisAuto.Application.DTOs.ComisionPago;
using MagisAuto.Application.Interfaces.ComisionPago;
using MagisAuto.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MagisAuto.Infrastructure.Repositories;

public class ComisionPagoRepository : IComisionPagoRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public ComisionPagoRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<ComisionPagoDto>> ObtenerTodosAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_ComisionPago_ObtenerTodos",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        var resultado = new List<ComisionPagoDto>();

        while (await reader.ReadAsync())
        {
            resultado.Add(Mapear(reader));
        }

        return resultado;
    }

    public async Task<IEnumerable<ComisionPagoDto>> BuscarAsync(string? criterio)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_ComisionPago_Buscar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@Texto", SqlDbType.VarChar, 250)
            .Value = string.IsNullOrWhiteSpace(criterio)
                ? DBNull.Value
                : criterio;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        var resultado = new List<ComisionPagoDto>();

        while (await reader.ReadAsync())
        {
            resultado.Add(Mapear(reader));
        }

        return resultado;
    }

    public async Task<int> CrearAsync(CrearComisionPagoDto dto)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_ComisionPago_Insertar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@PagoId", dto.PagoId);
        command.Parameters.AddWithValue("@MontoComision", dto.MontoComision);

        command.Parameters.Add("@FechaGeneracion", SqlDbType.DateTime2)
            .Value = dto.FechaGeneracion;

        await connection.OpenAsync();

        var resultado = await command.ExecuteScalarAsync();

        return Convert.ToInt32(resultado);
    }

    public async Task<bool> ActualizarAsync(
        int comisionPagoId,
        ActualizarComisionPagoDto dto)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_ComisionPago_Actualizar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue(
            "@ComisionPagoId",
            comisionPagoId);

        command.Parameters.AddWithValue(
            "@PagoId",
            dto.PagoId);

        command.Parameters.AddWithValue(
            "@MontoComision",
            dto.MontoComision);

        command.Parameters.Add("@FechaGeneracion", SqlDbType.DateTime2)
            .Value = dto.FechaGeneracion;

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }

    public async Task<bool> EliminarAsync(int comisionPagoId)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_ComisionPago_Eliminar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue(
            "@ComisionPagoId",
            comisionPagoId);

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }

    private static ComisionPagoDto Mapear(SqlDataReader reader)
    {
        return new ComisionPagoDto
        {
            ComisionPagoId = reader.GetInt32(
                reader.GetOrdinal("ComisionPagoId")),

            PagoId = reader.GetInt32(
                reader.GetOrdinal("PagoId")),

            MontoComision = reader.GetDecimal(
                reader.GetOrdinal("MontoComision")),

            FechaGeneracion = reader.GetDateTime(
                reader.GetOrdinal("FechaGeneracion"))
        };
    }
}