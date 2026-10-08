using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagisAuto.Application.DTOs.Marca;
using MagisAuto.Application.Interfaces.Marca;
using MagisAuto.Domain.Entities;
using MagisAuto.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace MagisAuto.Infrastructure.Repositories
{
    public class MarcaRepository : IMarcaRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;

        public MarcaRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<MarcaDto>> ObtenerTodosAsync()
        {
            var marcas = new List<MarcaDto>();

            await using var connection = _connectionFactory.CreateConnection();

            await using var command = new SqlCommand(
                "sp_Marca_ObtenerTodos",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();

            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                marcas.Add(new MarcaDto
                {
                    MarcaId = reader.GetInt32(
                        reader.GetOrdinal("MarcaId")),

                    Nombre = reader.GetString(
                        reader.GetOrdinal("Nombre")),

                    CodigoInterno = reader.GetString(
                        reader.GetOrdinal("CodigoInterno"))
                });
            }

            return marcas;
        }

        public async Task<IEnumerable<MarcaDto>> BuscarAsync(
            string? criterio)
        {
            var marcas = new List<MarcaDto>();

            await using var connection = _connectionFactory.CreateConnection();

            await using var command = new SqlCommand(
                "sp_Marca_Buscar",
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

            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                marcas.Add(new MarcaDto
                {
                    MarcaId = reader.GetInt32(
                        reader.GetOrdinal("MarcaId")),

                    Nombre = reader.GetString(
                        reader.GetOrdinal("Nombre")),

                    CodigoInterno = reader.GetString(
                        reader.GetOrdinal("CodigoInterno"))
                });
            }

            return marcas;
        }

        public async Task<int> CrearAsync(CrearMarcaDto marca)
        {
            await using var connection = _connectionFactory.CreateConnection();

            await using var command = new SqlCommand(
                "sp_Marca_Insertar",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(
                "@Nombre",
                SqlDbType.VarChar,
                80).Value = marca.Nombre;

            command.Parameters.Add(
                "@CodigoInterno",
                SqlDbType.VarChar,
                30).Value = marca.CodigoInterno;

            await connection.OpenAsync();

            var resultado = await command.ExecuteScalarAsync();

            return Convert.ToInt32(resultado);
        }

        public async Task<bool> ActualizarAsync(
            int marcaId,
            ActualizarMarcaDto marca)
        {
            await using var connection = _connectionFactory.CreateConnection();

            await using var command = new SqlCommand(
                "sp_Marca_Actualizar",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(
                "@MarcaId",
                SqlDbType.Int).Value = marcaId;

            command.Parameters.Add(
                "@Nombre",
                SqlDbType.VarChar,
                80).Value = marca.Nombre;

            command.Parameters.Add(
                "@CodigoInterno",
                SqlDbType.VarChar,
                30).Value = marca.CodigoInterno;

            await connection.OpenAsync();

            await command.ExecuteNonQueryAsync();

            return true;
        }

        public async Task<bool> EliminarAsync(int marcaId)
        {
            await using var connection = _connectionFactory.CreateConnection();

            await using var command = new SqlCommand(
                "sp_Marca_Eliminar",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(
                "@MarcaId",
                SqlDbType.Int).Value = marcaId;

            await connection.OpenAsync();

            await command.ExecuteNonQueryAsync();

            return true;
        }
    }
}
