using MagisAuto.Application.DTOs.Cuota;
using MagisAuto.Application.Interfaces.Cuota;
using MagisAuto.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MagisAuto.Infrastructure.Repositories;

public class CuotaRepository : ICuotaRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public CuotaRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<CuotaDto>> ObtenerTodosAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_Cuota_ObtenerTodos", connection);

        command.CommandType = CommandType.StoredProcedure;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        var resultado = new List<CuotaDto>();

        while (await reader.ReadAsync())
        {
            resultado.Add(Mapear(reader));
        }

        return resultado;
    }

    public async Task<IEnumerable<CuotaDto>> BuscarAsync(string? criterio)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_Cuota_Buscar", connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@Texto", SqlDbType.VarChar, 250)
            .Value = string.IsNullOrWhiteSpace(criterio)
                ? DBNull.Value
                : criterio;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        var resultado = new List<CuotaDto>();

        while (await reader.ReadAsync())
        {
            resultado.Add(Mapear(reader));
        }

        return resultado;
    }

    public async Task<int> CrearAsync(CrearCuotaDto dto)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_Cuota_Insertar", connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@CreditoId", dto.CreditoId);
        command.Parameters.AddWithValue("@NumeroCuota", dto.NumeroCuota);

        command.Parameters.Add("@FechaVencimiento", SqlDbType.Date)
            .Value = dto.FechaVencimiento.ToDateTime(TimeOnly.MinValue);

        command.Parameters.AddWithValue("@MontoCapital", dto.MontoCapital);
        command.Parameters.AddWithValue("@MontoInteres", dto.MontoInteres);
        command.Parameters.AddWithValue("@MontoManejoCuenta", dto.MontoManejoCuenta);
        command.Parameters.AddWithValue("@MontoSeguroVehiculo", dto.MontoSeguroVehiculo);
        command.Parameters.AddWithValue("@MontoSeguroVida", dto.MontoSeguroVida);
        command.Parameters.AddWithValue("@EstadoCuotaId", dto.EstadoCuotaId);

        await connection.OpenAsync();

        var resultado = await command.ExecuteScalarAsync();

        return Convert.ToInt32(resultado);
    }

    public async Task<bool> ActualizarAsync(
        int cuotaId,
        ActualizarCuotaDto dto)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_Cuota_Actualizar", connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@CuotaId", cuotaId);

        command.Parameters.Add("@FechaVencimiento", SqlDbType.Date)
            .Value = dto.FechaVencimiento.ToDateTime(TimeOnly.MinValue);

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }

    public async Task<bool> EliminarAsync(int cuotaId)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_Cuota_Eliminar", connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@CuotaId", cuotaId);

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }

    private static CuotaDto Mapear(SqlDataReader reader)
    {
        return new CuotaDto
        {
            CuotaId = reader.GetInt32(reader.GetOrdinal("CuotaId")),
            CreditoId = reader.GetInt32(reader.GetOrdinal("CreditoId")),
            NumeroCuota = reader.GetInt32(reader.GetOrdinal("NumeroCuota")),
            FechaVencimiento = DateOnly.FromDateTime(
                reader.GetDateTime(reader.GetOrdinal("FechaVencimiento"))),
            MontoCapital = reader.GetDecimal(reader.GetOrdinal("MontoCapital")),
            MontoInteres = reader.GetDecimal(reader.GetOrdinal("MontoInteres")),
            MontoManejoCuenta = reader.GetDecimal(reader.GetOrdinal("MontoManejoCuenta")),
            MontoSeguroVehiculo = reader.GetDecimal(reader.GetOrdinal("MontoSeguroVehiculo")),
            MontoSeguroVida = reader.GetDecimal(reader.GetOrdinal("MontoSeguroVida")),
            MontoTotal = reader.GetDecimal(reader.GetOrdinal("MontoTotal")),
            EstadoCuotaId = reader.GetInt32(reader.GetOrdinal("EstadoCuotaId"))
        };
    }
}