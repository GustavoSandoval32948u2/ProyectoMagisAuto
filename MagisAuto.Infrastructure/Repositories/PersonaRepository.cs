using System.Data;
using MagisAuto.Application.DTOs.Persona;
using MagisAuto.Application.Interfaces.Persona;
using MagisAuto.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace MagisAuto.Infrastructure.Repositories;

public class PersonaRepository : IPersonaRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public PersonaRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<PersonaDto>> ObtenerTodosAsync()
    {
        var personas = new List<PersonaDto>();

        await using var connection = _connectionFactory.CreateConnection();
        await using var command = new SqlCommand(
            "sp_Persona_ObtenerTodos",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        await connection.OpenAsync();

        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            personas.Add(MapearPersona(reader));
        }

        return personas;
    }

    public async Task<IEnumerable<PersonaDto>> BuscarAsync(string? criterio)
    {
        var personas = new List<PersonaDto>();

        await using var connection = _connectionFactory.CreateConnection();
        await using var command = new SqlCommand(
            "sp_Persona_Buscar",
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
            personas.Add(MapearPersona(reader));
        }

        return personas;
    }

    public async Task<int> CrearAsync(CrearPersonaDto dto)
    {
        await using var connection = _connectionFactory.CreateConnection();
        await using var command = new SqlCommand(
            "sp_Persona_Insertar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        AgregarParametrosCrear(command, dto);

        await connection.OpenAsync();

        var resultado = await command.ExecuteScalarAsync();

        return Convert.ToInt32(resultado);
    }

    public async Task<bool> ActualizarAsync(
        int personaId,
        ActualizarPersonaDto dto)
    {
        await using var connection = _connectionFactory.CreateConnection();
        await using var command = new SqlCommand(
            "sp_Persona_Actualizar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add(
            "@PersonaId",
            SqlDbType.Int).Value = personaId;

        AgregarParametrosActualizar(command, dto);

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }

    public async Task<bool> EliminarAsync(int personaId)
    {
        await using var connection = _connectionFactory.CreateConnection();
        await using var command = new SqlCommand(
            "sp_Persona_Eliminar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add(
            "@PersonaId",
            SqlDbType.Int).Value = personaId;

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }

    private static void AgregarParametrosCrear(
        SqlCommand command,
        CrearPersonaDto dto)
    {
        command.Parameters.Add(
            "@NIT",
            SqlDbType.VarChar,
            20).Value = dto.NIT;

        command.Parameters.Add(
            "@DPI",
            SqlDbType.VarChar,
            13).Value = dto.DPI;

        command.Parameters.Add(
            "@NombreCompleto",
            SqlDbType.VarChar,
            150).Value = dto.NombreCompleto;

        command.Parameters.Add(
            "@CorreoElectronico",
            SqlDbType.VarChar,
            150).Value = dto.CorreoElectronico;

        command.Parameters.Add(
            "@FechaNacimiento",
            SqlDbType.Date).Value =
            dto.FechaNacimiento.HasValue
                ? dto.FechaNacimiento.Value.ToDateTime(TimeOnly.MinValue)
                : DBNull.Value;
    }

    private static void AgregarParametrosActualizar(
        SqlCommand command,
        ActualizarPersonaDto dto)
    {
        command.Parameters.Add(
            "@NIT",
            SqlDbType.VarChar,
            20).Value = dto.NIT;

        command.Parameters.Add(
            "@DPI",
            SqlDbType.VarChar,
            13).Value = dto.DPI;

        command.Parameters.Add(
            "@NombreCompleto",
            SqlDbType.VarChar,
            150).Value = dto.NombreCompleto;

        command.Parameters.Add(
            "@CorreoElectronico",
            SqlDbType.VarChar,
            150).Value = dto.CorreoElectronico;

        command.Parameters.Add(
            "@FechaNacimiento",
            SqlDbType.Date).Value =
            dto.FechaNacimiento.HasValue
                ? dto.FechaNacimiento.Value.ToDateTime(TimeOnly.MinValue)
                : DBNull.Value;
    }

    private static PersonaDto MapearPersona(SqlDataReader reader)
    {
        return new PersonaDto
        {
            PersonaId = reader.GetInt32(
                reader.GetOrdinal("PersonaId")),

            NIT = reader.GetString(
                reader.GetOrdinal("NIT")),

            DPI = reader.GetString(
                reader.GetOrdinal("DPI")),

            NombreCompleto = reader.GetString(
                reader.GetOrdinal("NombreCompleto")),

            CorreoElectronico = reader.GetString(
                reader.GetOrdinal("CorreoElectronico")),

            FechaNacimiento =
                reader.IsDBNull(
                    reader.GetOrdinal("FechaNacimiento"))
                    ? null
                    : DateOnly.FromDateTime(
                        reader.GetDateTime(
                            reader.GetOrdinal("FechaNacimiento")))
        };
    }
}