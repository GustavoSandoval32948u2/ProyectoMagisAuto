using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagisAuto.Application.DTOs.Municipio;
using MagisAuto.Application.Interfaces.Municipio;
using MagisAuto.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace MagisAuto.Infrastructure.Repositories
{
    public class MunicipioRepository : IMunicipioRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;

        public MunicipioRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<MunicipioDto>> ObtenerTodosAsync()
        {
            var municipios = new List<MunicipioDto>();

            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand(
                "sp_Municipio_ObtenerTodos",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                municipios.Add(new MunicipioDto
                {
                    MunicipioId = Convert.ToInt32(reader["MunicipioId"]),
                    DepartamentoId = Convert.ToInt32(reader["DepartamentoId"]),
                    Nombre = reader["Nombre"].ToString()!
                });
            }

            return municipios;
        }

        public async Task<IEnumerable<MunicipioDto>> BuscarAsync(
            string? criterio)
        {
            var municipios = new List<MunicipioDto>();

            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand(
                "sp_Municipio_Buscar",
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
                municipios.Add(new MunicipioDto
                {
                    MunicipioId = Convert.ToInt32(reader["MunicipioId"]),
                    DepartamentoId = Convert.ToInt32(reader["DepartamentoId"]),
                    Nombre = reader["Nombre"].ToString()!
                });
            }

            return municipios;
        }

        public async Task<int> CrearAsync(CrearMunicipioDto dto)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand(
                "sp_Municipio_Insertar",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(
                "@DepartamentoId",
                SqlDbType.Int).Value = dto.DepartamentoId;

            command.Parameters.Add(
                "@Nombre",
                SqlDbType.VarChar,
                100).Value = dto.Nombre;

            await connection.OpenAsync();

            var result = await command.ExecuteScalarAsync();

            return Convert.ToInt32(result);
        }

        public async Task<bool> ActualizarAsync(
            int municipioId,
            ActualizarMunicipioDto dto)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand(
                "sp_Municipio_Actualizar",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(
                "@MunicipioId",
                SqlDbType.Int).Value = municipioId;

            command.Parameters.Add(
                "@DepartamentoId",
                SqlDbType.Int).Value = dto.DepartamentoId;

            command.Parameters.Add(
                "@Nombre",
                SqlDbType.VarChar,
                100).Value = dto.Nombre;

            await connection.OpenAsync();

            await command.ExecuteNonQueryAsync();

            return true;
        }

        public async Task<bool> EliminarAsync(int municipioId)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand(
                "sp_Municipio_Eliminar",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(
                "@MunicipioId",
                SqlDbType.Int).Value = municipioId;

            await connection.OpenAsync();

            await command.ExecuteNonQueryAsync();

            return true;
        }
    }
}
