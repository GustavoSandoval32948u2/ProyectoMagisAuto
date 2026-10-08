using MagisAuto.Application.Interfaces.Reportes;
using Microsoft.AspNetCore.Mvc;

namespace MagisAutoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportesController : ControllerBase
{
    private readonly IReporteService _reporteService;

    public ReportesController(IReporteService reporteService)
    {
        _reporteService = reporteService;
    }

    [HttpGet("creditos-por-estado")]
    public async Task<IActionResult> ObtenerCreditosPorEstado()
    {
        var resultado = await _reporteService.ObtenerCreditosPorEstadoAsync();

        return Ok(resultado);
    }

    [HttpGet("cuotas-por-estado")]
    public async Task<IActionResult> ObtenerCuotasPorEstado()
    {
        var resultado = await _reporteService.ObtenerCuotasPorEstadoAsync();

        return Ok(resultado);
    }

    [HttpGet("vehiculos-por-marca")]
    public async Task<IActionResult> ObtenerVehiculosPorMarca()
    {
        var resultado = await _reporteService.ObtenerVehiculosPorMarcaAsync();

        return Ok(resultado);
    }

    [HttpGet("creditos-por-agencia")]
    public async Task<IActionResult> ObtenerCreditosPorAgencia()
    {
        var resultado = await _reporteService.ObtenerCreditosPorAgenciaAsync();

        return Ok(resultado);
    }

    [HttpGet("creditos-por-region")]
    public async Task<IActionResult> ObtenerCreditosPorRegion()
    {
        var resultado = await _reporteService.ObtenerCreditosPorRegionAsync();

        return Ok(resultado);
    }

    [HttpGet("creditos-aprobados-por-fecha")]
    public async Task<IActionResult> ObtenerCreditosAprobadosPorFecha(
    [FromQuery] DateOnly fechaInicio,
    [FromQuery] DateOnly fechaFin)
    {
        var resultado = await _reporteService.ObtenerCreditosAprobadosPorFechaAsync(fechaInicio, fechaFin);

        return Ok(resultado);
    }

    [HttpGet("pagos-por-fecha-y-medio")]
    public async Task<IActionResult> ObtenerPagosPorFechaYMedio(
    [FromQuery] DateOnly fechaInicio,
    [FromQuery] DateOnly fechaFin,
    [FromQuery] int? medioPagoId)
    {
        var resultado = await _reporteService
            .ObtenerPagosPorFechaYMedioAsync(
                fechaInicio,
                fechaFin,
                medioPagoId);

        return Ok(resultado);
    }

    [HttpGet("ventas-por-vendedor")]
    public async Task<IActionResult> ObtenerVentasPorVendedor(
    [FromQuery] DateOnly? fechaInicio,
    [FromQuery] DateOnly? fechaFin)
    {
        var resultado = await _reporteService
            .ObtenerVentasPorVendedorAsync(fechaInicio, fechaFin);

        return Ok(resultado);
    }

    [HttpGet("cumpleaneros-mes")]
    public async Task<IActionResult> ObtenerCumpleanerosMes(
    [FromQuery] int mes)
    {
        var resultado = await _reporteService
            .ObtenerCumpleanerosMesAsync(mes);

        return Ok(resultado);
    }

    [HttpGet("estado-cuenta-cliente")]
    public async Task<IActionResult> ObtenerEstadoCuentaCliente(
    [FromQuery] int clienteId)
    {
        var resultado = await _reporteService
            .ObtenerEstadoCuentaClienteAsync(clienteId);

        return Ok(resultado);
    }

    [HttpGet("plan-pagos-credito")]
    public async Task<IActionResult> ObtenerPlanPagosCredito(
    [FromQuery] int creditoId)
    {
        var resultado = await _reporteService
            .ObtenerPlanPagosCreditoAsync(creditoId);

        return Ok(resultado);
    }

    [HttpGet("monto-total-financiado")]
    public async Task<IActionResult> ObtenerMontoTotalFinanciado(
    [FromQuery] DateOnly? fechaInicio,
    [FromQuery] DateOnly? fechaFin)
    {
        var resultado = await _reporteService
            .ObtenerMontoTotalFinanciadoAsync(
                fechaInicio,
                fechaFin);

        return Ok(resultado);
    }

    [HttpGet("comisiones-por-vendedor")]
    public async Task<IActionResult> ObtenerComisionesPorVendedor(
    [FromQuery] DateOnly? fechaInicio,
    [FromQuery] DateOnly? fechaFin)
    {
        var resultado = await _reporteService
            .ObtenerComisionesPorVendedorAsync(
                fechaInicio,
                fechaFin);

        return Ok(resultado);
    }

    [HttpGet("saldo-pendiente-credito")]
    public async Task<IActionResult> ObtenerSaldoPendienteCredito(
    [FromQuery] int creditoId)
    {
        var resultado = await _reporteService
            .ObtenerSaldoPendienteCreditoAsync(creditoId);

        return Ok(resultado);
    }

    [HttpGet("garantias-credito")]
    public async Task<IActionResult> ObtenerGarantiasCredito(
    [FromQuery] int creditoId)
    {
        var resultado = await _reporteService
            .ObtenerGarantiasCreditoAsync(creditoId);

        return Ok(resultado);
    }

    [HttpGet("depreciacion-vehiculos")]
    public async Task<IActionResult> ObtenerDepreciacionVehiculos(
    [FromQuery] int? vehiculoId)
    {
        var resultado = await _reporteService
            .ObtenerDepreciacionVehiculosAsync(vehiculoId);

        return Ok(resultado);
    }
    [HttpGet("total-pagado-por-cliente")]
    public async Task<IActionResult> ObtenerTotalPagadoPorCliente(
    [FromQuery] int? clienteId)
    {
        var resultado = await _reporteService
            .ObtenerTotalPagadoPorClienteAsync(clienteId);

        return Ok(resultado);
    }
    [HttpGet("ingresos-por-mes")]
    public async Task<IActionResult> ObtenerIngresosPorMes(
    [FromQuery] int anio)
    {
        var resultado = await _reporteService
            .ObtenerIngresosPorMesAsync(anio);

        return Ok(resultado);
    }

    [HttpGet("cartera-mora")]
    public async Task<IActionResult> ObtenerCarteraMora(
    [FromQuery] int diasAtrasoMinimo = 1)
    {
        var resultado = await _reporteService
            .ObtenerCarteraMoraAsync(diasAtrasoMinimo);

        return Ok(resultado);
    }

    [HttpGet("resumen-financiero-general")]
    public async Task<IActionResult> ObtenerResumenFinancieroGeneral()
    {
        var resultado = await _reporteService
            .ObtenerResumenFinancieroGeneralAsync();

        return Ok(resultado);
    }
}