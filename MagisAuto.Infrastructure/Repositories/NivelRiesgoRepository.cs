using System.Data;
using MagisAuto.Application.DTOs.NivelRiesgo;
using MagisAuto.Application.Interfaces.NivelRiesgo;
using MagisAuto.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace MagisAuto.Infrastructure.Repositories;

public class NivelRiesgoRepository : INivelRiesgoRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public NivelRiesgoRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<NivelRiesgoDto>> ObtenerTodosAsync()
    {
        var lista = new List<NivelRiesgoDto>();

        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_NivelRiesgo_ObtenerTodos",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            lista.Add(new NivelRiesgoDto
            {
                NivelRiesgoId = reader.GetInt32(
                    reader.GetOrdinal("NivelRiesgoId")),

                Nombre = reader.GetString(
                    reader.GetOrdinal("Nombre")),

                RequiereFiador = reader.GetBoolean(
                    reader.GetOrdinal("RequiereFiador"))
            });
        }

        return lista;
    }

    public async Task<IEnumerable<NivelRiesgoDto>> BuscarAsync(string? criterio)
    {
        var lista = new List<NivelRiesgoDto>();

        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_NivelRiesgo_Buscar",
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
            lista.Add(new NivelRiesgoDto
            {
                NivelRiesgoId = reader.GetInt32(
                    reader.GetOrdinal("NivelRiesgoId")),

                Nombre = reader.GetString(
                    reader.GetOrdinal("Nombre")),

                RequiereFiador = reader.GetBoolean(
                    reader.GetOrdinal("RequiereFiador"))
            });
        }

        return lista;
    }

    public async Task<int> CrearAsync(CrearNivelRiesgoDto dto)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_NivelRiesgo_Insertar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@Nombre", SqlDbType.VarChar, 50)
            .Value = dto.Nombre;

        command.Parameters.Add("@RequiereFiador", SqlDbType.Bit)
            .Value = dto.RequiereFiador;

        await connection.OpenAsync();

        var resultado = await command.ExecuteScalarAsync();

        return Convert.ToInt32(resultado);
    }

    public async Task<bool> ActualizarAsync(
        int nivelRiesgoId,
        ActualizarNivelRiesgoDto dto)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_NivelRiesgo_Actualizar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@NivelRiesgoId", SqlDbType.Int)
            .Value = nivelRiesgoId;

        command.Parameters.Add("@Nombre", SqlDbType.VarChar, 50)
            .Value = dto.Nombre;

        command.Parameters.Add("@RequiereFiador", SqlDbType.Bit)
            .Value = dto.RequiereFiador;

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }

    public async Task<bool> EliminarAsync(int nivelRiesgoId)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_NivelRiesgo_Eliminar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@NivelRiesgoId", SqlDbType.Int)
            .Value = nivelRiesgoId;

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }
}