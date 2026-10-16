using FluentValidation;
using MagisAuto.Application.Interfaces.Agencia;
using MagisAuto.Application.Interfaces.Banco;
using MagisAuto.Application.Interfaces.Cliente;
using MagisAuto.Application.Interfaces.ComisionPago;
using MagisAuto.Application.Interfaces.Concesionaria;
using MagisAuto.Application.Interfaces.Credito;
using MagisAuto.Application.Interfaces.CreditoFiador;
using MagisAuto.Application.Interfaces.Cuota;
using MagisAuto.Application.Interfaces.Departamento;
using MagisAuto.Application.Interfaces.DireccionPersona;
using MagisAuto.Application.Interfaces.DireccionReferencia;
using MagisAuto.Application.Interfaces.EstadoCredito;
using MagisAuto.Application.Interfaces.EstadoCuota;
using MagisAuto.Application.Interfaces.Garantia;
using MagisAuto.Application.Interfaces.HistorialDepreciacion;
using MagisAuto.Application.Interfaces.LineaVehiculo;
using MagisAuto.Application.Interfaces.Marca;
using MagisAuto.Application.Interfaces.MedioPago;
using MagisAuto.Application.Interfaces.Municipio;
using MagisAuto.Application.Interfaces.NivelRiesgo;
using MagisAuto.Application.Interfaces.Pago;
using MagisAuto.Application.Interfaces.ParametroFinanciero;
using MagisAuto.Application.Interfaces.Parentesco;
using MagisAuto.Application.Interfaces.Persona;
using MagisAuto.Application.Interfaces.Referencia;
using MagisAuto.Application.Interfaces.Region;
using MagisAuto.Application.Interfaces.Reportes;
using MagisAuto.Application.Interfaces.TelefonoPersona;
using MagisAuto.Application.Interfaces.TelefonoReferencia;
using MagisAuto.Application.Interfaces.TipoGarantia;
using MagisAuto.Application.Interfaces.TipoTelefono;
using MagisAuto.Application.Interfaces.Vehiculo;
using MagisAuto.Application.Interfaces.Vendedor;
using MagisAuto.Application.Services;
using MagisAuto.Application.Validators.Cliente;
using MagisAuto.Infrastructure.Data;
using MagisAuto.Infrastructure.Repositories;
using MagisAutoAPI.Filters;
using MagisAutoAPI.Middleware;
using Microsoft.Data.SqlClient;

namespace MagisAutoAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddScoped<FluentValidationFilter>();

            builder.Services.AddControllers(options =>
            {
                options.Filters.AddService<FluentValidationFilter>();
            });

            builder.Services.AddScoped<SqlConnectionFactory>();
            builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
            builder.Services.AddScoped<IClienteService, ClienteService>();
            builder.Services.AddScoped<IMarcaRepository, MarcaRepository>();
            builder.Services.AddScoped<IMarcaService, MarcaService>();
            builder.Services.AddScoped<ITipoTelefonoRepository, TipoTelefonoRepository>();
            builder.Services.AddScoped<ITipoTelefonoService, TipoTelefonoService>();
            builder.Services.AddScoped<IVehiculoRepository, VehiculoRepository>();
            builder.Services.AddScoped<IVehiculoService, VehiculoService>();
            builder.Services.AddScoped<IRegionRepository, RegionRepository>();
            builder.Services.AddScoped<IRegionService, RegionService>();
            builder.Services.AddScoped<ILineaVehiculoRepository, LineaVehiculoRepository>();
            builder.Services.AddScoped<ILineaVehiculoService, LineaVehiculoService>();
            builder.Services.AddScoped<IAgenciaRepository, AgenciaRepository>();
            builder.Services.AddScoped<IAgenciaService, AgenciaService>();
            builder.Services.AddScoped<IDepartamentoRepository, DepartamentoRepository>();
            builder.Services.AddScoped<IDepartamentoService, DepartamentoService>();
            builder.Services.AddScoped<IMunicipioRepository, MunicipioRepository>();
            builder.Services.AddScoped<IMunicipioService, MunicipioService>();
            builder.Services.AddScoped<IParentescoRepository, ParentescoRepository>();
            builder.Services.AddScoped<IParentescoService, ParentescoService>();
            builder.Services.AddScoped<IEstadoCreditoRepository, EstadoCreditoRepository>();
            builder.Services.AddScoped<IEstadoCreditoService, EstadoCreditoService>();
            builder.Services.AddScoped<IEstadoCuotaRepository, EstadoCuotaRepository>();
            builder.Services.AddScoped<IEstadoCuotaService, EstadoCuotaService>();
            builder.Services.AddScoped<IMedioPagoRepository, MedioPagoRepository>();
            builder.Services.AddScoped<IMedioPagoService, MedioPagoService>();
            builder.Services.AddScoped<INivelRiesgoRepository, NivelRiesgoRepository>();
            builder.Services.AddScoped<INivelRiesgoService, NivelRiesgoService>();
            builder.Services.AddScoped<ITipoGarantiaRepository, TipoGarantiaRepository>();
            builder.Services.AddScoped<ITipoGarantiaService, TipoGarantiaService>();
            builder.Services.AddScoped<IBancoRepository, BancoRepository>();
            builder.Services.AddScoped<IBancoService, BancoService>();
            builder.Services.AddScoped<IParametroFinancieroRepository, ParametroFinancieroRepository>();
            builder.Services.AddScoped<IParametroFinancieroService, ParametroFinancieroService>();
            builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();
            builder.Services.AddScoped<IPersonaService, PersonaService>();
            builder.Services.AddScoped<ITelefonoPersonaRepository, TelefonoPersonaRepository>();
            builder.Services.AddScoped<ITelefonoPersonaService, TelefonoPersonaService>();
            builder.Services.AddScoped<IDireccionPersonaRepository, DireccionPersonaRepository>();
            builder.Services.AddScoped<IDireccionPersonaService, DireccionPersonaService>();
            builder.Services.AddScoped<IReferenciaRepository, ReferenciaRepository>();
            builder.Services.AddScoped<IReferenciaService, ReferenciaService>();
            builder.Services.AddScoped<ITelefonoReferenciaRepository, TelefonoReferenciaRepository>();
            builder.Services.AddScoped<ITelefonoReferenciaService, TelefonoReferenciaService>();
            builder.Services.AddScoped<IDireccionReferenciaRepository, DireccionReferenciaRepository>();
            builder.Services.AddScoped<IDireccionReferenciaService, DireccionReferenciaService>();
            builder.Services.AddScoped<IConcesionariaRepository, ConcesionariaRepository>();
            builder.Services.AddScoped<IConcesionariaService, ConcesionariaService>();
            builder.Services.AddScoped<IHistorialDepreciacionRepository, HistorialDepreciacionRepository>();
            builder.Services.AddScoped<IHistorialDepreciacionService, HistorialDepreciacionService>();
            builder.Services.AddScoped<IVendedorRepository, VendedorRepository>();
            builder.Services.AddScoped<IVendedorService, VendedorService>();
            builder.Services.AddScoped<ICreditoRepository, CreditoRepository>();
            builder.Services.AddScoped<ICreditoService, CreditoService>();
            builder.Services.AddScoped<ICreditoFiadorRepository, CreditoFiadorRepository>();
            builder.Services.AddScoped<ICreditoFiadorService, CreditoFiadorService>();
            builder.Services.AddScoped<IGarantiaRepository, GarantiaRepository>();
            builder.Services.AddScoped<IGarantiaService, GarantiaService>();
            builder.Services.AddScoped<ICuotaRepository, CuotaRepository>();
            builder.Services.AddScoped<ICuotaService, CuotaService>();
            builder.Services.AddScoped<IPagoRepository, PagoRepository>();
            builder.Services.AddScoped<IPagoService, PagoService>();
            builder.Services.AddScoped<IComisionPagoRepository, ComisionPagoRepository>();
            builder.Services.AddScoped<IComisionPagoService, ComisionPagoService>();
            builder.Services.AddScoped<IReporteRepository, ReporteRepository>();
            builder.Services.AddScoped<IReporteService, ReporteService>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddValidatorsFromAssemblyContaining<CrearClienteValidator>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MagisFrontend", policy =>
                {
                    policy
                        .WithOrigins("http://localhost:5173")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("MagisFrontend");

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
