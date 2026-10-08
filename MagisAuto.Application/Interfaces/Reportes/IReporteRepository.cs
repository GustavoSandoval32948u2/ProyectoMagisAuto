using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagisAuto.Application.DTOs.Reportes;

namespace MagisAuto.Application.Interfaces.Reportes;

public interface IReporteRepository
{
    Task<IEnumerable<CreditosPorEstadoDto>> ObtenerCreditosPorEstadoAsync();
    Task<IEnumerable<CuotasPorEstadoDto>> ObtenerCuotasPorEstadoAsync();
    Task<IEnumerable<VehiculosPorMarcaDto>> ObtenerVehiculosPorMarcaAsync();
    Task<IEnumerable<CreditosPorAgenciaDto>> ObtenerCreditosPorAgenciaAsync();
    Task<IEnumerable<CreditosPorRegionDto>> ObtenerCreditosPorRegionAsync();
    Task<IEnumerable<CreditosAprobadosPorFechaDto>> ObtenerCreditosAprobadosPorFechaAsync
        (DateOnly fechaInicio,DateOnly fechaFin);
    Task<IEnumerable<PagosPorFechaYMedioDto>> ObtenerPagosPorFechaYMedioAsync(
    DateOnly fechaInicio, DateOnly fechaFin, int? medioPagoId);
    Task<IEnumerable<VentasPorVendedorDto>> ObtenerVentasPorVendedorAsync(
    DateOnly? fechaInicio, DateOnly? fechaFin);
    Task<IEnumerable<CumpleanerosMesDto>> ObtenerCumpleanerosMesAsync(int mes);
    Task<IEnumerable<EstadoCuentaClienteDto>> ObtenerEstadoCuentaClienteAsync(
    int clienteId);
    Task<IEnumerable<PlanPagosCreditoDto>> ObtenerPlanPagosCreditoAsync(
    int creditoId);
    Task<IEnumerable<MontoTotalFinanciadoDto>> ObtenerMontoTotalFinanciadoAsync(
    DateOnly? fechaInicio, DateOnly? fechaFin);
    Task<IEnumerable<ComisionesPorVendedorDto>> ObtenerComisionesPorVendedorAsync(
    DateOnly? fechaInicio,
    DateOnly? fechaFin);
    Task<IEnumerable<SaldoPendienteCreditoDto>> ObtenerSaldoPendienteCreditoAsync(
    int creditoId);
    Task<IEnumerable<GarantiasCreditoDto>> ObtenerGarantiasCreditoAsync(
    int creditoId);
    Task<IEnumerable<DepreciacionVehiculosDto>> ObtenerDepreciacionVehiculosAsync(
    int? vehiculoId);
    Task<IEnumerable<TotalPagadoPorClienteDto>> ObtenerTotalPagadoPorClienteAsync(
    int? clienteId);
    Task<IEnumerable<IngresosPorMesDto>> ObtenerIngresosPorMesAsync(
    int anio);
    Task<IEnumerable<CarteraMoraDto>> ObtenerCarteraMoraAsync(
    int diasAtrasoMinimo);
    Task<IEnumerable<ResumenFinancieroGeneralDto>> ObtenerResumenFinancieroGeneralAsync();
}
