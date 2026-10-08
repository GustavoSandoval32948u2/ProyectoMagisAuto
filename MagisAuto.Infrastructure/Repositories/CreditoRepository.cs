using System.Data;
using Microsoft.Data.SqlClient;
using MagisAuto.Application.DTOs.Credito;
using MagisAuto.Application.Interfaces.Credito;
using MagisAuto.Infrastructure.Data;

namespace MagisAuto.Infrastructure.Repositories;

public class CreditoRepository : ICreditoRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public CreditoRepository(
        SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<CreditoDto>> ObtenerTodosAsync()
    {
        var lista = new List<CreditoDto>();

        using var connection = _connectionFactory.CreateConnection();

        using var command = new SqlCommand(
            "sp_Credito_ObtenerTodos",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            lista.Add(MapearCredito(reader));
        }

        return lista;
    }

    public async Task<IEnumerable<CreditoDto>> BuscarAsync(
        string? criterio)
    {
        var lista = new List<CreditoDto>();

        using var connection = _connectionFactory.CreateConnection();

        using var command = new SqlCommand(
            "sp_Credito_Buscar",
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
            lista.Add(MapearCredito(reader));
        }

        return lista;
    }

    public async Task<int> CrearAsync(
        CrearCreditoDto dto)
    {
        using var connection = _connectionFactory.CreateConnection();

        using var command = new SqlCommand(
            "sp_Credito_Insertar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue(
            "@ClienteId",
            dto.ClienteId);

        command.Parameters.AddWithValue(
            "@VehiculoId",
            dto.VehiculoId);

        command.Parameters.AddWithValue(
            "@VendedorId",
            dto.VendedorId);

        command.Parameters.AddWithValue(
            "@NivelRiesgoId",
            dto.NivelRiesgoId);

        command.Parameters.AddWithValue(
            "@MontoEnganche",
            dto.MontoEnganche);

        command.Parameters.AddWithValue(
            "@PorcentajeComision",
            dto.PorcentajeComision);

        command.Parameters.Add("@FiadoresIds", SqlDbType.VarChar, -1)
            .Value = string.IsNullOrWhiteSpace(dto.FiadoresIds)
                ? DBNull.Value
                : dto.FiadoresIds;

        command.Parameters.Add(
            "@PlazoMeses",
            SqlDbType.Int).Value =
            dto.PlazoMeses.HasValue
                ? dto.PlazoMeses.Value
                : DBNull.Value;

        command.Parameters.Add(
            "@TasaInteresAnual",
            SqlDbType.Decimal).Value =
            dto.TasaInteresAnual.HasValue
                ? dto.TasaInteresAnual.Value
                : DBNull.Value;

        command.Parameters.Add(
            "@FechaAprobacion",
            SqlDbType.Date).Value =
            dto.FechaAprobacion.HasValue
                ? dto.FechaAprobacion.Value
                    .ToDateTime(TimeOnly.MinValue)
                : DBNull.Value;

        await connection.OpenAsync();

        var resultado = await command.ExecuteScalarAsync();

        return Convert.ToInt32(resultado);
    }

    public async Task<bool> ActualizarAsync(
        int creditoId,
        ActualizarCreditoDto dto)
    {
        using var connection = _connectionFactory.CreateConnection();

        using var command = new SqlCommand(
            "sp_Credito_Actualizar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue(
            "@CreditoId",
            creditoId);

        command.Parameters.AddWithValue(
            "@EstadoCreditoId",
            dto.EstadoCreditoId);

        command.Parameters.AddWithValue(
            "@NivelRiesgoId",
            dto.NivelRiesgoId);

        command.Parameters.AddWithValue(
            "@PorcentajeComision",
            dto.PorcentajeComision);

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }

    public async Task<bool> EliminarAsync(
        int creditoId)
    {
        using var connection = _connectionFactory.CreateConnection();

        using var command = new SqlCommand(
            "sp_Credito_Eliminar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue(
            "@CreditoId",
            creditoId);

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }

    private static CreditoDto MapearCredito(
        SqlDataReader reader)
    {
        return new CreditoDto
        {
            CreditoId = reader.GetInt32(
                reader.GetOrdinal("CreditoId")),

            NumeroObligacion = reader.GetString(
                reader.GetOrdinal("NumeroObligacion")),

            ClienteId = reader.GetInt32(
                reader.GetOrdinal("ClienteId")),

            VehiculoId = reader.GetInt32(
                reader.GetOrdinal("VehiculoId")),

            VendedorId = reader.GetInt32(
                reader.GetOrdinal("VendedorId")),

            NivelRiesgoId = reader.GetInt32(
                reader.GetOrdinal("NivelRiesgoId")),

            FechaAprobacion = DateOnly.FromDateTime(
                reader.GetDateTime(
                    reader.GetOrdinal("FechaAprobacion"))),

            PlazoMeses = reader.GetInt32(
                reader.GetOrdinal("PlazoMeses")),

            MontoEnganche = reader.GetDecimal(
                reader.GetOrdinal("MontoEnganche")),

            MontoNetoFinanciado = reader.GetDecimal(
                reader.GetOrdinal("MontoNetoFinanciado")),

            TasaInteresAnual = reader.GetDecimal(
                reader.GetOrdinal("TasaInteresAnual")),

            MontoTotalIntereses = reader.GetDecimal(
                reader.GetOrdinal("MontoTotalIntereses")),

            MontoTotalNetoRecaudable = reader.GetDecimal(
                reader.GetOrdinal("MontoTotalNetoRecaudable")),

            PorcentajeComision = reader.GetDecimal(
                reader.GetOrdinal("PorcentajeComision")),

            EstadoCreditoId = reader.GetInt32(
                reader.GetOrdinal("EstadoCreditoId"))
        };
    }
}