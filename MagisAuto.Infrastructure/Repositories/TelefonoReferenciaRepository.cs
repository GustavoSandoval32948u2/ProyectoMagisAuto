using System.Data;
using MagisAuto.Application.DTOs.TelefonoReferencia;
using MagisAuto.Application.Interfaces.TelefonoReferencia;
using MagisAuto.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace MagisAuto.Infrastructure.Repositories;

public class TelefonoReferenciaRepository : ITelefonoReferenciaRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public TelefonoReferenciaRepository(
        SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<TelefonoReferenciaDto>> ObtenerTodosAsync()
    {
        var telefonos = new List<TelefonoReferenciaDto>();

        await using var connection = _connectionFactory.CreateConnection();
        await using var command = new SqlCommand(
            "sp_TelefonoReferencia_ObtenerTodos",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        await connection.OpenAsync();

        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            telefonos.Add(MapearTelefonoReferencia(reader));
        }

        return telefonos;
    }

    public async Task<IEnumerable<TelefonoReferenciaDto>> BuscarAsync(
        string? criterio)
    {
        var telefonos = new List<TelefonoReferenciaDto>();

        await using var connection = _connectionFactory.CreateConnection();
        await using var command = new SqlCommand(
            "sp_TelefonoReferencia_Buscar",
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
            telefonos.Add(MapearTelefonoReferencia(reader));
        }

        return telefonos;
    }

    public async Task<int> CrearAsync(CrearTelefonoReferenciaDto dto)
    {
        await using var connection = _connectionFactory.CreateConnection();
        await using var command = new SqlCommand(
            "sp_TelefonoReferencia_Insertar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add(
            "@ReferenciaId",
            SqlDbType.Int).Value = dto.ReferenciaId;

        command.Parameters.Add(
            "@TipoTelefonoId",
            SqlDbType.Int).Value = dto.TipoTelefonoId;

        command.Parameters.Add(
            "@NumeroTelefono",
            SqlDbType.VarChar,
            20).Value = dto.NumeroTelefono;

        await connection.OpenAsync();

        var resultado = await command.ExecuteScalarAsync();

        return Convert.ToInt32(resultado);
    }

    public async Task<bool> ActualizarAsync(
        int telefonoReferenciaId,
        ActualizarTelefonoReferenciaDto dto)
    {
        await using var connection = _connectionFactory.CreateConnection();
        await using var command = new SqlCommand(
            "sp_TelefonoReferencia_Actualizar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add(
            "@TelefonoReferenciaId",
            SqlDbType.Int).Value = telefonoReferenciaId;

        command.Parameters.Add(
            "@ReferenciaId",
            SqlDbType.Int).Value = dto.ReferenciaId;

        command.Parameters.Add(
            "@TipoTelefonoId",
            SqlDbType.Int).Value = dto.TipoTelefonoId;

        command.Parameters.Add(
            "@NumeroTelefono",
            SqlDbType.VarChar,
            20).Value = dto.NumeroTelefono;

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }

    public async Task<bool> EliminarAsync(int telefonoReferenciaId)
    {
        await using var connection = _connectionFactory.CreateConnection();
        await using var command = new SqlCommand(
            "sp_TelefonoReferencia_Eliminar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add(
            "@TelefonoReferenciaId",
            SqlDbType.Int).Value = telefonoReferenciaId;

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }

    private static TelefonoReferenciaDto MapearTelefonoReferencia(
        SqlDataReader reader)
    {
        return new TelefonoReferenciaDto
        {
            TelefonoReferenciaId = reader.GetInt32(
                reader.GetOrdinal("TelefonoReferenciaId")),

            ReferenciaId = reader.GetInt32(
                reader.GetOrdinal("ReferenciaId")),

            TipoTelefonoId = reader.GetInt32(
                reader.GetOrdinal("TipoTelefonoId")),

            NumeroTelefono = reader.GetString(
                reader.GetOrdinal("NumeroTelefono"))
        };
    }
}