using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagisAuto.Application.DTOs.Parentesco;
using MagisAuto.Application.Interfaces.Parentesco;
using MagisAuto.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace MagisAuto.Infrastructure.Repositories
{
    public class ParentescoRepository : IParentescoRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;

        public ParentescoRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<ParentescoDto>> ObtenerTodosAsync()
        {
            var parentescos = new List<ParentescoDto>();

            await using var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync();

            await using var command = new SqlCommand("sp_Parentesco_ObtenerTodos", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                parentescos.Add(new ParentescoDto
                {
                    ParentescoId = reader.GetInt32(reader.GetOrdinal("ParentescoId")),
                    Nombre = reader.GetString(reader.GetOrdinal("Nombre"))
                });
            }

            return parentescos;
        }

        public async Task<IEnumerable<ParentescoDto>> BuscarAsync(string? criterio)
        {
            var parentescos = new List<ParentescoDto>();

            await using var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync();

            await using var command = new SqlCommand("sp_Parentesco_Buscar", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@Texto", SqlDbType.VarChar, 250).Value =
                string.IsNullOrWhiteSpace(criterio) ? DBNull.Value : criterio;

            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                parentescos.Add(new ParentescoDto
                {
                    ParentescoId = reader.GetInt32(reader.GetOrdinal("ParentescoId")),
                    Nombre = reader.GetString(reader.GetOrdinal("Nombre"))
                });
            }

            return parentescos;
        }

        public async Task<int> CrearAsync(CrearParentescoDto dto)
        {
            await using var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync();

            await using var command = new SqlCommand("sp_Parentesco_Insertar", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@Nombre", SqlDbType.VarChar, 80).Value = dto.Nombre;

            var resultado = await command.ExecuteScalarAsync();

            return Convert.ToInt32(resultado);
        }

        public async Task<bool> ActualizarAsync(
            int parentescoId,
            ActualizarParentescoDto dto)
        {
            await using var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync();

            await using var command = new SqlCommand("sp_Parentesco_Actualizar", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@ParentescoId", SqlDbType.Int).Value = parentescoId;
            command.Parameters.Add("@Nombre", SqlDbType.VarChar, 80).Value = dto.Nombre;

            await command.ExecuteNonQueryAsync();

            return true;
        }

        public async Task<bool> EliminarAsync(int parentescoId)
        {
            await using var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync();

            await using var command = new SqlCommand("sp_Parentesco_Eliminar", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@ParentescoId", SqlDbType.Int).Value = parentescoId;

            await command.ExecuteNonQueryAsync();

            return true;
        }
    }
}