using System.Data;
using MagisAuto.Application.DTOs.Banco;
using MagisAuto.Application.Interfaces.Banco;
using MagisAuto.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace MagisAuto.Infrastructure.Repositories;

public class BancoRepository : IBancoRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public BancoRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<BancoDto>> ObtenerTodosAsync()
    {
        var lista = new List<BancoDto>();

        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_Banco_ObtenerTodos",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            lista.Add(new BancoDto
            {
                BancoId = reader.GetInt32(
                    reader.GetOrdinal("BancoId")),

                Nombre = reader.GetString(
                    reader.GetOrdinal("Nombre"))
            });
        }

        return lista;
    }

    public async Task<IEnumerable<BancoDto>> BuscarAsync(string? criterio)
    {
        var lista = new List<BancoDto>();

        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_Banco_Buscar",
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
            lista.Add(new BancoDto
            {
                BancoId = reader.GetInt32(
                    reader.GetOrdinal("BancoId")),

                Nombre = reader.GetString(
                    reader.GetOrdinal("Nombre"))
            });
        }

        return lista;
    }

    public async Task<int> CrearAsync(CrearBancoDto dto)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_Banco_Insertar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@Nombre", SqlDbType.VarChar, 120)
            .Value = dto.Nombre;

        await connection.OpenAsync();

        var resultado = await command.ExecuteScalarAsync();

        return Convert.ToInt32(resultado);
    }

    public async Task<bool> ActualizarAsync(
        int bancoId,
        ActualizarBancoDto dto)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_Banco_Actualizar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@BancoId", SqlDbType.Int)
            .Value = bancoId;

        command.Parameters.Add("@Nombre", SqlDbType.VarChar, 120)
            .Value = dto.Nombre;

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }

    public async Task<bool> EliminarAsync(int bancoId)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_Banco_Eliminar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@BancoId", SqlDbType.Int)
            .Value = bancoId;

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }
}