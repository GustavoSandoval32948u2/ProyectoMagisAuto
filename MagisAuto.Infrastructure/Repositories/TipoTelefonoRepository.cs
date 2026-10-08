using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagisAuto.Application.DTOs.TipoTelefono;
using MagisAuto.Application.Interfaces.TipoTelefono;
using MagisAuto.Domain.Entities;
using MagisAuto.Infrastructure.Data;
using Microsoft.Data.SqlClient;


namespace MagisAuto.Infrastructure.Repositories
{
    public class TipoTelefonoRepository : ITipoTelefonoRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;

        public TipoTelefonoRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<TipoTelefonoDto>> ObtenerTodosAsync()
        {
            var lista = new List<TipoTelefonoDto>();

            using var connection = _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_TipoTelefono_ObtenerTodos",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(new TipoTelefonoDto
                {
                    TipoTelefonoId = Convert.ToInt32(
                        reader["TipoTelefonoId"]),

                    Nombre = reader["Nombre"]?.ToString()
                        ?? string.Empty
                });
            }

            return lista;
        }

        public async Task<IEnumerable<TipoTelefonoDto>> BuscarAsync(
            string? criterio)
        {
            var lista = new List<TipoTelefonoDto>();

            using var connection = _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_TipoTelefono_Buscar",
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
                lista.Add(new TipoTelefonoDto
                {
                    TipoTelefonoId = Convert.ToInt32(
                        reader["TipoTelefonoId"]),

                    Nombre = reader["Nombre"]?.ToString()
                        ?? string.Empty
                });
            }

            return lista;
        }

        public async Task<int> CrearAsync(CrearTipoTelefonoDto dto)
        {
            using var connection = _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_TipoTelefono_Insertar",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(
                "@Nombre",
                SqlDbType.VarChar,
                30).Value = dto.Nombre;

            await connection.OpenAsync();

            var result = await command.ExecuteScalarAsync();

            return Convert.ToInt32(result);
        }

        public async Task<bool> ActualizarAsync(
            int tipoTelefonoId,
            ActualizarTipoTelefonoDto dto)
        {
            using var connection = _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_TipoTelefono_Actualizar",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(
                "@TipoTelefonoId",
                SqlDbType.Int).Value = tipoTelefonoId;

            command.Parameters.Add(
                "@Nombre",
                SqlDbType.VarChar,
                30).Value = dto.Nombre;

            await connection.OpenAsync();

            await command.ExecuteNonQueryAsync();

            return true;
        }

        public async Task<bool> EliminarAsync(int tipoTelefonoId)
        {
            using var connection = _connectionFactory.CreateConnection();

            using var command = new SqlCommand(
                "sp_TipoTelefono_Eliminar",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(
                "@TipoTelefonoId",
                SqlDbType.Int).Value = tipoTelefonoId;

            await connection.OpenAsync();

            await command.ExecuteNonQueryAsync();

            return true;
        }
    }
}
