using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagisAuto.Application.DTOs.Reportes;
using MagisAuto.Application.Interfaces.Reportes;

namespace MagisAuto.Application.Services;

public class ReporteService : IReporteService
{
    private readonly IReporteRepository _reporteRepository;

    public ReporteService(IReporteRepository reporteRepository)
    {
        _reporteRepository = reporteRepository;
    }

    public async Task<IEnumerable<CreditosPorEstadoDto>> ObtenerCreditosPorEstadoAsync()
    {
        return await _reporteRepository.ObtenerCreditosPorEstadoAsync();
    }
    public async Task<IEnumerable<CuotasPorEstadoDto>> ObtenerCuotasPorEstadoAsync()
    {
        return await _reporteRepository.ObtenerCuotasPorEstadoAsync();
    }
    public async Task<IEnumerable<VehiculosPorMarcaDto>> ObtenerVehiculosPorMarcaAsync()
    {
        return await _reporteRepository.ObtenerVehiculosPorMarcaAsync();
    }
    public async Task<IEnumerable<CreditosPorAgenciaDto>> ObtenerCreditosPorAgenciaAsync()
    {
        return await _reporteRepository.ObtenerCreditosPorAgenciaAsync();
    }
    public async Task<IEnumerable<CreditosPorRegionDto>> ObtenerCreditosPorRegionAsync()
    {
        return await _reporteRepository.ObtenerCreditosPorRegionAsync();
    }
    public async Task<IEnumerable<CreditosAprobadosPorFechaDto>> ObtenerCreditosAprobadosPorFechaAsync(
    DateOnly fechaInicio,
    DateOnly fechaFin)
    {
        return await _reporteRepository.ObtenerCreditosAprobadosPorFechaAsync(
            fechaInicio,
            fechaFin);
    }
    public async Task<IEnumerable<PagosPorFechaYMedioDto>> ObtenerPagosPorFechaYMedioAsync(
    DateOnly fechaInicio,
    DateOnly fechaFin,
    int? medioPagoId)
    {
        return await _reporteRepository.ObtenerPagosPorFechaYMedioAsync(
            fechaInicio,
            fechaFin,
            medioPagoId);
    }
    public async Task<IEnumerable<VentasPorVendedorDto>> ObtenerVentasPorVendedorAsync(
    DateOnly? fechaInicio,
    DateOnly? fechaFin)
    {
        return await _reporteRepository
            .ObtenerVentasPorVendedorAsync(fechaInicio, fechaFin);
    }
    public async Task<IEnumerable<CumpleanerosMesDto>> ObtenerCumpleanerosMesAsync(int mes)
    {
        return await _reporteRepository.ObtenerCumpleanerosMesAsync(mes);
    }
    public async Task<IEnumerable<EstadoCuentaClienteDto>> ObtenerEstadoCuentaClienteAsync(
    int clienteId)
    {
        return await _reporteRepository
            .ObtenerEstadoCuentaClienteAsync(clienteId);
    }
    public async Task<IEnumerable<PlanPagosCreditoDto>> ObtenerPlanPagosCreditoAsync(
    int creditoId)
    {
        return await _reporteRepository
            .ObtenerPlanPagosCreditoAsync(creditoId);
    }
    public async Task<IEnumerable<MontoTotalFinanciadoDto>> ObtenerMontoTotalFinanciadoAsync(
    DateOnly? fechaInicio,
    DateOnly? fechaFin)
    {
        return await _reporteRepository
            .ObtenerMontoTotalFinanciadoAsync(fechaInicio, fechaFin);
    }
    public async Task<IEnumerable<ComisionesPorVendedorDto>> ObtenerComisionesPorVendedorAsync(
    DateOnly? fechaInicio,
    DateOnly? fechaFin)
    {
        return await _reporteRepository
            .ObtenerComisionesPorVendedorAsync(fechaInicio, fechaFin);
    }
    public async Task<IEnumerable<SaldoPendienteCreditoDto>> ObtenerSaldoPendienteCreditoAsync(
    int creditoId)
    {
        return await _reporteRepository
            .ObtenerSaldoPendienteCreditoAsync(creditoId);
    }
    public async Task<IEnumerable<GarantiasCreditoDto>> ObtenerGarantiasCreditoAsync(
    int creditoId)
    {
        return await _reporteRepository
            .ObtenerGarantiasCreditoAsync(creditoId);
    }
    public async Task<IEnumerable<DepreciacionVehiculosDto>> ObtenerDepreciacionVehiculosAsync(
    int? vehiculoId)
    {
        return await _reporteRepository
            .ObtenerDepreciacionVehiculosAsync(vehiculoId);
    }
    public async Task<IEnumerable<TotalPagadoPorClienteDto>> ObtenerTotalPagadoPorClienteAsync(
    int? clienteId)
    {
        return await _reporteRepository
            .ObtenerTotalPagadoPorClienteAsync(clienteId);
    }
    public async Task<IEnumerable<IngresosPorMesDto>> ObtenerIngresosPorMesAsync(
    int anio)
    {
        return await _reporteRepository
            .ObtenerIngresosPorMesAsync(anio);
    }
    public async Task<IEnumerable<CarteraMoraDto>> ObtenerCarteraMoraAsync(
    int diasAtrasoMinimo)
    {
        return await _reporteRepository
            .ObtenerCarteraMoraAsync(diasAtrasoMinimo);
    }
    public async Task<IEnumerable<ResumenFinancieroGeneralDto>> ObtenerResumenFinancieroGeneralAsync()
    {
        return await _reporteRepository
            .ObtenerResumenFinancieroGeneralAsync();
    }
}