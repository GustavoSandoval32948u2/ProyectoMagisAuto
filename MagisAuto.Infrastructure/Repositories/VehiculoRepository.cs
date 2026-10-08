using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagisAuto.Application.DTOs.Vehiculo;
using MagisAuto.Application.Interfaces.Vehiculo;
using MagisAuto.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace MagisAuto.Infrastructure.Repositories
{
    public class VehiculoRepository : IVehiculoRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;

        public VehiculoRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<VehiculoDto>> ObtenerTodosAsync()
        {
            var vehiculos = new List<VehiculoDto>();

            using var connection = _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_Vehiculo_ObtenerTodos",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                vehiculos.Add(new VehiculoDto
                {
                    VehiculoId = Convert.ToInt32(reader["VehiculoId"]),
                    LineaVehiculoId = Convert.ToInt32(reader["LineaVehiculoId"]),
                    ConcesionariaId = Convert.ToInt32(reader["ConcesionariaId"]),
                    Vin = reader["Vin"].ToString()!,
                    AnioFabricacion = Convert.ToInt16(reader["AnioFabricacion"]),
                    ValorAgencia = Convert.ToDecimal(reader["ValorAgencia"]),
                    Placa = reader["Placa"] == DBNull.Value
                        ? null
                        : reader["Placa"].ToString()
                });
            }

            return vehiculos;
        }

        public async Task<IEnumerable<VehiculoDto>> BuscarAsync(
            string? criterio)
        {
            var vehiculos = new List<VehiculoDto>();

            using var connection = _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_Vehiculo_Buscar",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(
                "@Texto",
                SqlDbType.VarChar,
                250).Value =
                    string.IsNullOrWhiteSpace(criterio)
                        ? DBNull.Value
                        : criterio;

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                vehiculos.Add(new VehiculoDto
                {
                    VehiculoId = Convert.ToInt32(reader["VehiculoId"]),
                    LineaVehiculoId = Convert.ToInt32(reader["LineaVehiculoId"]),
                    ConcesionariaId = Convert.ToInt32(reader["ConcesionariaId"]),
                    Vin = reader["Vin"].ToString()!,
                    AnioFabricacion = Convert.ToInt16(reader["AnioFabricacion"]),
                    ValorAgencia = Convert.ToDecimal(reader["ValorAgencia"]),
                    Placa = reader["Placa"] == DBNull.Value
                        ? null
                        : reader["Placa"].ToString()
                });
            }

            return vehiculos;
        }

        public async Task<int> CrearAsync(CrearVehiculoDto dto)
        {
            using var connection = _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_Vehiculo_Insertar",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(
                "@LineaVehiculoId",
                SqlDbType.Int).Value = dto.LineaVehiculoId;

            command.Parameters.Add(
                "@ConcesionariaId",
                SqlDbType.Int).Value = dto.ConcesionariaId;

            command.Parameters.Add(
                "@Vin",
                SqlDbType.VarChar,
                17).Value = dto.Vin;

            command.Parameters.Add(
                "@AnioFabricacion",
                SqlDbType.SmallInt).Value = dto.AnioFabricacion;

            var valorAgencia = command.Parameters.Add(
                "@ValorAgencia",
                SqlDbType.Decimal);

            valorAgencia.Precision = 12;
            valorAgencia.Scale = 2;
            valorAgencia.Value = dto.ValorAgencia;

            command.Parameters.Add(
                "@Placa",
                SqlDbType.VarChar,
                15).Value =
                    string.IsNullOrWhiteSpace(dto.Placa)
                        ? DBNull.Value
                        : dto.Placa;

            await connection.OpenAsync();

            var result = await command.ExecuteScalarAsync();

            return Convert.ToInt32(result);
        }

        public async Task<bool> ActualizarAsync(
            int vehiculoId,
            ActualizarVehiculoDto dto)
        {
            using var connection = _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_Vehiculo_Actualizar",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(
                "@VehiculoId",
                SqlDbType.Int).Value = vehiculoId;

            command.Parameters.Add(
                "@LineaVehiculoId",
                SqlDbType.Int).Value = dto.LineaVehiculoId;

            command.Parameters.Add(
                "@ConcesionariaId",
                SqlDbType.Int).Value = dto.ConcesionariaId;

            command.Parameters.Add(
                "@Vin",
                SqlDbType.VarChar,
                17).Value = dto.Vin;

            command.Parameters.Add(
                "@AnioFabricacion",
                SqlDbType.SmallInt).Value = dto.AnioFabricacion;

            var valorAgencia = command.Parameters.Add(
                "@ValorAgencia",
                SqlDbType.Decimal);

            valorAgencia.Precision = 12;
            valorAgencia.Scale = 2;
            valorAgencia.Value = dto.ValorAgencia;

            command.Parameters.Add(
                "@Placa",
                SqlDbType.VarChar,
                15).Value =
                    string.IsNullOrWhiteSpace(dto.Placa)
                        ? DBNull.Value
                        : dto.Placa;

            await connection.OpenAsync();

            await command.ExecuteNonQueryAsync();

            return true;
        }

        public async Task<bool> EliminarAsync(int vehiculoId)
        {
            using var connection = _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_Vehiculo_Eliminar",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(
                "@VehiculoId",
                SqlDbType.Int).Value = vehiculoId;

            await connection.OpenAsync();

            await command.ExecuteNonQueryAsync();

            return true;
        }
    }
}
