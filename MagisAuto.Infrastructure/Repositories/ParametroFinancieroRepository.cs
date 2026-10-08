using System.Data;
using MagisAuto.Application.DTOs.ParametroFinanciero;
using MagisAuto.Application.Interfaces.ParametroFinanciero;
using MagisAuto.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace MagisAuto.Infrastructure.Repositories;

public class ParametroFinancieroRepository : IParametroFinancieroRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public ParametroFinancieroRepository(
        SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<ParametroFinancieroDto>> ObtenerTodosAsync()
    {
        var lista = new List<ParametroFinancieroDto>();

        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_ParametroFinanciero_ObtenerTodos",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            lista.Add(new ParametroFinancieroDto
            {
                ParametroFinancieroId = reader.GetInt32(
                    reader.GetOrdinal("ParametroFinancieroId")),

                Nombre = reader.GetString(
                    reader.GetOrdinal("Nombre")),

                Valor = reader.GetDecimal(
                    reader.GetOrdinal("Valor"))
            });
        }

        return lista;
    }

    public async Task<IEnumerable<ParametroFinancieroDto>> BuscarAsync(
        string? criterio)
    {
        var lista = new List<ParametroFinancieroDto>();

        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_ParametroFinanciero_Buscar",
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
            lista.Add(new ParametroFinancieroDto
            {
                ParametroFinancieroId = reader.GetInt32(
                    reader.GetOrdinal("ParametroFinancieroId")),

                Nombre = reader.GetString(
                    reader.GetOrdinal("Nombre")),

                Valor = reader.GetDecimal(
                    reader.GetOrdinal("Valor"))
            });
        }

        return lista;
    }

    public async Task<int> CrearAsync(CrearParametroFinancieroDto dto)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_ParametroFinanciero_Insertar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@Nombre", SqlDbType.VarChar, 80)
            .Value = dto.Nombre;

        var parametroValor = command.Parameters.Add(
            "@Valor",
            SqlDbType.Decimal);

        parametroValor.Precision = 12;
        parametroValor.Scale = 4;
        parametroValor.Value = dto.Valor;

        await connection.OpenAsync();

        var resultado = await command.ExecuteScalarAsync();

        return Convert.ToInt32(resultado);
    }

    public async Task<bool> ActualizarAsync(
        int parametroFinancieroId,
        ActualizarParametroFinancieroDto dto)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_ParametroFinanciero_Actualizar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@ParametroFinancieroId", SqlDbType.Int)
            .Value = parametroFinancieroId;

        command.Parameters.Add("@Nombre", SqlDbType.VarChar, 80)
            .Value = dto.Nombre;

        var parametroValor = command.Parameters.Add(
            "@Valor",
            SqlDbType.Decimal);

        parametroValor.Precision = 12;
        parametroValor.Scale = 4;
        parametroValor.Value = dto.Valor;

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }

    public async Task<bool> EliminarAsync(int parametroFinancieroId)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_ParametroFinanciero_Eliminar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@ParametroFinancieroId", SqlDbType.Int)
            .Value = parametroFinancieroId;

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }
}