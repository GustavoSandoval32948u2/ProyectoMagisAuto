using MagisAuto.Application.DTOs.Pago;
using MagisAuto.Application.Interfaces.Pago;
using MagisAuto.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MagisAuto.Infrastructure.Repositories;

public class PagoRepository : IPagoRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public PagoRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<PagoDto>> ObtenerTodosAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_Pago_ObtenerTodos", connection);

        command.CommandType = CommandType.StoredProcedure;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        var resultado = new List<PagoDto>();

        while (await reader.ReadAsync())
        {
            resultado.Add(Mapear(reader));
        }

        return resultado;
    }

    public async Task<IEnumerable<PagoDto>> BuscarAsync(string? criterio)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_Pago_Buscar", connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@Texto", SqlDbType.VarChar, 250)
            .Value = string.IsNullOrWhiteSpace(criterio)
                ? DBNull.Value
                : criterio;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        var resultado = new List<PagoDto>();

        while (await reader.ReadAsync())
        {
            resultado.Add(Mapear(reader));
        }

        return resultado;
    }

    public async Task<int> CrearAsync(CrearPagoDto dto)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_Pago_Insertar", connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@CuotaId", dto.CuotaId);
        command.Parameters.AddWithValue("@NumeroComprobante", dto.NumeroComprobante);
        command.Parameters.AddWithValue("@MontoPagado", dto.MontoPagado);
        command.Parameters.AddWithValue("@MedioPagoId", dto.MedioPagoId);

        command.Parameters.Add("@BancoId", SqlDbType.Int)
            .Value = dto.BancoId.HasValue
                ? dto.BancoId.Value
                : DBNull.Value;

        command.Parameters.Add("@ReferenciaPago", SqlDbType.VarChar, 100)
            .Value = string.IsNullOrWhiteSpace(dto.ReferenciaPago)
                ? DBNull.Value
                : dto.ReferenciaPago;

        command.Parameters.Add("@FechaHoraPago", SqlDbType.DateTime2)
            .Value = dto.FechaHoraPago.HasValue
                ? dto.FechaHoraPago.Value
                : DBNull.Value;

        await connection.OpenAsync();

        var resultado = await command.ExecuteScalarAsync();

        return Convert.ToInt32(resultado);
    }

    public async Task<bool> ActualizarAsync(
        int pagoId,
        ActualizarPagoDto dto)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_Pago_Actualizar", connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@PagoId", pagoId);
        command.Parameters.AddWithValue("@CuotaId", dto.CuotaId);
        command.Parameters.AddWithValue("@NumeroComprobante", dto.NumeroComprobante);
        command.Parameters.AddWithValue("@MontoPagado", dto.MontoPagado);
        command.Parameters.AddWithValue("@MedioPagoId", dto.MedioPagoId);

        command.Parameters.Add("@BancoId", SqlDbType.Int)
            .Value = dto.BancoId.HasValue
                ? dto.BancoId.Value
                : DBNull.Value;

        command.Parameters.Add("@ReferenciaPago", SqlDbType.VarChar, 100)
            .Value = string.IsNullOrWhiteSpace(dto.ReferenciaPago)
                ? DBNull.Value
                : dto.ReferenciaPago;

        command.Parameters.Add("@FechaHoraPago", SqlDbType.DateTime2)
            .Value = dto.FechaHoraPago.HasValue
                ? dto.FechaHoraPago.Value
                : DBNull.Value;

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }

    public async Task<bool> EliminarAsync(int pagoId)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_Pago_Eliminar", connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@PagoId", pagoId);

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }

    private static PagoDto Mapear(SqlDataReader reader)
    {
        return new PagoDto
        {
            PagoId = reader.GetInt32(reader.GetOrdinal("PagoId")),
            CuotaId = reader.GetInt32(reader.GetOrdinal("CuotaId")),
            NumeroComprobante = reader.GetString(
                reader.GetOrdinal("NumeroComprobante")),
            FechaHoraPago = reader.GetDateTime(
                reader.GetOrdinal("FechaHoraPago")),
            MontoPagado = reader.GetDecimal(
                reader.GetOrdinal("MontoPagado")),
            MedioPagoId = reader.GetInt32(
                reader.GetOrdinal("MedioPagoId")),
            BancoId = reader.IsDBNull(reader.GetOrdinal("BancoId"))
                ? null
                : reader.GetInt32(reader.GetOrdinal("BancoId")),
            ReferenciaPago = reader.IsDBNull(
                reader.GetOrdinal("ReferenciaPago"))
                ? null
                : reader.GetString(reader.GetOrdinal("ReferenciaPago"))
        };
    }
}