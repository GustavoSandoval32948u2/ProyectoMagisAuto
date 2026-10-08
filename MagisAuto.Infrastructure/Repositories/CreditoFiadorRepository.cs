using MagisAuto.Application.DTOs.CreditoFiador;
using MagisAuto.Application.Interfaces.CreditoFiador;
using MagisAuto.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MagisAuto.Infrastructure.Repositories;

public class CreditoFiadorRepository : ICreditoFiadorRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public CreditoFiadorRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<CreditoFiadorDto>> ObtenerTodosAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_CreditoFiador_ObtenerTodos", connection);

        command.CommandType = CommandType.StoredProcedure;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        var resultado = new List<CreditoFiadorDto>();

        while (await reader.ReadAsync())
        {
            resultado.Add(Mapear(reader));
        }

        return resultado;
    }

    public async Task<IEnumerable<CreditoFiadorDto>> BuscarAsync(string? criterio)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_CreditoFiador_Buscar", connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@Texto", SqlDbType.VarChar, 250)
            .Value = string.IsNullOrWhiteSpace(criterio)
                ? DBNull.Value
                : criterio;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        var resultado = new List<CreditoFiadorDto>();

        while (await reader.ReadAsync())
        {
            resultado.Add(Mapear(reader));
        }

        return resultado;
    }

    public async Task<int> CrearAsync(CrearCreditoFiadorDto dto)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_CreditoFiador_Insertar", connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@CreditoId", dto.CreditoId);
        command.Parameters.AddWithValue("@PersonaId", dto.PersonaId);

        await connection.OpenAsync();

        var resultado = await command.ExecuteScalarAsync();

        return Convert.ToInt32(resultado);
    }

    public async Task<bool> ActualizarAsync(
        int creditoFiadorId,
        ActualizarCreditoFiadorDto dto)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_CreditoFiador_Actualizar", connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@CreditoFiadorId", creditoFiadorId);
        command.Parameters.AddWithValue("@CreditoId", dto.CreditoId);
        command.Parameters.AddWithValue("@PersonaId", dto.PersonaId);

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }

    public async Task<bool> EliminarAsync(int creditoFiadorId)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_CreditoFiador_Eliminar", connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@CreditoFiadorId", creditoFiadorId);

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }

    private static CreditoFiadorDto Mapear(SqlDataReader reader)
    {
        return new CreditoFiadorDto
        {
            CreditoFiadorId = reader.GetInt32(reader.GetOrdinal("CreditoFiadorId")),
            CreditoId = reader.GetInt32(reader.GetOrdinal("CreditoId")),
            PersonaId = reader.GetInt32(reader.GetOrdinal("PersonaId"))
        };
    }
}