using MagisAuto.Application.DTOs.Cliente;
using MagisAuto.Application.Interfaces.Cliente;
using MagisAuto.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MagisAutoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDto>>> ObtenerTodos()
        {
            var clientes = await _clienteService.ObtenerTodosAsync();

            return Ok(clientes);
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<ClienteDto>>> Buscar(
            [FromQuery] string? criterio)
        {
            var clientes = await _clienteService.BuscarAsync(criterio);

            return Ok(clientes);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Crear(CrearClienteDto cliente)
        {
            var clienteId = await _clienteService.CrearAsync(cliente);

            return Ok(clienteId);
        }

        [HttpPut("{clienteId:int}")]
        public async Task<IActionResult> Actualizar(
            int clienteId,
            ActualizarClienteDto cliente)
        {
            var actualizado = await _clienteService.ActualizarAsync(
                clienteId,
                cliente);

            if (!actualizado)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{clienteId:int}")]
        public async Task<IActionResult> Eliminar(int clienteId)
        {
            var eliminado = await _clienteService.EliminarAsync(clienteId);

            if (!eliminado)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
