using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MagisAuto.Application.DTOs.Reportes;
using MagisAuto.Application.Interfaces.Reportes;
using MagisAuto.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MagisAuto.Infrastructure.Repositories;

public class ReporteRepository : IReporteRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public ReporteRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<CreditosPorEstadoDto>> ObtenerCreditosPorEstadoAsync()
    {
        var resultados = new List<CreditosPorEstadoDto>();

        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_Reporte_CreditosPorEstado", connection);

        command.CommandType = CommandType.StoredProcedure;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            resultados.Add(new CreditosPorEstadoDto
            {
                EstadoCreditoId = reader.GetInt32(
                    reader.GetOrdinal("EstadoCreditoId")),

                Nombre = reader.GetString(
                    reader.GetOrdinal("Nombre")),

                Cantidad = reader.IsDBNull(reader.GetOrdinal("Cantidad"))
                    ? null
                    : reader.GetInt32(reader.GetOrdinal("Cantidad")),

                TotalFinanciado = reader.IsDBNull(reader.GetOrdinal("TotalFinanciado"))
                    ? null
                    : reader.GetDecimal(reader.GetOrdinal("TotalFinanciado"))
            });
        }

        return resultados;
    }
    public async Task<IEnumerable<CuotasPorEstadoDto>> ObtenerCuotasPorEstadoAsync()
    {
        var resultados = new List<CuotasPorEstadoDto>();

        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_Reporte_CuotasPorEstado", connection);

        command.CommandType = CommandType.StoredProcedure;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            resultados.Add(new CuotasPorEstadoDto
            {
                EstadoCuotaId = reader.GetInt32(
                    reader.GetOrdinal("EstadoCuotaId")),

                Nombre = reader.GetString(
                    reader.GetOrdinal("Nombre")),

                Cantidad = reader.IsDBNull(reader.GetOrdinal("Cantidad"))
                    ? null
                    : reader.GetInt32(reader.GetOrdinal("Cantidad")),

                TotalCuotas = reader.IsDBNull(reader.GetOrdinal("TotalCuotas"))
                    ? null
                    : reader.GetDecimal(reader.GetOrdinal("TotalCuotas"))
            });
        }

        return resultados;
    }
    public async Task<IEnumerable<VehiculosPorMarcaDto>> ObtenerVehiculosPorMarcaAsync()
    {
        var resultados = new List<VehiculosPorMarcaDto>();

        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_Reporte_VehiculosPorMarca",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            resultados.Add(new VehiculosPorMarcaDto
            {
                MarcaId = reader.GetInt32(
                    reader.GetOrdinal("MarcaId")),

                Nombre = reader.GetString(
                    reader.GetOrdinal("Nombre")),

                CantidadVehiculos = reader.IsDBNull(
                    reader.GetOrdinal("CantidadVehiculos"))
                        ? null
                        : reader.GetInt32(
                            reader.GetOrdinal("CantidadVehiculos")),

                ValorAgenciaTotal = reader.IsDBNull(
                    reader.GetOrdinal("ValorAgenciaTotal"))
                        ? null
                        : reader.GetDecimal(
                            reader.GetOrdinal("ValorAgenciaTotal"))
            });
        }

        return resultados;
    }
    public async Task<IEnumerable<CreditosPorAgenciaDto>> ObtenerCreditosPorAgenciaAsync()
    {
        var resultados = new List<CreditosPorAgenciaDto>();

        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_Reporte_CreditosPorAgencia",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            resultados.Add(new CreditosPorAgenciaDto
            {
                AgenciaId = reader.GetInt32(
                    reader.GetOrdinal("AgenciaId")),

                Nombre = reader.GetString(
                    reader.GetOrdinal("Nombre")),

                CantidadCreditos = reader.IsDBNull(
                    reader.GetOrdinal("CantidadCreditos"))
                        ? null
                        : reader.GetInt32(
                            reader.GetOrdinal("CantidadCreditos")),

                TotalFinanciado = reader.IsDBNull(
                    reader.GetOrdinal("TotalFinanciado"))
                        ? null
                        : reader.GetDecimal(
                            reader.GetOrdinal("TotalFinanciado"))
            });
        }

        return resultados;
    }
    public async Task<IEnumerable<CreditosPorRegionDto>> ObtenerCreditosPorRegionAsync()
    {
        var resultados = new List<CreditosPorRegionDto>();

        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_Reporte_CreditosPorRegion",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            resultados.Add(new CreditosPorRegionDto
            {
                RegionId = reader.GetInt32(
                    reader.GetOrdinal("RegionId")),

                Nombre = reader.GetString(
                    reader.GetOrdinal("Nombre")),

                CantidadCreditos = reader.IsDBNull(
                    reader.GetOrdinal("CantidadCreditos"))
                        ? null
                        : reader.GetInt32(
                            reader.GetOrdinal("CantidadCreditos")),

                TotalFinanciado = reader.IsDBNull(
                    reader.GetOrdinal("TotalFinanciado"))
                        ? null
                        : reader.GetDecimal(
                            reader.GetOrdinal("TotalFinanciado"))
            });
        }

        return resultados;
    }
    public async Task<IEnumerable<CreditosAprobadosPorFechaDto>> ObtenerCreditosAprobadosPorFechaAsync(
    DateOnly fechaInicio,
    DateOnly fechaFin)
    {
        var resultados = new List<CreditosAprobadosPorFechaDto>();

        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_Reporte_CreditosAprobadosPorFecha",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@FechaInicio", SqlDbType.Date)
            .Value = fechaInicio.ToDateTime(TimeOnly.MinValue);

        command.Parameters.Add("@FechaFin", SqlDbType.Date)
            .Value = fechaFin.ToDateTime(TimeOnly.MinValue);

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            resultados.Add(new CreditosAprobadosPorFechaDto
            {
                NumeroObligacion = reader.GetString(
                    reader.GetOrdinal("NumeroObligacion")),

                FechaAprobacion = DateOnly.FromDateTime(
                    reader.GetDateTime(
                        reader.GetOrdinal("FechaAprobacion"))),

                Cliente = reader.GetString(
                    reader.GetOrdinal("Cliente")),

                Vin = reader.GetString(
                    reader.GetOrdinal("Vin")),

                Marca = reader.GetString(
                    reader.GetOrdinal("Marca")),

                Linea = reader.GetString(
                    reader.GetOrdinal("Linea")),

                Vendedor = reader.GetString(
                    reader.GetOrdinal("Vendedor")),

                MontoNetoFinanciado = reader.GetDecimal(
                    reader.GetOrdinal("MontoNetoFinanciado")),

                Estado = reader.GetString(
                    reader.GetOrdinal("Estado"))
            });
        }

        return resultados;
    }
    public async Task<IEnumerable<PagosPorFechaYMedioDto>> ObtenerPagosPorFechaYMedioAsync(
    DateOnly fechaInicio,
    DateOnly fechaFin,
    int? medioPagoId)
    {
        var resultados = new List<PagosPorFechaYMedioDto>();

        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_Reporte_PagosPorFechaYMedio",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@FechaInicio", SqlDbType.Date)
            .Value = fechaInicio.ToDateTime(TimeOnly.MinValue);

        command.Parameters.Add("@FechaFin", SqlDbType.Date)
            .Value = fechaFin.ToDateTime(TimeOnly.MinValue);

        command.Parameters.Add("@MedioPagoId", SqlDbType.Int)
            .Value = medioPagoId.HasValue
                ? medioPagoId.Value
                : DBNull.Value;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            resultados.Add(new PagosPorFechaYMedioDto
            {
                PagoId = reader.GetInt32(
                    reader.GetOrdinal("PagoId")),

                FechaHoraPago = reader.GetDateTime(
                    reader.GetOrdinal("FechaHoraPago")),

                NumeroObligacion = reader.GetString(
                    reader.GetOrdinal("NumeroObligacion")),

                MedioPago = reader.GetString(
                    reader.GetOrdinal("MedioPago")),

                MontoPagado = reader.GetDecimal(
                    reader.GetOrdinal("MontoPagado")),

                Banco = reader.IsDBNull(
                    reader.GetOrdinal("Banco"))
                        ? null
                        : reader.GetString(
                            reader.GetOrdinal("Banco")),

                Referencia = reader.IsDBNull(
                    reader.GetOrdinal("Referencia"))
                        ? null
                        : reader.GetString(
                            reader.GetOrdinal("Referencia"))
            });
        }

        return resultados;
    }
    public async Task<IEnumerable<VentasPorVendedorDto>> ObtenerVentasPorVendedorAsync(
    DateOnly? fechaInicio,
    DateOnly? fechaFin)
    {
        var resultado = new List<VentasPorVendedorDto>();

        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_Reporte_VentasPorVendedor",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@FechaInicio", SqlDbType.Date)
            .Value = fechaInicio.HasValue
                ? fechaInicio.Value.ToDateTime(TimeOnly.MinValue)
                : DBNull.Value;

        command.Parameters.Add("@FechaFin", SqlDbType.Date)
            .Value = fechaFin.HasValue
                ? fechaFin.Value.ToDateTime(TimeOnly.MinValue)
                : DBNull.Value;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            resultado.Add(new VentasPorVendedorDto
            {
                VendedorId = reader.GetInt32(reader.GetOrdinal("VendedorId")),
                CodigoEmpleado = reader.GetString(reader.GetOrdinal("CodigoEmpleado")),
                NombreCompleto = reader.GetString(reader.GetOrdinal("NombreCompleto")),
                CantidadCreditos = reader.IsDBNull(reader.GetOrdinal("CantidadCreditos"))
                    ? null
                    : reader.GetInt32(reader.GetOrdinal("CantidadCreditos")),
                TotalFinanciado = reader.IsDBNull(reader.GetOrdinal("TotalFinanciado"))
                    ? null
                    : reader.GetDecimal(reader.GetOrdinal("TotalFinanciado"))
            });
        }

        return resultado;
    }
    public async Task<IEnumerable<CumpleanerosMesDto>> ObtenerCumpleanerosMesAsync(int mes)
    {
        var resultado = new List<CumpleanerosMesDto>();

        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_Reporte_CumpleanerosMes",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@Mes", SqlDbType.Int)
            .Value = mes;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            resultado.Add(new CumpleanerosMesDto
            {
                ClienteId = reader.GetInt32(
                    reader.GetOrdinal("ClienteId")),

                NombreCompleto = reader.GetString(
                    reader.GetOrdinal("NombreCompleto")),

                FechaNacimiento = reader.IsDBNull(
                    reader.GetOrdinal("FechaNacimiento"))
                    ? null
                    : DateOnly.FromDateTime(
                        reader.GetDateTime(
                            reader.GetOrdinal("FechaNacimiento"))),

                CorreoElectronico = reader.GetString(
                    reader.GetOrdinal("CorreoElectronico"))
            });
        }

        return resultado;
    }
    public async Task<IEnumerable<EstadoCuentaClienteDto>> ObtenerEstadoCuentaClienteAsync(
    int clienteId)
    {
        var resultado = new List<EstadoCuentaClienteDto>();

        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_Reporte_EstadoCuentaCliente",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@ClienteId", SqlDbType.Int)
            .Value = clienteId;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            resultado.Add(new EstadoCuentaClienteDto
            {
                CreditoId = reader.GetInt32(
                    reader.GetOrdinal("CreditoId")),

                NumeroObligacion = reader.GetString(
                    reader.GetOrdinal("NumeroObligacion")),

                FechaAprobacion = DateOnly.FromDateTime(
                    reader.GetDateTime(
                        reader.GetOrdinal("FechaAprobacion"))),

                Estado = reader.GetString(
                    reader.GetOrdinal("Estado")),

                MontoNetoFinanciado = reader.GetDecimal(
                    reader.GetOrdinal("MontoNetoFinanciado")),

                MontoTotalNetoRecaudable = reader.GetDecimal(
                    reader.GetOrdinal("MontoTotalNetoRecaudable")),

                TotalPagado = reader.IsDBNull(
                    reader.GetOrdinal("TotalPagado"))
                    ? null
                    : reader.GetDecimal(
                        reader.GetOrdinal("TotalPagado")),

                Saldo = reader.IsDBNull(
                    reader.GetOrdinal("Saldo"))
                    ? null
                    : reader.GetDecimal(
                        reader.GetOrdinal("Saldo"))
            });
        }

        return resultado;
    }
    public async Task<IEnumerable<PlanPagosCreditoDto>> ObtenerPlanPagosCreditoAsync(
    int creditoId)
    {
        var resultado = new List<PlanPagosCreditoDto>();

        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_Reporte_PlanPagosCredito",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@CreditoId", SqlDbType.Int)
            .Value = creditoId;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            resultado.Add(new PlanPagosCreditoDto
            {
                NumeroCuota = reader.GetInt32(
                    reader.GetOrdinal("NumeroCuota")),

                FechaVencimiento = DateOnly.FromDateTime(
                    reader.GetDateTime(
                        reader.GetOrdinal("FechaVencimiento"))),

                MontoCapital = reader.GetDecimal(
                    reader.GetOrdinal("MontoCapital")),

                MontoInteres = reader.GetDecimal(
                    reader.GetOrdinal("MontoInteres")),

                MontoManejoCuenta = reader.GetDecimal(
                    reader.GetOrdinal("MontoManejoCuenta")),

                MontoSeguroVehiculo = reader.GetDecimal(
                    reader.GetOrdinal("MontoSeguroVehiculo")),

                MontoSeguroVida = reader.GetDecimal(
                    reader.GetOrdinal("MontoSeguroVida")),

                MontoTotal = reader.IsDBNull(
                    reader.GetOrdinal("MontoTotal"))
                    ? null
                    : reader.GetDecimal(
                        reader.GetOrdinal("MontoTotal")),

                Estado = reader.GetString(
                    reader.GetOrdinal("Estado")),

                Pagado = reader.IsDBNull(
                    reader.GetOrdinal("Pagado"))
                    ? null
                    : reader.GetDecimal(
                        reader.GetOrdinal("Pagado")),

                Saldo = reader.IsDBNull(
                    reader.GetOrdinal("Saldo"))
                    ? null
                    : reader.GetDecimal(
                        reader.GetOrdinal("Saldo"))
            });
        }

        return resultado;
    }
    public async Task<IEnumerable<MontoTotalFinanciadoDto>> ObtenerMontoTotalFinanciadoAsync(
    DateOnly? fechaInicio,
    DateOnly? fechaFin)
    {
        var resultado = new List<MontoTotalFinanciadoDto>();

        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_Reporte_MontoTotalFinanciado",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@FechaInicio", SqlDbType.Date)
            .Value = fechaInicio.HasValue
                ? fechaInicio.Value.ToDateTime(TimeOnly.MinValue)
                : DBNull.Value;

        command.Parameters.Add("@FechaFin", SqlDbType.Date)
            .Value = fechaFin.HasValue
                ? fechaFin.Value.ToDateTime(TimeOnly.MinValue)
                : DBNull.Value;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            resultado.Add(new MontoTotalFinanciadoDto
            {
                CantidadCreditos = reader.IsDBNull(
                    reader.GetOrdinal("CantidadCreditos"))
                    ? null
                    : reader.GetInt32(
                        reader.GetOrdinal("CantidadCreditos")),

                TotalFinanciado = reader.IsDBNull(
                    reader.GetOrdinal("TotalFinanciado"))
                    ? null
                    : reader.GetDecimal(
                        reader.GetOrdinal("TotalFinanciado")),

                InteresesGenerados = reader.IsDBNull(
                    reader.GetOrdinal("InteresesGenerados"))
                    ? null
                    : reader.GetDecimal(
                        reader.GetOrdinal("InteresesGenerados")),

                TotalRecaudable = reader.IsDBNull(
                    reader.GetOrdinal("TotalRecaudable"))
                    ? null
                    : reader.GetDecimal(
                        reader.GetOrdinal("TotalRecaudable"))
            });
        }

        return resultado;
    }
    public async Task<IEnumerable<ComisionesPorVendedorDto>> ObtenerComisionesPorVendedorAsync(
    DateOnly? fechaInicio,
    DateOnly? fechaFin)
    {
        var resultado = new List<ComisionesPorVendedorDto>();

        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_Reporte_ComisionesPorVendedor",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@FechaInicio", SqlDbType.Date)
            .Value = fechaInicio.HasValue
                ? fechaInicio.Value.ToDateTime(TimeOnly.MinValue)
                : DBNull.Value;

        command.Parameters.Add("@FechaFin", SqlDbType.Date)
            .Value = fechaFin.HasValue
                ? fechaFin.Value.ToDateTime(TimeOnly.MinValue)
                : DBNull.Value;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            resultado.Add(new ComisionesPorVendedorDto
            {
                VendedorId = reader.GetInt32(
                    reader.GetOrdinal("VendedorId")),

                NombreCompleto = reader.GetString(
                    reader.GetOrdinal("NombreCompleto")),

                CantidadComisiones = reader.IsDBNull(
                    reader.GetOrdinal("CantidadComisiones"))
                    ? null
                    : reader.GetInt32(
                        reader.GetOrdinal("CantidadComisiones")),

                TotalComisiones = reader.IsDBNull(
                    reader.GetOrdinal("TotalComisiones"))
                    ? null
                    : reader.GetDecimal(
                        reader.GetOrdinal("TotalComisiones"))
            });
        }

        return resultado;
    }
    public async Task<IEnumerable<SaldoPendienteCreditoDto>> ObtenerSaldoPendienteCreditoAsync(
    int creditoId)
    {
        var resultado = new List<SaldoPendienteCreditoDto>();

        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_Reporte_SaldoPendienteCredito",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@CreditoId", SqlDbType.Int)
            .Value = creditoId;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            resultado.Add(new SaldoPendienteCreditoDto
            {
                CreditoId = reader.GetInt32(
                    reader.GetOrdinal("CreditoId")),

                NumeroObligacion = reader.GetString(
                    reader.GetOrdinal("NumeroObligacion")),

                MontoTotalNetoRecaudable = reader.GetDecimal(
                    reader.GetOrdinal("MontoTotalNetoRecaudable")),

                TotalPagado = reader.IsDBNull(
                    reader.GetOrdinal("TotalPagado"))
                    ? null
                    : reader.GetDecimal(
                        reader.GetOrdinal("TotalPagado")),

                SaldoPendiente = reader.IsDBNull(
                    reader.GetOrdinal("SaldoPendiente"))
                    ? null
                    : reader.GetDecimal(
                        reader.GetOrdinal("SaldoPendiente"))
            });
        }

        return resultado;
    }
    public async Task<IEnumerable<GarantiasCreditoDto>> ObtenerGarantiasCreditoAsync(
    int creditoId)
    {
        var resultado = new List<GarantiasCreditoDto>();

        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_Reporte_GarantiasCredito",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@CreditoId", SqlDbType.Int)
            .Value = creditoId;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            resultado.Add(new GarantiasCreditoDto
            {
                GarantiaId = reader.GetInt32(
                    reader.GetOrdinal("GarantiaId")),

                TipoGarantia = reader.GetString(
                    reader.GetOrdinal("TipoGarantia")),

                Descripcion = reader.GetString(
                    reader.GetOrdinal("Descripcion")),

                ValorGarantia = reader.GetDecimal(
                    reader.GetOrdinal("ValorGarantia"))
            });
        }

        return resultado;
    }
    public async Task<IEnumerable<DepreciacionVehiculosDto>> ObtenerDepreciacionVehiculosAsync(
    int? vehiculoId)
    {
        var resultado = new List<DepreciacionVehiculosDto>();

        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_Reporte_DepreciacionVehiculos",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@VehiculoId", SqlDbType.Int)
            .Value = vehiculoId.HasValue
                ? vehiculoId.Value
                : DBNull.Value;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            resultado.Add(new DepreciacionVehiculosDto
            {
                VehiculoId = reader.GetInt32(
                    reader.GetOrdinal("VehiculoId")),

                Vin = reader.GetString(
                    reader.GetOrdinal("Vin")),

                Marca = reader.GetString(
                    reader.GetOrdinal("Marca")),

                Linea = reader.GetString(
                    reader.GetOrdinal("Linea")),

                FechaRegistro = DateOnly.FromDateTime(
                    reader.GetDateTime(
                        reader.GetOrdinal("FechaRegistro"))),

                PorcentajeDepreciacion = reader.GetDecimal(
                    reader.GetOrdinal("PorcentajeDepreciacion")),

                ValorAgencia = reader.GetDecimal(
                    reader.GetOrdinal("ValorAgencia")),

                ValorDepreciacion = reader.IsDBNull(
                    reader.GetOrdinal("ValorDepreciacion"))
                    ? null
                    : reader.GetDecimal(
                        reader.GetOrdinal("ValorDepreciacion")),

                ValorEstimado = reader.IsDBNull(
                    reader.GetOrdinal("ValorEstimado"))
                    ? null
                    : reader.GetDecimal(
                        reader.GetOrdinal("ValorEstimado"))
            });
        }

        return resultado;
    }
    public async Task<IEnumerable<TotalPagadoPorClienteDto>> ObtenerTotalPagadoPorClienteAsync(
    int? clienteId)
    {
        var resultado = new List<TotalPagadoPorClienteDto>();

        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_Reporte_TotalPagadoPorCliente",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@ClienteId", SqlDbType.Int)
            .Value = clienteId.HasValue
                ? clienteId.Value
                : DBNull.Value;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            resultado.Add(new TotalPagadoPorClienteDto
            {
                ClienteId = reader.GetInt32(
                    reader.GetOrdinal("ClienteId")),

                NombreCompleto = reader.GetString(
                    reader.GetOrdinal("NombreCompleto")),

                TotalPagado = reader.IsDBNull(
                    reader.GetOrdinal("TotalPagado"))
                    ? null
                    : reader.GetDecimal(
                        reader.GetOrdinal("TotalPagado"))
            });
        }

        return resultado;
    }
    public async Task<IEnumerable<IngresosPorMesDto>> ObtenerIngresosPorMesAsync(
    int anio)
    {
        var resultado = new List<IngresosPorMesDto>();

        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_Reporte_IngresosPorMes",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@Anio", SqlDbType.Int)
            .Value = anio;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            resultado.Add(new IngresosPorMesDto
            {
                Mes = reader.GetInt32(
                    reader.GetOrdinal("Mes")),

                CantidadPagos = reader.IsDBNull(
                    reader.GetOrdinal("CantidadPagos"))
                    ? null
                    : reader.GetInt32(
                        reader.GetOrdinal("CantidadPagos")),

                TotalIngresos = reader.IsDBNull(
                    reader.GetOrdinal("TotalIngresos"))
                    ? null
                    : reader.GetDecimal(
                        reader.GetOrdinal("TotalIngresos"))
            });
        }

        return resultado;
    }
    public async Task<IEnumerable<CarteraMoraDto>> ObtenerCarteraMoraAsync(
    int diasAtrasoMinimo)
    {
        var resultado = new List<CarteraMoraDto>();

        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_Reporte_CarteraMora",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@DiasAtrasoMinimo", SqlDbType.Int)
            .Value = diasAtrasoMinimo;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            resultado.Add(new CarteraMoraDto
            {
                NumeroObligacion = reader.GetString(
                    reader.GetOrdinal("NumeroObligacion")),

                Cliente = reader.GetString(
                    reader.GetOrdinal("Cliente")),

                NumeroCuota = reader.GetInt32(
                    reader.GetOrdinal("NumeroCuota")),

                FechaVencimiento = DateOnly.FromDateTime(
                    reader.GetDateTime(
                        reader.GetOrdinal("FechaVencimiento"))),

                DiasAtraso = reader.IsDBNull(
                    reader.GetOrdinal("DiasAtraso"))
                    ? null
                    : reader.GetInt32(
                        reader.GetOrdinal("DiasAtraso")),

                MontoTotal = reader.IsDBNull(
                    reader.GetOrdinal("MontoTotal"))
                    ? null
                    : reader.GetDecimal(
                        reader.GetOrdinal("MontoTotal")),

                Pagado = reader.IsDBNull(
                    reader.GetOrdinal("Pagado"))
                    ? null
                    : reader.GetDecimal(
                        reader.GetOrdinal("Pagado")),

                SaldoPendiente = reader.IsDBNull(
                    reader.GetOrdinal("SaldoPendiente"))
                    ? null
                    : reader.GetDecimal(
                        reader.GetOrdinal("SaldoPendiente"))
            });
        }

        return resultado;
    }
    public async Task<IEnumerable<ResumenFinancieroGeneralDto>> ObtenerResumenFinancieroGeneralAsync()
    {
        var resultado = new List<ResumenFinancieroGeneralDto>();

        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand(
            "sp_Reporte_ResumenFinancieroGeneral",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            resultado.Add(new ResumenFinancieroGeneralDto
            {
                TotalClientes = reader.IsDBNull(
                    reader.GetOrdinal("TotalClientes"))
                    ? null
                    : reader.GetInt32(
                        reader.GetOrdinal("TotalClientes")),

                TotalCreditos = reader.IsDBNull(
                    reader.GetOrdinal("TotalCreditos"))
                    ? null
                    : reader.GetInt32(
                        reader.GetOrdinal("TotalCreditos")),

                TotalFinanciado = reader.IsDBNull(
                    reader.GetOrdinal("TotalFinanciado"))
                    ? null
                    : reader.GetDecimal(
                        reader.GetOrdinal("TotalFinanciado")),

                TotalRecaudado = reader.IsDBNull(
                    reader.GetOrdinal("TotalRecaudado"))
                    ? null
                    : reader.GetDecimal(
                        reader.GetOrdinal("TotalRecaudado")),

                TotalComisiones = reader.IsDBNull(
                    reader.GetOrdinal("TotalComisiones"))
                    ? null
                    : reader.GetDecimal(
                        reader.GetOrdinal("TotalComisiones")),

                TotalVehiculos = reader.IsDBNull(
                    reader.GetOrdinal("TotalVehiculos"))
                    ? null
                    : reader.GetInt32(
                        reader.GetOrdinal("TotalVehiculos"))
            });
        }

        return resultado;
    }
}