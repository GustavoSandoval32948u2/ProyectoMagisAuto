using System.Data;
using MagisAuto.Application.DTOs.Referencia;
using MagisAuto.Application.Interfaces.Referencia;
using MagisAuto.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace MagisAuto.Infrastructure.Repositories;

public class ReferenciaRepository : IReferenciaRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public ReferenciaRepository(
        SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<ReferenciaDto>> ObtenerTodosAsync()
    {
        var referencias = new List<ReferenciaDto>();

        await using var connection = _connectionFactory.CreateConnection();
        await using var command = new SqlCommand(
            "sp_Referencia_ObtenerTodos",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        await connection.OpenAsync();

        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            referencias.Add(MapearReferencia(reader));
        }

        return referencias;
    }

    public async Task<IEnumerable<ReferenciaDto>> BuscarAsync(
        string? criterio)
    {
        var referencias = new List<ReferenciaDto>();

        await using var connection = _connectionFactory.CreateConnection();
        await using var command = new SqlCommand(
            "sp_Referencia_Buscar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add(
            "@Texto",
            SqlDbType.VarChar,
            250).Value =
            string.IsNullOrWhiteSpace(criterio)
                ? DBNull.Value
                : criterio;

        await connection.OpenAsync();

        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            referencias.Add(MapearReferencia(reader));
        }

        return referencias;
    }

    public async Task<int> CrearAsync(CrearReferenciaDto dto)
    {
        await using var connection = _connectionFactory.CreateConnection();
        await using var command = new SqlCommand(
            "sp_Referencia_Insertar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add(
            "@ClienteId",
            SqlDbType.Int).Value = dto.ClienteId;

        command.Parameters.Add(
            "@ParentescoId",
            SqlDbType.Int).Value = dto.ParentescoId;

        command.Parameters.Add(
            "@LugarTrabajo",
            SqlDbType.VarChar,
            150).Value = dto.LugarTrabajo;

        command.Parameters.Add(
            "@NombreCompleto",
            SqlDbType.VarChar,
            150).Value = dto.NombreCompleto;

        await connection.OpenAsync();

        var resultado = await command.ExecuteScalarAsync();

        return Convert.ToInt32(resultado);
    }

    public async Task<bool> ActualizarAsync(
        int referenciaId,
        ActualizarReferenciaDto dto)
    {
        await using var connection = _connectionFactory.CreateConnection();
        await using var command = new SqlCommand(
            "sp_Referencia_Actualizar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add(
            "@ReferenciaId",
            SqlDbType.Int).Value = referenciaId;

        command.Parameters.Add(
            "@ClienteId",
            SqlDbType.Int).Value = dto.ClienteId;

        command.Parameters.Add(
            "@ParentescoId",
            SqlDbType.Int).Value = dto.ParentescoId;

        command.Parameters.Add(
            "@LugarTrabajo",
            SqlDbType.VarChar,
            150).Value = dto.LugarTrabajo;

        command.Parameters.Add(
            "@NombreCompleto",
            SqlDbType.VarChar,
            150).Value = dto.NombreCompleto;

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }

    public async Task<bool> EliminarAsync(int referenciaId)
    {
        await using var connection = _connectionFactory.CreateConnection();
        await using var command = new SqlCommand(
            "sp_Referencia_Eliminar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add(
            "@ReferenciaId",
            SqlDbType.Int).Value = referenciaId;

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }

    private static ReferenciaDto MapearReferencia(
        SqlDataReader reader)
    {
        return new ReferenciaDto
        {
            ReferenciaId = reader.GetInt32(
                reader.GetOrdinal("ReferenciaId")),

            ClienteId = reader.GetInt32(
                reader.GetOrdinal("ClienteId")),

            ParentescoId = reader.GetInt32(
                reader.GetOrdinal("ParentescoId")),

            LugarTrabajo = reader.GetString(
                reader.GetOrdinal("LugarTrabajo")),

            NombreCompleto = reader.GetString(
                reader.GetOrdinal("NombreCompleto"))
        };
    }
}