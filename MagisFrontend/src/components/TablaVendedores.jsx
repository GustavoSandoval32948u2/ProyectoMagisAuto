
import { useCallback, useEffect, useState } from 'react';
import {
  obtenerVendedores,
  crearVendedor,
  actualizarVendedor,
  eliminarVendedor,
} from '../services/vendedoresService';

const formularioInicial = {
  codigoEmpleado: '',
  dpi: '',
  nombreCompleto: '',
  agenciaId: '',
};

function obtenerId(vendedor) {
  return vendedor.vendedorId ?? vendedor.VendedorId;
}

function obtenerCampo(objeto, camelCase, pascalCase) {
  return objeto[camelCase] ?? objeto[pascalCase] ?? '';
}

export default function TablaVendedores() {
  const [vendedores, setVendedores] = useState([]);
  const [criterio, setCriterio] = useState('');
  const [formulario, setFormulario] = useState(formularioInicial);
  const [idEditar, setIdEditar] = useState(null);
  const [cargando, setCargando] = useState(false);
  const [error, setError] = useState('');
  const [mensaje, setMensaje] = useState('');

  const cargarVendedores = useCallback(async () => {
    setCargando(true);
    setError('');

    try {
      const datos = await obtenerVendedores(criterio);
      setVendedores(Array.isArray(datos) ? datos : []);
    } catch (err) {
      setError(err.message || 'No se pudieron cargar los vendedores.');
    } finally {
      setCargando(false);
    }
  }, [criterio]);

  useEffect(() => {
    cargarVendedores();
  }, [cargarVendedores]);

  function cambiarCampo(evento) {
    const { name, value } = evento.target;

    setFormulario((actual) => ({
      ...actual,
      [name]: value,
    }));
  }

  function editarVendedor(vendedor) {
    setIdEditar(obtenerId(vendedor));

    setFormulario({
      codigoEmpleado: String(
        obtenerCampo(vendedor, 'codigoEmpleado', 'CodigoEmpleado')
      ),
      dpi: String(obtenerCampo(vendedor, 'dpi', 'DPI')),
      nombreCompleto: String(
        obtenerCampo(vendedor, 'nombreCompleto', 'NombreCompleto')
      ),
      agenciaId: String(
        obtenerCampo(vendedor, 'agenciaId', 'AgenciaId')
      ),
    });

    setError('');
    setMensaje('');
  }

  function limpiarFormulario() {
    setFormulario(formularioInicial);
    setIdEditar(null);
  }

  async function guardarVendedor(evento) {
    evento.preventDefault();
    setError('');
    setMensaje('');

    const codigoEmpleado = formulario.codigoEmpleado.trim();
    const dpi = formulario.dpi.trim();
    const nombreCompleto = formulario.nombreCompleto.trim();
    const agenciaId = Number(formulario.agenciaId);

    if (!codigoEmpleado || codigoEmpleado.length > 20) {
      setError('El código de empleado es obligatorio y admite hasta 20 caracteres.');
      return;
    }

    if (!/^\d{13}$/.test(dpi)) {
      setError('El DPI debe contener exactamente 13 dígitos.');
      return;
    }

    if (!nombreCompleto || nombreCompleto.length > 150) {
      setError('El nombre es obligatorio y admite hasta 150 caracteres.');
      return;
    }

    if (!Number.isInteger(agenciaId) || agenciaId <= 0) {
      setError('Ingresa un ID de agencia válido.');
      return;
    }

    const datos = {
      codigoEmpleado,
      dpi,
      nombreCompleto,
      agenciaId,
    };

    try {
      if (idEditar !== null) {
        await actualizarVendedor(idEditar, datos);
        setMensaje('Vendedor actualizado correctamente.');
      } else {
        await crearVendedor(datos);
        setMensaje('Vendedor creado correctamente.');
      }

      limpiarFormulario();
      await cargarVendedores();
    } catch (err) {
      setError(err.message || 'No se pudo guardar el vendedor.');
    }
  }

  async function borrarVendedor(vendedor) {
    const id = obtenerId(vendedor);

    if (!window.confirm(
      `¿Seguro que deseas eliminar al vendedor ${
        obtenerCampo(vendedor, 'nombreCompleto', 'NombreCompleto')
      }?`
    )) {
      return;
    }

    setError('');
    setMensaje('');

    try {
      await eliminarVendedor(id);

      if (idEditar === id) {
        limpiarFormulario();
      }

      setMensaje('Vendedor eliminado correctamente.');
      await cargarVendedores();
    } catch (err) {
      setError(err.message || 'No se pudo eliminar el vendedor.');
    }
  }

  return (
    <section className="crud">
      <div className="crud-encabezado">
        <div>
          <h2>Vendedores</h2>
          <p>Administra los vendedores de MAGIS-AUTO.</p>
        </div>
      </div>

      <form className="formulario" onSubmit={guardarVendedor}>
        <h3>{idEditar !== null ? 'Editar vendedor' : 'Registrar vendedor'}</h3>

        <div className="campos-grid">
          <label>
            Código de empleado
            <input
              name="codigoEmpleado"
              value={formulario.codigoEmpleado}
              onChange={cambiarCampo}
              maxLength={20}
              required
            />
          </label>

          <label>
            DPI
            <input
              name="dpi"
              value={formulario.dpi}
              onChange={cambiarCampo}
              inputMode="numeric"
              maxLength={13}
              pattern="[0-9]{13}"
              required
            />
          </label>

          <label>
            Nombre completo
            <input
              name="nombreCompleto"
              value={formulario.nombreCompleto}
              onChange={cambiarCampo}
              maxLength={150}
              required
            />
          </label>

          <label>
            ID de agencia
            <input
              name="agenciaId"
              type="number"
              min="1"
              step="1"
              value={formulario.agenciaId}
              onChange={cambiarCampo}
              required
            />
          </label>
        </div>

        <div className="acciones">
          <button type="submit">
            {idEditar !== null ? 'Guardar cambios' : 'Registrar vendedor'}
          </button>

          {idEditar !== null && (
            <button
              type="button"
              className="btn-secundario"
              onClick={limpiarFormulario}
            >
              Cancelar edición
            </button>
          )}
        </div>
      </form>

      {error && <p className="mensaje error">{error}</p>}
      {mensaje && <p className="mensaje exito">{mensaje}</p>}

      <div className="busqueda">
        <input
          type="search"
          placeholder="Buscar vendedores..."
          value={criterio}
          onChange={(evento) => setCriterio(evento.target.value)}
        />

        <button type="button" onClick={cargarVendedores}>
          Buscar
        </button>
      </div>

      <div className="tabla-contenedor">
        <table>
          <thead>
            <tr>
              <th>ID</th>
              <th>Código</th>
              <th>DPI</th>
              <th>Nombre completo</th>
              <th>Agencia ID</th>
              <th>Acciones</th>
            </tr>
          </thead>

          <tbody>
            {cargando ? (
              <tr>
                <td colSpan={6}>Cargando vendedores...</td>
              </tr>
            ) : vendedores.length === 0 ? (
              <tr>
                <td colSpan={6}>No hay vendedores para mostrar.</td>
              </tr>
            ) : (
              vendedores.map((vendedor) => (
                <tr key={obtenerId(vendedor)}>
                  <td>{obtenerId(vendedor)}</td>
                  <td>{obtenerCampo(vendedor, 'codigoEmpleado', 'CodigoEmpleado')}</td>
                  <td>{obtenerCampo(vendedor, 'dpi', 'DPI')}</td>
                  <td>{obtenerCampo(vendedor, 'nombreCompleto', 'NombreCompleto')}</td>
                  <td>{obtenerCampo(vendedor, 'agenciaId', 'AgenciaId')}</td>
                  <td className="acciones-tabla">
                    <button
                      type="button"
                      className="btn-secundario"
                      onClick={() => editarVendedor(vendedor)}
                    >
                      Editar
                    </button>

                    <button
                      type="button"
                      className="btn-peligro"
                      onClick={() => borrarVendedor(vendedor)}
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
