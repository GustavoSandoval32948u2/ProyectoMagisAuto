using System.Data;
using MagisAuto.Application.DTOs.EstadoCredito;
using MagisAuto.Application.Interfaces.EstadoCredito;
using MagisAuto.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace MagisAuto.Infrastructure.Repositories;

public class EstadoCreditoRepository : IEstadoCreditoRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public EstadoCreditoRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<EstadoCreditoDto>> ObtenerTodosAsync()
    {
        var estados = new List<EstadoCreditoDto>();

        await using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync();

        await using var command = new SqlCommand(
            "sp_EstadoCredito_ObtenerTodos",
            connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            estados.Add(new EstadoCreditoDto
            {
                EstadoCreditoId = reader.GetInt32(
                    reader.GetOrdinal("EstadoCreditoId")),

                Nombre = reader.GetString(
                    reader.GetOrdinal("Nombre"))
            });
        }

        return estados;
    }

    public async Task<IEnumerable<EstadoCreditoDto>> BuscarAsync(
        string? criterio)
    {
        var estados = new List<EstadoCreditoDto>();

        await using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync();

        await using var command = new SqlCommand(
            "sp_EstadoCredito_Buscar",
            connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@Texto", SqlDbType.VarChar, 250).Value =
            string.IsNullOrWhiteSpace(criterio)
                ? DBNull.Value
                : criterio;

        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            estados.Add(new EstadoCreditoDto
            {
                EstadoCreditoId = reader.GetInt32(
                    reader.GetOrdinal("EstadoCreditoId")),

                Nombre = reader.GetString(
                    reader.GetOrdinal("Nombre"))
            });
        }

        return estados;
    }

    public async Task<int> CrearAsync(CrearEstadoCreditoDto dto)
    {
        await using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync();

        await using var command = new SqlCommand(
            "sp_EstadoCredito_Insertar",
            connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add(
            "@Nombre",
            SqlDbType.VarChar,
            50).Value = dto.Nombre;

        var resultado = await command.ExecuteScalarAsync();

        return Convert.ToInt32(resultado);
    }

    public async Task<bool> ActualizarAsync(
        int estadoCreditoId,
        ActualizarEstadoCreditoDto dto)
    {
        await using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync();

        await using var command = new SqlCommand(
            "sp_EstadoCredito_Actualizar",
            connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add(
            "@EstadoCreditoId",
            SqlDbType.Int).Value = estadoCreditoId;

        command.Parameters.Add(
            "@Nombre",
            SqlDbType.VarChar,
            50).Value = dto.Nombre;

        await command.ExecuteNonQueryAsync();

        return true;
    }

    public async Task<bool> EliminarAsync(int estadoCreditoId)
    {
        await using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync();

        await using var command = new SqlCommand(
            "sp_EstadoCredito_Eliminar",
            connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add(
            "@EstadoCreditoId",
            SqlDbType.Int).Value = estadoCreditoId;

        await command.ExecuteNonQueryAsync();

        return true;
    }
}