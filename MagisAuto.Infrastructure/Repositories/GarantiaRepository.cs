using MagisAuto.Application.DTOs.Garantia;
using MagisAuto.Application.Interfaces.Garantia;
using MagisAuto.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MagisAuto.Infrastructure.Repositories;

public class GarantiaRepository : IGarantiaRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public GarantiaRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<GarantiaDto>> ObtenerTodosAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_Garantia_ObtenerTodos", connection);

        command.CommandType = CommandType.StoredProcedure;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        var resultado = new List<GarantiaDto>();

        while (await reader.ReadAsync())
        {
            resultado.Add(Mapear(reader));
        }

        return resultado;
    }

    public async Task<IEnumerable<GarantiaDto>> BuscarAsync(string? criterio)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_Garantia_Buscar", connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@Texto", SqlDbType.VarChar, 250)
            .Value = string.IsNullOrWhiteSpace(criterio)
                ? DBNull.Value
                : criterio;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        var resultado = new List<GarantiaDto>();

        while (await reader.ReadAsync())
        {
            resultado.Add(Mapear(reader));
        }

        return resultado;
    }

    public async Task<int> CrearAsync(CrearGarantiaDto dto)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_Garantia_Insertar", connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@CreditoId", dto.CreditoId);
        command.Parameters.AddWithValue("@TipoGarantiaId", dto.TipoGarantiaId);
        command.Parameters.AddWithValue("@Descripcion", dto.Descripcion);
        command.Parameters.AddWithValue("@ValorGarantia", dto.ValorGarantia);

        await connection.OpenAsync();

        var resultado = await command.ExecuteScalarAsync();

        return Convert.ToInt32(resultado);
    }

    public async Task<bool> ActualizarAsync(
        int garantiaId,
        ActualizarGarantiaDto dto)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_Garantia_Actualizar", connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@GarantiaId", garantiaId);
        command.Parameters.AddWithValue("@CreditoId", dto.CreditoId);
        command.Parameters.AddWithValue("@TipoGarantiaId", dto.TipoGarantiaId);
        command.Parameters.AddWithValue("@Descripcion", dto.Descripcion);
        command.Parameters.AddWithValue("@ValorGarantia", dto.ValorGarantia);

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }

    public async Task<bool> EliminarAsync(int garantiaId)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_Garantia_Eliminar", connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@GarantiaId", garantiaId);

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }

    private static GarantiaDto Mapear(SqlDataReader reader)
    {
        return new GarantiaDto
        {
            GarantiaId = reader.GetInt32(reader.GetOrdinal("GarantiaId")),
            CreditoId = reader.GetInt32(reader.GetOrdinal("CreditoId")),
            TipoGarantiaId = reader.GetInt32(reader.GetOrdinal("TipoGarantiaId")),
            Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
            ValorGarantia = reader.GetDecimal(reader.GetOrdinal("ValorGarantia"))
        };
    }
}