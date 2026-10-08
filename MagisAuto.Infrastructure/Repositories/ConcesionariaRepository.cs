using System.Data;
using Microsoft.Data.SqlClient;
using MagisAuto.Application.DTOs.Concesionaria;
using MagisAuto.Application.Interfaces.Concesionaria;
using MagisAuto.Infrastructure.Data;

namespace MagisAuto.Infrastructure.Repositories;

public class ConcesionariaRepository : IConcesionariaRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public ConcesionariaRepository(
        SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<ConcesionariaDto>> ObtenerTodosAsync()
    {
        var lista = new List<ConcesionariaDto>();

        using var connection = _connectionFactory.CreateConnection();

        using var command = new SqlCommand(
            "sp_Concesionaria_ObtenerTodos",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            lista.Add(MapearConcesionaria(reader));
        }

        return lista;
    }

    public async Task<IEnumerable<ConcesionariaDto>> BuscarAsync(
        string? criterio)
    {
        var lista = new List<ConcesionariaDto>();

        using var connection = _connectionFactory.CreateConnection();

        using var command = new SqlCommand(
            "sp_Concesionaria_Buscar",
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
            lista.Add(MapearConcesionaria(reader));
        }

        return lista;
    }

    public async Task<int> CrearAsync(
        CrearConcesionariaDto dto)
    {
        using var connection = _connectionFactory.CreateConnection();

        using var command = new SqlCommand(
            "sp_Concesionaria_Insertar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue(
            "@Nombre",
            dto.Nombre);

        command.Parameters.Add("@NIT", SqlDbType.VarChar, 20)
            .Value = string.IsNullOrWhiteSpace(dto.NIT)
                ? DBNull.Value
                : dto.NIT;

        await connection.OpenAsync();

        var resultado = await command.ExecuteScalarAsync();

        return Convert.ToInt32(resultado);
    }

    public async Task<bool> ActualizarAsync(
        int concesionariaId,
        ActualizarConcesionariaDto dto)
    {
        using var connection = _connectionFactory.CreateConnection();

        using var command = new SqlCommand(
            "sp_Concesionaria_Actualizar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue(
            "@ConcesionariaId",
            concesionariaId);

        command.Parameters.AddWithValue(
            "@Nombre",
            dto.Nombre);

        command.Parameters.Add("@NIT", SqlDbType.VarChar, 20)
            .Value = string.IsNullOrWhiteSpace(dto.NIT)
                ? DBNull.Value
                : dto.NIT;

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }

    public async Task<bool> EliminarAsync(
        int concesionariaId)
    {
        using var connection = _connectionFactory.CreateConnection();

        using var command = new SqlCommand(
            "sp_Concesionaria_Eliminar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue(
            "@ConcesionariaId",
            concesionariaId);

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }

    private static ConcesionariaDto MapearConcesionaria(
        SqlDataReader reader)
    {
        return new ConcesionariaDto
        {
            ConcesionariaId = reader.GetInt32(
                reader.GetOrdinal("ConcesionariaId")),

            Nombre = reader.GetString(
                reader.GetOrdinal("Nombre")),

            NIT = reader.IsDBNull(
                reader.GetOrdinal("NIT"))
                    ? null
                    : reader.GetString(
                        reader.GetOrdinal("NIT"))
        };
    }
}