import { useCallback, useEffect, useState } from 'react';
import {
  obtenerClientes,
  crearCliente,
  actualizarCliente,
  eliminarCliente,
} from '../services/clientesService';

const formularioInicial = {
  nit: '',
  dpi: '',
  nombreCompleto: '',
  correoElectronico: '',
  fechaNacimiento: '',
};

function TablaClientes() {
  const [clientes, setClientes] = useState([]);
  const [valores, setValores] = useState(formularioInicial);
  const [editando, setEditando] = useState(null);
  const [busqueda, setBusqueda] = useState('');
  const [cargando, setCargando] = useState(true);
  const [guardando, setGuardando] = useState(false);
  const [error, setError] = useState('');
  const [mensaje, setMensaje] = useState('');

  const cargarClientes = useCallback(async (criterio = '') => {
    setCargando(true);
    setError('');

    try {
      const datos = await obtenerClientes(criterio);
      setClientes(Array.isArray(datos) ? datos : []);
    } catch (err) {
      setError(`No se pudieron cargar los clientes: ${err.message}`);
    } finally {
      setCargando(false);
    }
  }, []);

  useEffect(() => {
    cargarClientes();
  }, [cargarClientes]);

  function limpiarFormulario() {
    setValores(formularioInicial);
    setEditando(null);
    setError('');
    setMensaje('');
  }

  function cambiarCampo(e) {
    const { name, value } = e.target;

    setValores(prev => ({
      ...prev,
      [name]: value,
    }));
  }

  function editar(cliente) {
    setValores({
      nit: cliente.nit ?? '',
      dpi: cliente.dpi ?? '',
      nombreCompleto: cliente.nombreCompleto ?? '',
      correoElectronico: cliente.correoElectronico ?? '',
      fechaNacimiento: cliente.fechaNacimiento
        ? String(cliente.fechaNacimiento).slice(0, 10)
        : '',
    });

    setEditando(cliente.clienteId);
    setError('');
    setMensaje('');
  }

  async function enviar(e) {
    e.preventDefault();
    setError('');
    setMensaje('');

    const cuerpo = {
      nit: valores.nit.trim(),
      dpi: valores.dpi.trim(),
      nombreCompleto: valores.nombreCompleto.trim(),
      correoElectronico: valores.correoElectronico.trim(),
      fechaNacimiento: valores.fechaNacimiento || null,
    };

    setGuardando(true);

    try {
      if (editando !== null) {
        await actualizarCliente(editando, cuerpo);
        setMensaje('Cliente actualizado correctamente.');
      } else {
        await crearCliente(cuerpo);
        setMensaje('Cliente creado correctamente.');
      }

      setValores(formularioInicial);
      setEditando(null);
      await cargarClientes(busqueda);
    } catch (err) {
      setError(`No se pudo guardar el cliente: ${err.message}`);
    } finally {
      setGuardando(false);
    }
  }

  async function borrar(cliente) {
    if (!window.confirm(
      `¿Seguro que deseas eliminar al cliente ${cliente.nombreCompleto}?`
    )) {
      return;
    }

    setError('');
    setMensaje('');

    try {
      await eliminarCliente(cliente.clienteId);
      setMensaje('Cliente eliminado correctamente.');
      await cargarClientes(busqueda);
    } catch (err) {
      setError(`No se pudo eliminar el cliente: ${err.message}`);
    }
  }

  async function buscar(e) {
    e.preventDefault();
    await cargarClientes(busqueda);
  }

  async function limpiarBusqueda() {
    setBusqueda('');
    await cargarClientes('');
  }

  return (
    <section className="crud">
      <div className="crud-encabezado">
        <h2>Clientes</h2>

        <button type="button" onClick={limpiarFormulario}>
          Nuevo cliente
        </button>
      </div>

      {error && <p className="mensaje error">{error}</p>}
      {mensaje && <p className="mensaje exito">{mensaje}</p>}

      <form className="formulario" onSubmit={enviar}>
        <h3>{editando !== null ? 'Editar cliente' : 'Registrar cliente'}</h3>

        <div className="campos-grid">
          <label>
            NIT
            <input
              name="nit"
              value={valores.nit}
              onChange={cambiarCampo}
              maxLength={20}
              required
            />
          </label>

          <label>
            DPI
            <input
              name="dpi"
              value={valores.dpi}
              onChange={cambiarCampo}
              maxLength={20}
              required
            />
          </label>

          <label>
            Nombre completo
            <input
              name="nombreCompleto"
              value={valores.nombreCompleto}
              onChange={cambiarCampo}
              maxLength={150}
              required
            />
          </label>

          <label>
            Correo electrónico
            <input
              type="email"
              name="correoElectronico"
              value={valores.correoElectronico}
              onChange={cambiarCampo}
              maxLength={150}
              required
            />
          </label>

          <label>
            Fecha de nacimiento
            <input
              type="date"
              name="fechaNacimiento"
              value={valores.fechaNacimiento}
              onChange={cambiarCampo}
              max={new Date().toLocaleDateString('en-CA')}
            />
          </label>
        </div>

        <div className="acciones">
          <button type="submit" disabled={guardando}>
            {guardando
              ? 'Guardando...'
              : editando !== null
                ? 'Guardar cambios'
                : 'Registrar cliente'}
          </button>

          <button
            type="button"
            className="btn-secundario"
            onClick={limpiarFormulario}
          >
            Cancelar / Limpiar
          </button>
        </div>
      </form>

      <form className="busqueda" onSubmit={buscar}>
        <input
          value={busqueda}
          onChange={e => setBusqueda(e.target.value)}
          placeholder="Buscar clientes..."
        />

        <button type="submit" disabled={cargando}>
          Buscar
        </button>

        <button
          type="button"
          className="btn-secundario"
          onClick={limpiarBusqueda}
          disabled={cargando}
        >
          Limpiar
        </button>
      </form>

      <p>Total de clientes: {clientes.length}</p>

      <div className="tabla-contenedor">
        <table>
          <thead>
            <tr>
              <th>ID</th>
              <th>NIT</th>
              <th>DPI</th>
              <th>Nombre completo</th>
              <th>Correo electrónico</th>
              <th>Fecha de nacimiento</th>
              <th>Acciones</th>
            </tr>
          </thead>

          <tbody>
            {cargando ? (
              <tr>
                <td colSpan={7}>Cargando clientes...</td>
              </tr>
            ) : clientes.length === 0 ? (
              <tr>
                <td colSpan={7}>No hay clientes registrados.</td>
              </tr>
            ) : (
              clientes.map(cliente => (
                <tr key={cliente.clienteId}>
                  <td>{cliente.clienteId}</td>
                  <td>{cliente.nit ?? '—'}</td>
                  <td>{cliente.dpi ?? '—'}</td>
                  <td>{cliente.nombreCompleto ?? '—'}</td>
                  <td>{cliente.correoElectronico ?? '—'}</td>
                  <td>
                    {cliente.fechaNacimiento
                      ? new Date(
                          `${String(cliente.fechaNacimiento).slice(0, 10)}T12:00:00`
                        ).toLocaleDateString('es-GT')
                      : '—'}
                  </td>
                  <td className="acciones-tabla">
                    <button
                      type="button"
                      onClick={() => editar(cliente)}
                    >
                      Editar
                    </button>

                    <button
                      type="button"
                      className="btn-peligro"
                      onClick={() => borrar(cliente)}
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

export default TablaClientes;
