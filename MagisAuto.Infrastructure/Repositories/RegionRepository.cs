using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagisAuto.Application.DTOs.Region;
using MagisAuto.Application.Interfaces.Region;
using MagisAuto.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace MagisAuto.Infrastructure.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;

        public RegionRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<RegionDto>> ObtenerTodosAsync()
        {
            var regiones = new List<RegionDto>();

            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand(
                "sp_Region_ObtenerTodos",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                regiones.Add(new RegionDto
                {
                    RegionId = Convert.ToInt32(reader["RegionId"]),
                    Nombre = reader["Nombre"].ToString()!
                });
            }

            return regiones;
        }

        public async Task<IEnumerable<RegionDto>> BuscarAsync(string? criterio)
        {
            var regiones = new List<RegionDto>();

            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand(
                "sp_Region_Buscar",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@Texto", SqlDbType.VarChar, 250).Value =
                string.IsNullOrWhiteSpace(criterio)
                    ? DBNull.Value
                    : criterio;

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                regiones.Add(new RegionDto
                {
                    RegionId = Convert.ToInt32(reader["RegionId"]),
                    Nombre = reader["Nombre"].ToString()!
                });
            }

            return regiones;
        }

        public async Task<int> CrearAsync(CrearRegionDto dto)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand(
                "sp_Region_Insertar",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value =
                dto.Nombre;

            await connection.OpenAsync();

            var result = await command.ExecuteScalarAsync();

            return Convert.ToInt32(result);
        }

        public async Task<bool> ActualizarAsync(
            int regionId,
            ActualizarRegionDto dto)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand(
                "sp_Region_Actualizar",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@RegionId", SqlDbType.Int).Value =
                regionId;

            command.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value =
                dto.Nombre;

            await connection.OpenAsync();

            await command.ExecuteNonQueryAsync();

            return true;
        }

        public async Task<bool> EliminarAsync(int regionId)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand(
                "sp_Region_Eliminar",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@RegionId", SqlDbType.Int).Value =
                regionId;

            await connection.OpenAsync();

            await command.ExecuteNonQueryAsync();

            return true;
        }
    }
}
