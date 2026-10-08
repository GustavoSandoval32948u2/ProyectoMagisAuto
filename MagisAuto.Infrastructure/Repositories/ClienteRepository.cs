using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagisAuto.Application.DTOs.Cliente;
using MagisAuto.Application.Interfaces.Cliente;
using MagisAuto.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace MagisAuto.Infrastructure.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public ClienteRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<ClienteDto>> ObtenerTodosAsync()
    {
        var clientes = new List<ClienteDto>();

        await using var connection = _connectionFactory.CreateConnection();

        await using var command = new SqlCommand(
            "sp_Cliente_ObtenerTodos",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        await connection.OpenAsync();

        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            clientes.Add(new ClienteDto
            {
                ClienteId = reader.GetInt32(reader.GetOrdinal("ClienteId")),
                Nit = reader.GetString(reader.GetOrdinal("Nit")),
                Dpi = reader.GetString(reader.GetOrdinal("Dpi")),
                NombreCompleto = reader.GetString(reader.GetOrdinal("NombreCompleto")),
                CorreoElectronico = reader.GetString(reader.GetOrdinal("CorreoElectronico")),
                FechaNacimiento = reader.IsDBNull(reader.GetOrdinal("FechaNacimiento"))
                    ? null
                    : reader.GetDateTime(reader.GetOrdinal("FechaNacimiento"))
            });
        }

        return clientes;
    }

    public async Task<IEnumerable<ClienteDto>> BuscarAsync(string? criterio)
    {
        var clientes = new List<ClienteDto>();

        await using var connection = _connectionFactory.CreateConnection();

        await using var command = new SqlCommand(
            "sp_Cliente_Buscar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add(
            "@Texto",
            SqlDbType.VarChar,
            250).Value = string.IsNullOrWhiteSpace(criterio)
                ? DBNull.Value
                : criterio;

        await connection.OpenAsync();

        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            clientes.Add(new ClienteDto
            {
                ClienteId = reader.GetInt32(reader.GetOrdinal("ClienteId")),
                Nit = reader.GetString(reader.GetOrdinal("Nit")),
                Dpi = reader.GetString(reader.GetOrdinal("Dpi")),
                NombreCompleto = reader.GetString(reader.GetOrdinal("NombreCompleto")),
                CorreoElectronico = reader.GetString(reader.GetOrdinal("CorreoElectronico")),
                FechaNacimiento = reader.IsDBNull(reader.GetOrdinal("FechaNacimiento"))
                    ? null
                    : reader.GetDateTime(reader.GetOrdinal("FechaNacimiento"))
            });
        }

        return clientes;
    }

    public async Task<int> CrearAsync(CrearClienteDto cliente)
    {
        await using var connection = _connectionFactory.CreateConnection();

        await using var command = new SqlCommand(
            "sp_Cliente_Insertar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add(
            "@NIT",
            SqlDbType.VarChar,
            20).Value = cliente.Nit;

        command.Parameters.Add(
            "@DPI",
            SqlDbType.VarChar,
            13).Value = cliente.Dpi;

        command.Parameters.Add(
            "@NombreCompleto",
            SqlDbType.VarChar,
            150).Value = cliente.NombreCompleto;

        command.Parameters.Add(
            "@CorreoElectronico",
            SqlDbType.VarChar,
            150).Value = cliente.CorreoElectronico;

        command.Parameters.Add(
            "@FechaNacimiento",
            SqlDbType.Date).Value =
                cliente.FechaNacimiento ?? (object)DBNull.Value;

        await connection.OpenAsync();

        var resultado = await command.ExecuteScalarAsync();

        return Convert.ToInt32(resultado);
    }

    public async Task<bool> ActualizarAsync(
        int clienteId,
        ActualizarClienteDto cliente)
    {
        await using var connection = _connectionFactory.CreateConnection();

        await using var command = new SqlCommand(
            "sp_Cliente_Actualizar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add(
            "@ClienteId",
            SqlDbType.Int).Value = clienteId;

        command.Parameters.Add(
            "@NIT",
            SqlDbType.VarChar,
            20).Value = cliente.Nit;

        command.Parameters.Add(
            "@DPI",
            SqlDbType.VarChar,
            13).Value = cliente.Dpi;

        command.Parameters.Add(
            "@NombreCompleto",
            SqlDbType.VarChar,
            150).Value = cliente.NombreCompleto;

        command.Parameters.Add(
            "@CorreoElectronico",
            SqlDbType.VarChar,
            150).Value = cliente.CorreoElectronico;

        command.Parameters.Add(
            "@FechaNacimiento",
            SqlDbType.Date).Value =
                cliente.FechaNacimiento ?? (object)DBNull.Value;

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }

    public async Task<bool> EliminarAsync(int clienteId)
    {
        await using var connection = _connectionFactory.CreateConnection();

        await using var command = new SqlCommand(
            "sp_Cliente_Eliminar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add(
            "@ClienteId",
            SqlDbType.Int).Value = clienteId;

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return true;
    }
}