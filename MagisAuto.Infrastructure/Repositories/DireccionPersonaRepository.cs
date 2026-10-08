using System.Data;
using MagisAuto.Application.DTOs.DireccionPersona;
using MagisAuto.Application.Interfaces.DireccionPersona;
using MagisAuto.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace MagisAuto.Infrastructure.Repositories;

public class DireccionPersonaRepository : IDireccionPersonaRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public DireccionPersonaRepository(
        SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<DireccionPersonaDto>> ObtenerTodosAsync()
    {
        var direcciones = new List<DireccionPersonaDto>();

        await using var connection = _connectionFactory.CreateConnection();
        await using var command = new SqlCommand(
            "sp_DireccionPersona_ObtenerTodos",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        await connection.OpenAsync();

        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            direcciones.Add(MapearDireccionPersona(reader));
        }

        return direcciones;
    }

    public async Task<IEnumerable<DireccionPersonaDto>> BuscarAsync(
        string? criterio)
    {
        var direcciones = new List<DireccionPersonaDto>();

        await using var connection = _connectionFactory.CreateConnection();
        await using var command = new SqlCommand(
            "sp_DireccionPersona_Buscar",
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
            direcciones.Add(MapearDireccionPersona(reader));
        }

        return direcciones;
    }

    public async Task<int> CrearAsync(CrearDireccionPersonaDto dto)
    {
        await using var connection = _connectionFactory.CreateConnection();
        await using var command = new SqlCommand(
            "sp_DireccionPersona_Insertar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add(
            "@PersonaId",
            SqlDbType.Int).Value = dto.PersonaId;

        command.Parameters.Add(
            "@MunicipioId",
            SqlDbType.Int).Value = dto.MunicipioId;

        command.Parameters.Add(
            "@Zona",
            SqlDbType.VarChar,
            20).Value =
            string.IsNullOrWhiteSpace(dto.Zona)
                ? DBNull.Value
                : dto.Zona;

        command.Parameters.Add(
            "@Direccion",
            SqlDbType.VarChar,
            250).Value = dto.Direccion;

        await connection.OpenAsync();

        var resultado = await command.ExecuteScalarAsync();

        return Convert.ToInt32(resultado);
    }

    public async Task<bool> ActualizarAsync(
        int direccionPersonaId,
        ActualizarDireccionPersonaDto dto)
    {
        await using var connection = _connectionFactory.CreateConnection();
        await using var command = new SqlCommand(
            "sp_DireccionPersona_Actualizar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add(
            "@DireccionPersonaId",
            SqlDbType.Int).Value = direccionPersonaId;

        command.Parameters.Add(
            "@PersonaId",
            SqlDbType.Int).Value = dto.PersonaId;

        command.Parameters.Add(
            "@MunicipioId",
            SqlDbType.Int).Value = dto.MunicipioId;

        command.Parameters.Add(
            "@Zona",
            SqlDbType.VarChar,
            20).Value =
            string.IsNullOrWhiteSpace(dto.Zona)
                ? DBNull.Value
                : dto.Zona;

        command.Parameters.Add(
            "@Direccion",
            SqlDbType.VarChar,
            250).Value = dto.Direccion;

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }

    public async Task<bool> EliminarAsync(int direccionPersonaId)
    {
        await using var connection = _connectionFactory.CreateConnection();
        await using var command = new SqlCommand(
            "sp_DireccionPersona_Eliminar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add(
            "@DireccionPersonaId",
            SqlDbType.Int).Value = direccionPersonaId;

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }

    private static DireccionPersonaDto MapearDireccionPersona(
        SqlDataReader reader)
    {
        return new DireccionPersonaDto
        {
            DireccionPersonaId = reader.GetInt32(
                reader.GetOrdinal("DireccionPersonaId")),

            PersonaId = reader.GetInt32(
                reader.GetOrdinal("PersonaId")),

            MunicipioId = reader.GetInt32(
                reader.GetOrdinal("MunicipioId")),

            Zona = reader.IsDBNull(
                reader.GetOrdinal("Zona"))
                    ? null
                    : reader.GetString(
                        reader.GetOrdinal("Zona")),

            Direccion = reader.GetString(
                reader.GetOrdinal("Direccion"))
        };
    }
}