using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagisAuto.Application.DTOs.LineaVehiculo;
using MagisAuto.Application.Interfaces.LineaVehiculo;
using MagisAuto.Domain.Entities;
using MagisAuto.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace MagisAuto.Infrastructure.Repositories
{
    public class LineaVehiculoRepository : ILineaVehiculoRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;

        public LineaVehiculoRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<LineaVehiculoDto>> ObtenerTodosAsync()
        {
            var lineas = new List<LineaVehiculoDto>();

            await using var connection = _connectionFactory.CreateConnection();

            await using var command = new SqlCommand(
                "sp_LineaVehiculo_ObtenerTodos",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();

            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lineas.Add(new LineaVehiculoDto
                {
                    LineaVehiculoId = reader.GetInt32(
                        reader.GetOrdinal("LineaVehiculoId")),

                    MarcaId = reader.GetInt32(
                        reader.GetOrdinal("MarcaId")),

                    Nombre = reader.GetString(
                        reader.GetOrdinal("Nombre"))
                });
            }

            return lineas;
        }
        public async Task<IEnumerable<LineaVehiculoDto>> BuscarAsync(string? criterio)
        {
            var lineas = new List<LineaVehiculoDto>();

            await using var connection = _connectionFactory.CreateConnection();

            await using var command = new SqlCommand("sp_LineaVehiculo_Buscar", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(
                "@Texto", SqlDbType.VarChar, 250).Value = string.IsNullOrWhiteSpace(criterio)
                ? DBNull.Value : criterio;

            await connection.OpenAsync();

            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lineas.Add(new LineaVehiculoDto
                {
                    LineaVehiculoId = reader.GetInt32(reader.GetOrdinal("LineaVehiculoId")),
                    MarcaId = reader.GetInt32(reader.GetOrdinal("MarcaId")),
                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),

                });
            }

            return lineas;
        }
        public async Task<int> CrearAsync(CrearLineaVehiculoDto lineaVehiculo)
        {
            await using var connection = _connectionFactory.CreateConnection();

            await using var command = new SqlCommand("sp_LineaVehiculo_Insertar", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@MarcaId", SqlDbType.Int).Value = lineaVehiculo.MarcaId;

            command.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = lineaVehiculo.Nombre;

            await connection.OpenAsync();

            var resultado = await command.ExecuteScalarAsync();

            return Convert.ToInt32(resultado);
        }
        public async Task<bool> ActualizarAsync(int lineaVehiculoId, ActualizarLineaVehiculoDto lineaVehiculo)
        {
            await using var connection = _connectionFactory.CreateConnection();

            await using var command = new SqlCommand("sp_LineaVehiculo_Actualizar", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@LineaVehiculoId", SqlDbType.Int).Value = lineaVehiculoId;

            command.Parameters.Add("@MarcaId", SqlDbType.Int).Value = lineaVehiculo.MarcaId;

            command.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = lineaVehiculo.Nombre;

            await connection.OpenAsync();

            await command.ExecuteNonQueryAsync();

            return true;
        }
        public async Task<bool> EliminarAsync(int lineaVehiculoId)
        {
            await using var connection = _connectionFactory.CreateConnection();

            await using var command = new SqlCommand("sp_LineaVehiculo_Eliminar", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@LineaVehiculoId", SqlDbType.Int).Value = lineaVehiculoId;

            await connection.OpenAsync();

            await command.ExecuteNonQueryAsync();

            return true;
        }
    }
}
