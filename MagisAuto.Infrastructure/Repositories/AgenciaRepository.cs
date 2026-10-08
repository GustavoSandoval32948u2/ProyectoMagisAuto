using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagisAuto.Application.DTOs.Agencia;
using MagisAuto.Application.Interfaces.Agencia;
using MagisAuto.Domain.Entities;
using MagisAuto.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;

namespace MagisAuto.Infrastructure.Repositories
{
    public class AgenciaRepository : IAgenciaRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;

        public AgenciaRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<AgenciaDto>> ObtenerTodosAsync()
        {
            var agencias = new List<AgenciaDto>();

            await using var connection = _connectionFactory.CreateConnection();
            await using var command = new SqlCommand(
                "sp_Agencia_ObtenerTodos",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();

            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                agencias.Add(new AgenciaDto
                {
                    AgenciaId = reader.GetInt32(
                        reader.GetOrdinal("AgenciaId")),

                    Nombre = reader.GetString(
                        reader.GetOrdinal("Nombre")),

                    RegionId = reader.GetInt32(
                        reader.GetOrdinal("RegionId")),

                    MunicipioId = reader.GetInt32(
                        reader.GetOrdinal("MunicipioId")),

                    Direccion = reader.GetString(
                        reader.GetOrdinal("Direccion"))
                });
            }

            return agencias;
        }

        public async Task<IEnumerable<AgenciaDto>> BuscarAsync(
            string? criterio)
        {
            var agencias = new List<AgenciaDto>();

            using var connection = _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_Agencia_Buscar",
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
                agencias.Add(new AgenciaDto
                {
                    AgenciaId = reader.GetInt32(
                        reader.GetOrdinal("AgenciaId")),

                    Nombre = reader.GetString(
                        reader.GetOrdinal("Nombre")),

                    RegionId = reader.GetInt32(
                        reader.GetOrdinal("RegionId")),

                    MunicipioId = reader.GetInt32(
                        reader.GetOrdinal("MunicipioId")),

                    Direccion = reader.GetString(
                        reader.GetOrdinal("Direccion"))
                });
            }

            return agencias;
        }

        public async Task<int> CrearAsync(
            CrearAgenciaDto agencia)
        {
            await using var connection =
                _connectionFactory.CreateConnection();

            await using var command = new SqlCommand(
                "sp_Agencia_Insertar",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(
                "@Nombre",
                SqlDbType.VarChar,
                100).Value = agencia.Nombre;

            command.Parameters.Add(
                "@RegionId",
                SqlDbType.Int).Value = agencia.RegionId;

            command.Parameters.Add(
                "@MunicipioId",
                SqlDbType.Int).Value = agencia.MunicipioId;

            command.Parameters.Add(
                "@Direccion",
                SqlDbType.VarChar,
                250).Value = agencia.Direccion;

            await connection.OpenAsync();

            var resultado = await command.ExecuteScalarAsync();

            return Convert.ToInt32(resultado);
        }

        public async Task<bool> ActualizarAsync(
            int agenciaId,
            ActualizarAgenciaDto agencia)
        {
            await using var connection =
                _connectionFactory.CreateConnection();

            await using var command = new SqlCommand(
                "sp_Agencia_Actualizar",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(
                "@AgenciaId",
                SqlDbType.Int).Value = agenciaId;

            command.Parameters.Add(
                "@Nombre",
                SqlDbType.VarChar,
                100).Value = agencia.Nombre;

            command.Parameters.Add(
                "@RegionId",
                SqlDbType.Int).Value = agencia.RegionId;

            command.Parameters.Add(
                "@MunicipioId",
                SqlDbType.Int).Value = agencia.MunicipioId;

            command.Parameters.Add(
                "@Direccion",
                SqlDbType.VarChar,
                250).Value = agencia.Direccion;

            await connection.OpenAsync();

            await command.ExecuteNonQueryAsync();

            return true;
        }

        public async Task<bool> EliminarAsync(int agenciaId)
        {
            await using var connection =
                _connectionFactory.CreateConnection();

            await using var command = new SqlCommand(
                "sp_Agencia_Eliminar",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(
                "@AgenciaId",
                SqlDbType.Int).Value = agenciaId;

            await connection.OpenAsync();

            await command.ExecuteNonQueryAsync();

            return true;
        }
    }
}
