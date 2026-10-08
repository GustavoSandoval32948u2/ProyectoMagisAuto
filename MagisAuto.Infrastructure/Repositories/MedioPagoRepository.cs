using System.Data;
using MagisAuto.Application.DTOs.MedioPago;
using MagisAuto.Application.Interfaces.MedioPago;
using MagisAuto.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace MagisAuto.Infrastructure.Repositories;

public class MedioPagoRepository : IMedioPagoRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public MedioPagoRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<MedioPagoDto>> ObtenerTodosAsync()
    {
        var lista = new List<MedioPagoDto>();

        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_MedioPago_ObtenerTodos",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            lista.Add(new MedioPagoDto
            {
                MedioPagoId = reader.GetInt32(
                    reader.GetOrdinal("MedioPagoId")),

                Nombre = reader.GetString(
                    reader.GetOrdinal("Nombre")),

                RequiereReferencia = reader.GetBoolean(
                    reader.GetOrdinal("RequiereReferencia"))
            });
        }

        return lista;
    }

    public async Task<IEnumerable<MedioPagoDto>> BuscarAsync(string? criterio)
    {
        var lista = new List<MedioPagoDto>();

        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_MedioPago_Buscar",
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
            lista.Add(new MedioPagoDto
            {
                MedioPagoId = reader.GetInt32(
                    reader.GetOrdinal("MedioPagoId")),

                Nombre = reader.GetString(
                    reader.GetOrdinal("Nombre")),

                RequiereReferencia = reader.GetBoolean(
                    reader.GetOrdinal("RequiereReferencia"))
            });
        }

        return lista;
    }

    public async Task<int> CrearAsync(CrearMedioPagoDto dto)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_MedioPago_Insertar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@Nombre", SqlDbType.VarChar, 50)
            .Value = dto.Nombre;

        command.Parameters.Add("@RequiereReferencia", SqlDbType.Bit)
            .Value = dto.RequiereReferencia;

        await connection.OpenAsync();

        var resultado = await command.ExecuteScalarAsync();

        return Convert.ToInt32(resultado);
    }

    public async Task<bool> ActualizarAsync(
        int medioPagoId,
        ActualizarMedioPagoDto dto)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_MedioPago_Actualizar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@MedioPagoId", SqlDbType.Int)
            .Value = medioPagoId;

        command.Parameters.Add("@Nombre", SqlDbType.VarChar, 50)
            .Value = dto.Nombre;

        command.Parameters.Add("@RequiereReferencia", SqlDbType.Bit)
            .Value = dto.RequiereReferencia;

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }

    public async Task<bool> EliminarAsync(int medioPagoId)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_MedioPago_Eliminar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@MedioPagoId", SqlDbType.Int)
            .Value = medioPagoId;

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }
}