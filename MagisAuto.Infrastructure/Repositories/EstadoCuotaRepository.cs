using System.Data;
using MagisAuto.Application.DTOs.EstadoCuota;
using MagisAuto.Application.Interfaces.EstadoCuota;
using MagisAuto.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace MagisAuto.Infrastructure.Repositories;

public class EstadoCuotaRepository : IEstadoCuotaRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public EstadoCuotaRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<EstadoCuotaDto>> ObtenerTodosAsync()
    {
        var lista = new List<EstadoCuotaDto>();

        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_EstadoCuota_ObtenerTodos",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            lista.Add(new EstadoCuotaDto
            {
                EstadoCuotaId = reader.GetInt32(reader.GetOrdinal("EstadoCuotaId")),
                Nombre = reader.GetString(reader.GetOrdinal("Nombre"))
            });
        }

        return lista;
    }

    public async Task<IEnumerable<EstadoCuotaDto>> BuscarAsync(string? criterio)
    {
        var lista = new List<EstadoCuotaDto>();

        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_EstadoCuota_Buscar",
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
            lista.Add(new EstadoCuotaDto
            {
                EstadoCuotaId = reader.GetInt32(reader.GetOrdinal("EstadoCuotaId")),
                Nombre = reader.GetString(reader.GetOrdinal("Nombre"))
            });
        }

        return lista;
    }

    public async Task<int> CrearAsync(CrearEstadoCuotaDto dto)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_EstadoCuota_Insertar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@Nombre", SqlDbType.VarChar, 80)
            .Value = dto.Nombre;

        await connection.OpenAsync();

        var resultado = await command.ExecuteScalarAsync();

        return Convert.ToInt32(resultado);
    }

    public async Task<bool> ActualizarAsync(
        int estadoCuotaId,
        ActualizarEstadoCuotaDto dto)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_EstadoCuota_Actualizar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@EstadoCuotaId", SqlDbType.Int)
            .Value = estadoCuotaId;

        command.Parameters.Add("@Nombre", SqlDbType.VarChar, 80)
            .Value = dto.Nombre;

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }

    public async Task<bool> EliminarAsync(int estadoCuotaId)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_EstadoCuota_Eliminar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@EstadoCuotaId", SqlDbType.Int)
            .Value = estadoCuotaId;

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }
}