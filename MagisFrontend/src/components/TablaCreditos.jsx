import { useCallback, useEffect, useState } from 'react';
import {
  obtenerCreditos,
  crearCredito,
  actualizarCredito,
  eliminarCredito,
} from '../services/creditosService';

const formularioInicial = {
  clienteId: '',
  vehiculoId: '',
  vendedorId: '',
  nivelRiesgoId: '',
  montoEnganche: '',
  porcentajeComision: '',
  fiadoresIds: '',
  plazoMeses: '',
  tasaInteresAnual: '',
  fechaAprobacion: '',
};

function TablaCreditos() {
  const [creditos, setCreditos] = useState([]);
  const [valores, setValores] = useState(formularioInicial);
  const [busqueda, setBusqueda] = useState('');
  const [editando, setEditando] = useState(null);
  const [estadoCreditoId, setEstadoCreditoId] = useState('');
  const [cargando, setCargando] = useState(true);
  const [guardando, setGuardando] = useState(false);
  const [error, setError] = useState('');
  const [mensaje, setMensaje] = useState('');

  const cargarCreditos = useCallback(async (criterio = '') => {
    setCargando(true);
    setError('');

    try {
      const datos = await obtenerCreditos(criterio);
      setCreditos(Array.isArray(datos) ? datos : []);
    } catch (err) {
      setError(`No se pudieron cargar los créditos: ${err.message}`);
    } finally {
      setCargando(false);
    }
  }, []);

  useEffect(() => {
    cargarCreditos();
  }, [cargarCreditos]);

  function cambiarCampo(e) {
    const { name, value } = e.target;
    setValores(prev => ({ ...prev, [name]: value }));
  }

  function limpiarFormulario() {
    setValores(formularioInicial);
    setEditando(null);
    setEstadoCreditoId('');
    setError('');
    setMensaje('');
  }

  function editar(credito) {
    setEditando(credito.creditoId);
    setEstadoCreditoId(String(credito.estadoCreditoId ?? ''));
    setError('');
    setMensaje('');
  }

  async function enviar(e) {
    e.preventDefault();
    setError('');
    setMensaje('');
    setGuardando(true);

    const datos = {
      clienteId: Number(valores.clienteId),
      vehiculoId: Number(valores.vehiculoId),
      vendedorId: Number(valores.vendedorId),
      nivelRiesgoId: Number(valores.nivelRiesgoId),
      montoEnganche: Number(valores.montoEnganche),
      porcentajeComision: Number(valores.porcentajeComision),
      fiadoresIds: valores.fiadoresIds.trim() || null,
      plazoMeses: valores.plazoMeses
        ? Number(valores.plazoMeses)
        : null,
      tasaInteresAnual: valores.tasaInteresAnual
        ? Number(valores.tasaInteresAnual)
        : null,
      fechaAprobacion: valores.fechaAprobacion || null,
    };

    try {
      await crearCredito(datos);
      setMensaje('Crédito registrado correctamente.');
      setValores(formularioInicial);
      await cargarCreditos(busqueda);
    } catch (err) {
      setError(`No se pudo registrar el crédito: ${err.message}`);
    } finally {
      setGuardando(false);
    }
  }

  async function guardarEstado(e) {
    e.preventDefault();

    if (estadoCreditoId.trim() === '') {
      setError('Debes indicar el ID del estado.');
      return;
    }

    setGuardando(true);
    setError('');
    setMensaje('');

    try {
      await actualizarCredito(editando, {
        estadoCreditoId: Number(estadoCreditoId),
      });

      setMensaje('Estado del crédito actualizado.');
      setEditando(null);
      setEstadoCreditoId('');
      await cargarCreditos(busqueda);
    } catch (err) {
      setError(`No se pudo actualizar el estado: ${err.message}`);
    } finally {
      setGuardando(false);
    }
  }

  async function borrar(credito) {
    if (!window.confirm(
      `¿Eliminar el crédito ${credito.numeroObligacion ?? credito.creditoId}?`
    )) return;

    setError('');
    setMensaje('');

    try {
      await eliminarCredito(credito.creditoId);
      setMensaje('Solicitud de eliminación completada.');
      await cargarCreditos(busqueda);
    } catch (err) {
      setError(`No se pudo eliminar el crédito: ${err.message}`);
    }
  }

  async function buscar(e) {
    e.preventDefault();
    await cargarCreditos(busqueda);
  }

  async function limpiarBusqueda() {
    setBusqueda('');
    await cargarCreditos('');
  }

  return (
    <section className="crud">
      <div className="crud-encabezado">
        <h2>Gestión de créditos</h2>
        <button type="button" onClick={limpiarFormulario}>
          Nuevo crédito
        </button>
      </div>

      {error && <p className="mensaje error">{error}</p>}
      {mensaje && <p className="mensaje exito">{mensaje}</p>}

      {editando !== null ? (
        <form className="formulario" onSubmit={guardarEstado}>
          <h3>Actualizar estado del crédito #{editando}</h3>

          <label>
            ID del nuevo estado
            <input
              type="number"
              min="1"
              value={estadoCreditoId}
              onChange={e => setEstadoCreditoId(e.target.value)}
              required
            />
          </label>

          <div className="acciones">
            <button type="submit" disabled={guardando}>
              Guardar estado
            </button>
            <button
              type="button"
              className="btn-secundario"
              onClick={limpiarFormulario}
            >
              Cancelar
            </button>
          </div>
        </form>
      ) : (
        <form className="formulario" onSubmit={enviar}>
          <h3>Registrar crédito</h3>

          <div className="campos-grid">
            <label>
              ID del cliente
              <input
                name="clienteId"
                type="number"
                min="1"
                value={valores.clienteId}
                onChange={cambiarCampo}
                required
              />
            </label>

            <label>
              ID del vehículo
              <input
                name="vehiculoId"
                type="number"
                min="1"
                value={valores.vehiculoId}
                onChange={cambiarCampo}
                required
              />
            </label>

            <label>
              ID del vendedor
              <input
                name="vendedorId"
                type="number"
                min="1"
                value={valores.vendedorId}
                onChange={cambiarCampo}
                required
              />
            </label>

            <label>
              ID del nivel de riesgo
              <input
                name="nivelRiesgoId"
                type="number"
                min="1"
                value={valores.nivelRiesgoId}
                onChange={cambiarCampo}
                required
              />
            </label>

            <label>
              Monto del enganche
              <input
                name="montoEnganche"
                type="number"
                min="0"
                step="0.01"
                value={valores.montoEnganche}
                onChange={cambiarCampo}
                required
              />
            </label>

            <label>
              Porcentaje de comisión
              <input
                name="porcentajeComision"
                type="number"
                min="0"
                step="0.01"
                value={valores.porcentajeComision}
                onChange={cambiarCampo}
                required
              />
            </label>

            <label>
              Plazo en meses
              <input
                name="plazoMeses"
                type="number"
                min="1"
                value={valores.plazoMeses}
                onChange={cambiarCampo}
              />
            </label>

            <label>
              Tasa de interés anual
              <input
                name="tasaInteresAnual"
                type="number"
                min="0"
                step="0.01"
                value={valores.tasaInteresAnual}
                onChange={cambiarCampo}
              />
            </label>

            <label>
              Fecha de aprobación
              <input
                name="fechaAprobacion"
                type="date"
                value={valores.fechaAprobacion}
                onChange={cambiarCampo}
              />
            </label>

            <label>
              IDs de fiadores
              <input
                name="fiadoresIds"
                value={valores.fiadoresIds}
                onChange={cambiarCampo}
                placeholder="Ejemplo: 3,7,12"
              />
            </label>
          </div>

          <div className="acciones">
            <button type="submit" disabled={guardando}>
              {guardando ? 'Guardando...' : 'Registrar crédito'}
            </button>

            <button
              type="button"
              className="btn-secundario"
              onClick={limpiarFormulario}
            >
              Limpiar
            </button>
          </div>
        </form>
      )}

      <form className="busqueda" onSubmit={buscar}>
        <input
          value={busqueda}
          onChange={e => setBusqueda(e.target.value)}
          placeholder="Buscar créditos..."
        />
        <button type="submit" disabled={cargando}>Buscar</button>
        <button
          type="button"
          className="btn-secundario"
          onClick={limpiarBusqueda}
          disabled={cargando}
        >
          Limpiar
        </button>
      </form>

      <p>Total de créditos: {creditos.length}</p>

      <div className="tabla-contenedor">
        <table>
          <thead>
            <tr>
              <th>ID</th>
              <th>Obligación</th>
              <th>Cliente</th>
              <th>Vehículo</th>
              <th>Vendedor</th>
              <th>Fecha</th>
              <th>Plazo</th>
              <th>Enganche</th>
              <th>Monto financiado</th>
              <th>Intereses</th>
              <th>Total recaudable</th>
              <th>Estado</th>
              <th>Acciones</th>
            </tr>
          </thead>

          <tbody>
            {cargando ? (
              <tr><td colSpan={13}>Cargando créditos...</td></tr>
            ) : creditos.length === 0 ? (
              <tr><td colSpan={13}>No hay créditos registrados.</td></tr>
            ) : (
              creditos.map(credito => (
                <tr key={credito.creditoId}>
                  <td>{credito.creditoId}</td>
                  <td>{credito.numeroObligacion ?? '—'}</td>
                  <td>{credito.clienteId}</td>
                  <td>{credito.vehiculoId}</td>
                  <td>{credito.vendedorId}</td>
                  <td>{credito.fechaAprobacion
                    ? String(credito.fechaAprobacion).slice(0, 10)
                    : '—'}</td>
                  <td>{credito.plazoMeses ?? '—'}</td>
                  <td>{credito.montoEnganche ?? '—'}</td>
                  <td>{credito.montoNetoFinanciado ?? '—'}</td>
                  <td>{credito.montoTotalIntereses ?? '—'}</td>
                  <td>{credito.montoTotalNetoRecaudable ?? '—'}</td>
                  <td>{credito.estadoCreditoId ?? '—'}</td>
                  <td className="acciones-tabla">
                    <button
                      type="button"
                      onClick={() => editar(credito)}
                    >
                      Estado
                    </button>
                    <button
                      type="button"
                      className="btn-peligro"
                      onClick={() => borrar(credito)}
                    >
                      Eliminar
                    </button>
                  </td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>
    </section>
  );
}

export default TablaCreditos;