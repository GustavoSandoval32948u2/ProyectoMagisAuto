using System.Data;
using Microsoft.Data.SqlClient;
using MagisAuto.Application.DTOs.DireccionReferencia;
using MagisAuto.Application.Interfaces.DireccionReferencia;
using MagisAuto.Infrastructure.Data;

namespace MagisAuto.Infrastructure.Repositories;

public class DireccionReferenciaRepository : IDireccionReferenciaRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public DireccionReferenciaRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<DireccionReferenciaDto>> ObtenerTodosAsync()
    {
        var lista = new List<DireccionReferenciaDto>();

        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_DireccionReferencia_ObtenerTodos",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            lista.Add(MapearDireccionReferencia(reader));
        }

        return lista;
    }

    public async Task<IEnumerable<DireccionReferenciaDto>> BuscarAsync(string? criterio)
    {
        var lista = new List<DireccionReferenciaDto>();

        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_DireccionReferencia_Buscar",
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
            lista.Add(MapearDireccionReferencia(reader));
        }

        return lista;
    }

    public async Task<int> CrearAsync(CrearDireccionReferenciaDto dto)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_DireccionReferencia_Insertar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@ReferenciaId", dto.ReferenciaId);
        command.Parameters.AddWithValue("@MunicipioId", dto.MunicipioId);
        command.Parameters.Add("@Zona", SqlDbType.VarChar, 20)
            .Value = string.IsNullOrWhiteSpace(dto.Zona)
                ? DBNull.Value
                : dto.Zona;
        command.Parameters.AddWithValue("@Direccion", dto.Direccion);

        await connection.OpenAsync();

        var resultado = await command.ExecuteScalarAsync();

        return Convert.ToInt32(resultado);
    }

    public async Task<bool> ActualizarAsync(
        int direccionReferenciaId,
        ActualizarDireccionReferenciaDto dto)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_DireccionReferencia_Actualizar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue(
            "@DireccionReferenciaId",
            direccionReferenciaId);

        command.Parameters.AddWithValue("@ReferenciaId", dto.ReferenciaId);
        command.Parameters.AddWithValue("@MunicipioId", dto.MunicipioId);

        command.Parameters.Add("@Zona", SqlDbType.VarChar, 20)
            .Value = string.IsNullOrWhiteSpace(dto.Zona)
                ? DBNull.Value
                : dto.Zona;

        command.Parameters.AddWithValue("@Direccion", dto.Direccion);

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }

    public async Task<bool> EliminarAsync(int direccionReferenciaId)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_DireccionReferencia_Eliminar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue(
            "@DireccionReferenciaId",
            direccionReferenciaId);

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }

    private static DireccionReferenciaDto MapearDireccionReferencia(
        SqlDataReader reader)
    {
        return new DireccionReferenciaDto
        {
            DireccionReferenciaId = reader.GetInt32(
                reader.GetOrdinal("DireccionReferenciaId")),

            ReferenciaId = reader.GetInt32(
                reader.GetOrdinal("ReferenciaId")),

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