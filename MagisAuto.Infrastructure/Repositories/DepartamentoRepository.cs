using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagisAuto.Application.DTOs.Departamento;
using MagisAuto.Application.Interfaces.Departamento;
using MagisAuto.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace MagisAuto.Infrastructure.Repositories
{
    public class DepartamentoRepository : IDepartamentoRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;

        public DepartamentoRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<DepartamentoDto>> ObtenerTodosAsync()
        {
            var departamentos = new List<DepartamentoDto>();

            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand(
                "sp_Departamento_ObtenerTodos",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                departamentos.Add(new DepartamentoDto
                {
                    DepartamentoId = Convert.ToInt32(reader["DepartamentoId"]),
                    Nombre = reader["Nombre"].ToString()!
                });
            }

            return departamentos;
        }

        public async Task<IEnumerable<DepartamentoDto>> BuscarAsync(
            string? criterio)
        {
            var departamentos = new List<DepartamentoDto>();

            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand(
                "sp_Departamento_Buscar",
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
                departamentos.Add(new DepartamentoDto
                {
                    DepartamentoId = Convert.ToInt32(reader["DepartamentoId"]),
                    Nombre = reader["Nombre"].ToString()!
                });
            }

            return departamentos;
        }

        public async Task<int> CrearAsync(CrearDepartamentoDto dto)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand(
                "sp_Departamento_Insertar",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(
                "@Nombre",
                SqlDbType.VarChar,
                100).Value = dto.Nombre;

            await connection.OpenAsync();

            var result = await command.ExecuteScalarAsync();

            return Convert.ToInt32(result);
        }

        public async Task<bool> ActualizarAsync(
            int departamentoId,
            ActualizarDepartamentoDto dto)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand(
                "sp_Departamento_Actualizar",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(
                "@DepartamentoId",
                SqlDbType.Int).Value = departamentoId;

            command.Parameters.Add(
                "@Nombre",
                SqlDbType.VarChar,
                100).Value = dto.Nombre;

            await connection.OpenAsync();

            await command.ExecuteNonQueryAsync();

            return true;
        }

        public async Task<bool> EliminarAsync(int departamentoId)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand(
                "sp_Departamento_Eliminar",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(
                "@DepartamentoId",
                SqlDbType.Int).Value = departamentoId;

            await connection.OpenAsync();

            await command.ExecuteNonQueryAsync();

            return true;
        }
    }
}
