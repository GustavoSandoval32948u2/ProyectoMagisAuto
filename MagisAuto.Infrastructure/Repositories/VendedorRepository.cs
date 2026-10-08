using System.Data;
using Microsoft.Data.SqlClient;
using MagisAuto.Application.DTOs.Vendedor;
using MagisAuto.Application.Interfaces.Vendedor;
using MagisAuto.Infrastructure.Data;

namespace MagisAuto.Infrastructure.Repositories;

public class VendedorRepository : IVendedorRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public VendedorRepository(
        SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<VendedorDto>> ObtenerTodosAsync()
    {
        var lista = new List<VendedorDto>();

        using var connection = _connectionFactory.CreateConnection();

        using var command = new SqlCommand(
            "sp_Vendedor_ObtenerTodos",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            lista.Add(MapearVendedor(reader));
        }

        return lista;
    }

    public async Task<IEnumerable<VendedorDto>> BuscarAsync(
        string? criterio)
    {
        var lista = new List<VendedorDto>();

        using var connection = _connectionFactory.CreateConnection();

        using var command = new SqlCommand(
            "sp_Vendedor_Buscar",
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
            lista.Add(MapearVendedor(reader));
        }

        return lista;
    }

    public async Task<int> CrearAsync(
        CrearVendedorDto dto)
    {
        using var connection = _connectionFactory.CreateConnection();

        using var command = new SqlCommand(
            "sp_Vendedor_Insertar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue(
            "@CodigoEmpleado",
            dto.CodigoEmpleado);

        command.Parameters.AddWithValue(
            "@DPI",
            dto.DPI);

        command.Parameters.AddWithValue(
            "@NombreCompleto",
            dto.NombreCompleto);

        command.Parameters.AddWithValue(
            "@AgenciaId",
            dto.AgenciaId);

        await connection.OpenAsync();

        var resultado = await command.ExecuteScalarAsync();

        return Convert.ToInt32(resultado);
    }

    public async Task<bool> ActualizarAsync(
        int vendedorId,
        ActualizarVendedorDto dto)
    {
        using var connection = _connectionFactory.CreateConnection();

        using var command = new SqlCommand(
            "sp_Vendedor_Actualizar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue(
            "@VendedorId",
            vendedorId);

        command.Parameters.AddWithValue(
            "@CodigoEmpleado",
            dto.CodigoEmpleado);

        command.Parameters.AddWithValue(
            "@DPI",
            dto.DPI);

        command.Parameters.AddWithValue(
            "@NombreCompleto",
            dto.NombreCompleto);

        command.Parameters.AddWithValue(
            "@AgenciaId",
            dto.AgenciaId);

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }

    public async Task<bool> EliminarAsync(
        int vendedorId)
    {
        using var connection = _connectionFactory.CreateConnection();

        using var command = new SqlCommand(
            "sp_Vendedor_Eliminar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue(
            "@VendedorId",
            vendedorId);

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }

    private static VendedorDto MapearVendedor(
        SqlDataReader reader)
    {
        return new VendedorDto
        {
            VendedorId = reader.GetInt32(
                reader.GetOrdinal("VendedorId")),

            CodigoEmpleado = reader.GetString(
                reader.GetOrdinal("CodigoEmpleado")),

            DPI = reader.GetString(
                reader.GetOrdinal("DPI")),

            NombreCompleto = reader.GetString(
                reader.GetOrdinal("NombreCompleto")),

            AgenciaId = reader.GetInt32(
                reader.GetOrdinal("AgenciaId"))
        };
    }
}