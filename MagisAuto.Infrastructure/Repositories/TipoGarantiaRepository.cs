using System.Data;
using MagisAuto.Application.DTOs.TipoGarantia;
using MagisAuto.Application.Interfaces.TipoGarantia;
using MagisAuto.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace MagisAuto.Infrastructure.Repositories;

public class TipoGarantiaRepository : ITipoGarantiaRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public TipoGarantiaRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<TipoGarantiaDto>> ObtenerTodosAsync()
    {
        var lista = new List<TipoGarantiaDto>();

        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_TipoGarantia_ObtenerTodos",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            lista.Add(new TipoGarantiaDto
            {
                TipoGarantiaId = reader.GetInt32(
                    reader.GetOrdinal("TipoGarantiaId")),

                Nombre = reader.GetString(
                    reader.GetOrdinal("Nombre")),

                Descripcion = reader.GetString(
                    reader.GetOrdinal("Descripcion"))
            });
        }

        return lista;
    }

    public async Task<IEnumerable<TipoGarantiaDto>> BuscarAsync(string? criterio)
    {
        var lista = new List<TipoGarantiaDto>();

        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_TipoGarantia_Buscar",
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
            lista.Add(new TipoGarantiaDto
            {
                TipoGarantiaId = reader.GetInt32(
                    reader.GetOrdinal("TipoGarantiaId")),

                Nombre = reader.GetString(
                    reader.GetOrdinal("Nombre")),

                Descripcion = reader.GetString(
                    reader.GetOrdinal("Descripcion"))
            });
        }

        return lista;
    }

    public async Task<int> CrearAsync(CrearTipoGarantiaDto dto)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_TipoGarantia_Insertar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@Nombre", SqlDbType.VarChar, 80)
            .Value = dto.Nombre;

        command.Parameters.Add("@Descripcion", SqlDbType.VarChar, 250)
            .Value = dto.Descripcion;

        await connection.OpenAsync();

        var resultado = await command.ExecuteScalarAsync();

        return Convert.ToInt32(resultado);
    }

    public async Task<bool> ActualizarAsync(
        int tipoGarantiaId,
        ActualizarTipoGarantiaDto dto)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_TipoGarantia_Actualizar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@TipoGarantiaId", SqlDbType.Int)
            .Value = tipoGarantiaId;

        command.Parameters.Add("@Nombre", SqlDbType.VarChar, 80)
            .Value = dto.Nombre;

        command.Parameters.Add("@Descripcion", SqlDbType.VarChar, 250)
            .Value = dto.Descripcion;

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }

    public async Task<bool> EliminarAsync(int tipoGarantiaId)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_TipoGarantia_Eliminar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@TipoGarantiaId", SqlDbType.Int)
            .Value = tipoGarantiaId;

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }
}