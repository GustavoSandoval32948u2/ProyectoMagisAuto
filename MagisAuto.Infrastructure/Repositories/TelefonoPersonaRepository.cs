using System.Data;
using MagisAuto.Application.DTOs.TelefonoPersona;
using MagisAuto.Application.Interfaces.TelefonoPersona;
using MagisAuto.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace MagisAuto.Infrastructure.Repositories;

public class TelefonoPersonaRepository : ITelefonoPersonaRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public TelefonoPersonaRepository(
        SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<TelefonoPersonaDto>> ObtenerTodosAsync()
    {
        var telefonos = new List<TelefonoPersonaDto>();

        await using var connection = _connectionFactory.CreateConnection();
        await using var command = new SqlCommand(
            "sp_TelefonoPersona_ObtenerTodos",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        await connection.OpenAsync();

        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            telefonos.Add(MapearTelefonoPersona(reader));
        }

        return telefonos;
    }

    public async Task<IEnumerable<TelefonoPersonaDto>> BuscarAsync(
        string? criterio)
    {
        var telefonos = new List<TelefonoPersonaDto>();

        await using var connection = _connectionFactory.CreateConnection();
        await using var command = new SqlCommand(
            "sp_TelefonoPersona_Buscar",
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
            telefonos.Add(MapearTelefonoPersona(reader));
        }

        return telefonos;
    }

    public async Task<int> CrearAsync(CrearTelefonoPersonaDto dto)
    {
        await using var connection = _connectionFactory.CreateConnection();
        await using var command = new SqlCommand(
            "sp_TelefonoPersona_Insertar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add(
            "@PersonaId",
            SqlDbType.Int).Value = dto.PersonaId;

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
        int telefonoPersonaId,
        ActualizarTelefonoPersonaDto dto)
    {
        await using var connection = _connectionFactory.CreateConnection();
        await using var command = new SqlCommand(
            "sp_TelefonoPersona_Actualizar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add(
            "@TelefonoPersonaId",
            SqlDbType.Int).Value = telefonoPersonaId;

        command.Parameters.Add(
            "@PersonaId",
            SqlDbType.Int).Value = dto.PersonaId;

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

    public async Task<bool> EliminarAsync(int telefonoPersonaId)
    {
        await using var connection = _connectionFactory.CreateConnection();
        await using var command = new SqlCommand(
            "sp_TelefonoPersona_Eliminar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add(
            "@TelefonoPersonaId",
            SqlDbType.Int).Value = telefonoPersonaId;

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }

    private static TelefonoPersonaDto MapearTelefonoPersona(
        SqlDataReader reader)
    {
        return new TelefonoPersonaDto
        {
            TelefonoPersonaId = reader.GetInt32(
                reader.GetOrdinal("TelefonoPersonaId")),

            PersonaId = reader.GetInt32(
                reader.GetOrdinal("PersonaId")),

            TipoTelefonoId = reader.GetInt32(
                reader.GetOrdinal("TipoTelefonoId")),

            NumeroTelefono = reader.GetString(
                reader.GetOrdinal("NumeroTelefono"))
        };
    }
}