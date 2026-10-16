
import { useState } from 'react';
import './App.css';

import Header from './components/Header';
import Menu from './components/Menu';
import Dashboard from './components/Dashboard';
import TablaClientes from './components/TablaClientes';
import CrudCatalogo from './components/CrudCatalogo';
import TablaCreditos from './components/TablaCreditos';
import Reportes from './components/Reportes';
import TablaVendedores from './components/TablaVendedores';
import TablaCuotas from './components/TablaCuotas';
import TablaPagos from './components/TablaPagos';
import FiadoresGarantias from './components/FiadoresGarantias';

function App() {
  const [seccion, setSeccion] = useState('Inicio');

  return (
    <>
      <Header
        title="MAGIS-AUTO"
        subtitle="Sistema de gestión de vehículos y créditos"
      />

      <Menu
        cambiarSeccion={setSeccion}
        seccionActual={seccion}
      />

      <main className="contenido-principal">
        <div key={seccion} className="vista-modulo">

          {/* PANEL PRINCIPAL */}
          {seccion === 'Inicio' && (
            <Dashboard cambiarSeccion={setSeccion} />
          )}

          {/* CLIENTES */}
          {seccion === 'Clientes' && <TablaClientes />}

          {/* CATÁLOGOS DE VEHÍCULOS */}
          {seccion === 'Marcas' && (
            <CrudCatalogo
              titulo="Marcas"
              endpoint="/api/Marca"
              idCampo="marcaId"
              campos={[
                { nombre: 'nombre', etiqueta: 'Nombre' },
                { nombre: 'codigoInterno', etiqueta: 'Código interno' },
              ]}
            />
          )}

          {seccion === 'Líneas' && (
            <CrudCatalogo
              titulo="Líneas de vehículos"
              endpoint="/api/LineaVehiculo"
              idCampo="lineaVehiculoId"
              campos={[
                { nombre: 'marcaId', etiqueta: 'ID de marca', tipo: 'number' },
                { nombre: 'nombre', etiqueta: 'Nombre de la línea' },
              ]}
            />
          )}

          {seccion === 'Concesionarias' && (
            <CrudCatalogo
              titulo="Concesionarias"
              endpoint="/api/Concesionaria"
              idCampo="concesionariaId"
              campos={[
                { nombre: 'nombre', etiqueta: 'Nombre' },
                { nombre: 'nit', etiqueta: 'NIT', requerido: false },
              ]}
            />
          )}

          {seccion === 'Vehículos' && (
            <CrudCatalogo
              titulo="Vehículos"
              endpoint="/api/Vehiculo"
              idCampo="vehiculoId"
              campos={[
                { nombre: 'lineaVehiculoId', etiqueta: 'ID de línea', tipo: 'number' },
                { nombre: 'concesionariaId', etiqueta: 'ID de concesionaria', tipo: 'number' },
                { nombre: 'vin', etiqueta: 'VIN' },
                { nombre: 'anioFabricacion', etiqueta: 'Año de fabricación', tipo: 'number' },
                { nombre: 'valorAgencia', etiqueta: 'Valor de agencia', tipo: 'number' },
                { nombre: 'placa', etiqueta: 'Placa', requerido: false },
              ]}
            />
          )}

          {/* CRÉDITOS Y OPERACIONES */}
          {seccion === 'Créditos' && <TablaCreditos />}
          {seccion === 'Reportes' && <Reportes />}
          {seccion === 'Vendedores' && <TablaVendedores />}
          {seccion === 'Cuotas' && <TablaCuotas />}
          {seccion === 'Pagos' && <TablaPagos />}
          {seccion === 'Fiadores y Garantías' && <FiadoresGarantias />}

          {/* AGENCIAS */}
          {seccion === 'Agencias' && (
            <CrudCatalogo
              titulo="Agencias"
              endpoint="/api/Agencia"
              idCampo="agenciaId"
              campos={[
                { nombre: 'nombre', etiqueta: 'Nombre', tipo: 'text' },
                { nombre: 'regionId', etiqueta: 'ID de región', tipo: 'number' },
                { nombre: 'municipioId', etiqueta: 'ID de municipio', tipo: 'number' },
                { nombre: 'direccion', etiqueta: 'Dirección', tipo: 'text' },
              ]}
            />
          )}

          {/* BANCOS Y MEDIOS DE PAGO */}
          {seccion === 'Bancos' && (
            <CrudCatalogo
              titulo="Bancos"
              endpoint="/api/Banco"
              idCampo="bancoId"
              campos={[
                { nombre: 'nombre', etiqueta: 'Nombre', tipo: 'text' },
              ]}
            />
          )}

          {seccion === 'Medios de Pago' && (
            <CrudCatalogo
              titulo="Medios de Pago"
              endpoint="/api/MedioPago"
              idCampo="medioPagoId"
              campos={[
                { nombre: 'nombre', etiqueta: 'Nombre', tipo: 'text' },
                {
                  nombre: 'requiereReferencia',
                  etiqueta: '¿Requiere referencia?',
                  tipo: 'checkbox',
                },
              ]}
            />
          )}

          {/* ESTADOS Y RIESGOS */}
          {seccion === 'Estados de Crédito' && (
            <CrudCatalogo
              titulo="Estados de Crédito"
              endpoint="/api/EstadoCredito"
              idCampo="estadoCreditoId"
              campos={[
                { nombre: 'nombre', etiqueta: 'Nombre', tipo: 'text' },
              ]}
            />
          )}

          {seccion === 'Estados de Cuota' && (
            <CrudCatalogo
              titulo="Estados de Cuota"
              endpoint="/api/EstadoCuota"
              idCampo="estadoCuotaId"
              campos={[
                { nombre: 'nombre', etiqueta: 'Nombre', tipo: 'text' },
              ]}
            />
          )}

          {seccion === 'Niveles de Riesgo' && (
            <CrudCatalogo
              titulo="Niveles de Riesgo"
              endpoint="/api/NivelRiesgo"
              idCampo="nivelRiesgoId"
              campos={[
                { nombre: 'nombre', etiqueta: 'Nombre', tipo: 'text' },
                {
                  nombre: 'requiereFiador',
                  etiqueta: '¿Requiere fiador?',
                  tipo: 'checkbox',
                },
              ]}
            />
          )}

          {seccion === 'Tipos de Garantía' && (
            <CrudCatalogo
              titulo="Tipos de Garantía"
              endpoint="/api/TipoGarantia"
              idCampo="tipoGarantiaId"
              campos={[
                { nombre: 'nombre', etiqueta: 'Nombre', tipo: 'text' },
                { nombre: 'descripcion', etiqueta: 'Descripción', tipo: 'text' },
              ]}
            />
          )}

          {/* UBICACIONES GEOGRÁFICAS */}
          {seccion === 'Regiones' && (
            <CrudCatalogo
              titulo="Regiones"
              endpoint="/api/Region"
              idCampo="regionId"
              campos={[
                { nombre: 'nombre', etiqueta: 'Nombre', tipo: 'text' },
              ]}
            />
          )}

          {seccion === 'Departamentos' && (
            <CrudCatalogo
              titulo="Departamentos"
              endpoint="/api/Departamento"
              idCampo="departamentoId"
              campos={[
                { nombre: 'nombre', etiqueta: 'Nombre', tipo: 'text' },
              ]}
            />
          )}

          {seccion === 'Municipios' && (
            <CrudCatalogo
              titulo="Municipios"
              endpoint="/api/Municipio"
              idCampo="municipioId"
              campos={[
                {
                  nombre: 'departamentoId',
                  etiqueta: 'ID del departamento',
                  tipo: 'number',
                },
                { nombre: 'nombre', etiqueta: 'Nombre', tipo: 'text' },
              ]}
            />
          )}

          {/* PERSONAS */}
          {seccion === 'Personas' && (
            <CrudCatalogo
              titulo="Personas"
              endpoint="/api/Persona"
              idCampo="personaId"
              campos={[
                { nombre: 'nit', etiqueta: 'NIT', tipo: 'text' },
                { nombre: 'dpi', etiqueta: 'DPI', tipo: 'text' },
                { nombre: 'nombreCompleto', etiqueta: 'Nombre completo', tipo: 'text' },
                { nombre: 'correoElectronico', etiqueta: 'Correo electrónico', tipo: 'email' },
                {
                  nombre: 'fechaNacimiento',
                  etiqueta: 'Fecha de nacimiento',
                  tipo: 'date',
                  requerido: false,
                },
              ]}
            />
          )}

          {/* TELÉFONOS */}
          {seccion === 'Teléfonos de Persona' && (
            <CrudCatalogo
              titulo="Teléfonos de Persona"
              endpoint="/api/TelefonoPersona"
              idCampo="telefonoPersonaId"
              campos={[
                { nombre: 'personaId', etiqueta: 'ID de persona', tipo: 'number' },
                { nombre: 'tipoTelefonoId', etiqueta: 'ID de tipo de teléfono', tipo: 'number' },
                { nombre: 'numeroTelefono', etiqueta: 'Número de teléfono', tipo: 'text' },
              ]}
            />
          )}

          {seccion === 'Teléfonos de Referencia' && (
            <CrudCatalogo
              titulo="Teléfonos de Referencia"
              endpoint="/api/TelefonoReferencia"
              idCampo="telefonoReferenciaId"
              campos={[
                { nombre: 'referenciaId', etiqueta: 'ID de referencia', tipo: 'number' },
                { nombre: 'tipoTelefonoId', etiqueta: 'ID de tipo de teléfono', tipo: 'number' },
                { nombre: 'numeroTelefono', etiqueta: 'Número de teléfono', tipo: 'text' },
              ]}
            />
          )}

          {/* DIRECCIONES */}
          {seccion === 'Direcciones de Persona' && (
            <CrudCatalogo
              titulo="Direcciones de Persona"
              endpoint="/api/DireccionPersona"
              idCampo="direccionPersonaId"
              campos={[
                { nombre: 'personaId', etiqueta: 'ID de persona', tipo: 'number' },
                { nombre: 'municipioId', etiqueta: 'ID de municipio', tipo: 'number' },
                { nombre: 'zona', etiqueta: 'Zona', tipo: 'text', requerido: false },
                { nombre: 'direccion', etiqueta: 'Dirección', tipo: 'text' },
              ]}
            />
          )}

          {seccion === 'Direcciones de Referencia' && (
            <CrudCatalogo
              titulo="Direcciones de Referencia"
              endpoint="/api/DireccionReferencia"
              idCampo="direccionReferenciaId"
              campos={[
                { nombre: 'referenciaId', etiqueta: 'ID de referencia', tipo: 'number' },
                { nombre: 'municipioId', etiqueta: 'ID de municipio', tipo: 'number' },
                { nombre: 'zona', etiqueta: 'Zona', tipo: 'text', requerido: false },
                { nombre: 'direccion', etiqueta: 'Dirección', tipo: 'text' },
              ]}
            />
          )}

          {/* REFERENCIAS Y PARENTESCOS */}
          {seccion === 'Referencias' && (
            <CrudCatalogo
              titulo="Referencias personales"
              endpoint="/api/Referencia"
              idCampo="referenciaId"
              campos={[
                { nombre: 'clienteId', etiqueta: 'ID de cliente', tipo: 'number' },
                { nombre: 'parentescoId', etiqueta: 'ID de parentesco', tipo: 'number' },
                { nombre: 'lugarTrabajo', etiqueta: 'Lugar de trabajo', tipo: 'text' },
                { nombre: 'nombreCompleto', etiqueta: 'Nombre completo', tipo: 'text' },
              ]}
            />
          )}

          {seccion === 'Parentescos' && (
            <CrudCatalogo
              titulo="Parentescos"
              endpoint="/api/Parentesco"
              idCampo="parentescoId"
              campos={[
                { nombre: 'nombre', etiqueta: 'Nombre', tipo: 'text' },
              ]}
            />
          )}

          {seccion === 'Tipos de Teléfono' && (
            <CrudCatalogo
              titulo="Tipos de Teléfono"
              endpoint="/api/TipoTelefono"
              idCampo="tipoTelefonoId"
              campos={[
                { nombre: 'nombre', etiqueta: 'Nombre', tipo: 'text' },
              ]}
            />
          )}

          {/* HISTORIAL DE DEPRECIACIÓN */}
          {seccion === 'Historial de Depreciación' && (
            <CrudCatalogo
              titulo="Historial de Depreciación"
              endpoint="/api/HistorialDepreciacion"
              idCampo="historialDepreciacionId"
              campos={[
                { nombre: 'vehiculoId', etiqueta: 'ID de vehículo', tipo: 'number' },
                { nombre: 'fechaRegistro', etiqueta: 'Fecha de registro', tipo: 'date' },
                {
                  nombre: 'porcentajeDepreciacion',
                  etiqueta: 'Porcentaje de depreciación',
                  tipo: 'number',
                },
              ]}
            />
          )}

          {/* CONFIGURACIÓN FINANCIERA */}
          {seccion === 'Parámetros Financieros' && (
            <CrudCatalogo
              titulo="Parámetros Financieros"
              endpoint="/api/ParametroFinanciero"
              idCampo="parametroFinancieroId"
              campos={[
                { nombre: 'nombre', etiqueta: 'Nombre', tipo: 'text' },
                { nombre: 'valor', etiqueta: 'Valor', tipo: 'number' },
              ]}
            />
          )}

          {seccion === 'Comisiones de Pago' && (
            <CrudCatalogo
              titulo="Comisiones de Pago"
              endpoint="/api/ComisionPago"
              idCampo="comisionPagoId"
              campos={[
                { nombre: 'pagoId', etiqueta: 'ID de pago', tipo: 'number' },
                { nombre: 'montoComision', etiqueta: 'Monto de comisión', tipo: 'number' },
                { nombre: 'fechaGeneracion', etiqueta: 'Fecha de generación', tipo: 'date' },
              ]}
            />
          )}

        </div>
      </main>
    </>
  );
}

export default App;
