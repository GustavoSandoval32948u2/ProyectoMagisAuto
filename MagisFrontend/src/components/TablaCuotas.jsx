import { useCallback, useEffect, useState } from 'react';
import {
  obtenerCuotas,
  crearCuota,
  actualizarCuota,
  eliminarCuota,
} from '../services/cuotasService';

const formularioInicial = {
  creditoId: '',
  numeroCuota: '',
  fechaVencimiento: '',
  montoCapital: '',
  montoInteres: '',
  montoTotal: '',
};

function campo(objeto, nombre) {
  const Pascal = nombre.charAt(0).toUpperCase() + nombre.slice(1);
  return objeto?.[nombre] ?? objeto?.[Pascal] ?? '';
}

function idCuota(cuota) {
  return campo(cuota, 'cuotaId');
}

function obtenerEstado(cuota) {
  const estado = campo(cuota, 'estadoCuota');

  if (estado !== null && estado !== undefined && estado !== '') {
    // Si la API devuelve el nombre directamente.
    if (typeof estado === 'string' || typeof estado === 'number') {
      return String(estado);
    }

    // Si la API devuelve un objeto con el estado.
    if (typeof estado === 'object') {
      return (
        estado.nombre ??
        estado.Nombre ??
        estado.descripcion ??
        estado.Descripcion ??
        estado.estado ??
        estado.Estado ??
        'Estado no disponible'
      );
    }
  }

  const idEstado =
    cuota?.estadoCuotaId ??
    cuota?.EstadoCuotaId;

  if (idEstado !== null && idEstado !== undefined && idEstado !== '') {
    return `ID de estado: ${idEstado}`;
  }

  return 'Sin estado disponible';
}

export default function TablaCuotas() {
  const [cuotas, setCuotas] = useState([]);
  const [criterio, setCriterio] = useState('');
  const [formulario, setFormulario] = useState(formularioInicial);
  const [idEditar, setIdEditar] = useState(null);
  const [cargando, setCargando] = useState(false);
  const [error, setError] = useState('');
  const [mensaje, setMensaje] = useState('');

  const cargarCuotas = useCallback(async () => {
    setCargando(true);
    setError('');

    try {
      const datos = await obtenerCuotas(criterio);
      setCuotas(Array.isArray(datos) ? datos : []);
    } catch (err) {
      setError(err.message || 'No se pudieron cargar las cuotas.');
    } finally {
      setCargando(false);
    }
  }, [criterio]);

  useEffect(() => {
    cargarCuotas();
  }, [cargarCuotas]);

  function cambiarCampo(evento) {
    setFormulario((actual) => ({
      ...actual,
      [evento.target.name]: evento.target.value,
    }));
  }

  function editar(cuota) {
    setIdEditar(idCuota(cuota));

    setFormulario({
      creditoId: String(campo(cuota, 'creditoId')),
      numeroCuota: String(campo(cuota, 'numeroCuota')),
      fechaVencimiento: String(
        campo(cuota, 'fechaVencimiento')
      ).slice(0, 10),
      montoCapital: String(campo(cuota, 'montoCapital')),
      montoInteres: String(campo(cuota, 'montoInteres')),
      montoTotal: String(campo(cuota, 'montoTotal')),
    });

    setError('');
    setMensaje('');
  }

  function limpiar() {
    setFormulario(formularioInicial);
    setIdEditar(null);
  }

  async function guardar(evento) {
    evento.preventDefault();
    setError('');
    setMensaje('');

    const datos = {
      creditoId: Number(formulario.creditoId),
      numeroCuota: Number(formulario.numeroCuota),
      fechaVencimiento: formulario.fechaVencimiento,
      montoCapital: Number(formulario.montoCapital),
      montoInteres: Number(formulario.montoInteres),
      montoTotal: Number(formulario.montoTotal),
    };

    if (
      !Number.isInteger(datos.creditoId) ||
      datos.creditoId <= 0 ||
      !Number.isInteger(datos.numeroCuota) ||
      datos.numeroCuota <= 0 ||
      !datos.fechaVencimiento ||
      !Number.isFinite(datos.montoCapital) ||
      datos.montoCapital < 0 ||
      !Number.isFinite(datos.montoInteres) ||
      datos.montoInteres < 0 ||
      !Number.isFinite(datos.montoTotal) ||
      datos.montoTotal < 0
    ) {
      setError(
        'Revisa los datos: deben ser válidos y los importes no pueden ser negativos.'
      );
      return;
    }

    try {
      if (idEditar !== null) {
        await actualizarCuota(idEditar, datos);
        setMensaje('Cuota actualizada correctamente.');
      } else {
        await crearCuota(datos);
        setMensaje('Cuota creada correctamente.');
      }

      limpiar();
      await cargarCuotas();
    } catch (err) {
      setError(err.message || 'No se pudo guardar la cuota.');
    }
  }

  async function eliminar(cuota) {
    if (!window.confirm(`¿Eliminar la cuota ${idCuota(cuota)}?`)) {
      return;
    }

    setError('');
    setMensaje('');

    try {
      await eliminarCuota(idCuota(cuota));

      if (idEditar === idCuota(cuota)) {
        limpiar();
      }

      setMensaje('Cuota eliminada correctamente.');
      await cargarCuotas();
    } catch (err) {
      setError(err.message || 'No se pudo eliminar la cuota.');
    }
  }

  const columnas = [
    ['cuotaId', 'ID'],
    ['creditoId', 'Crédito'],
    ['numeroObligacion', 'N.º obligación'],
    ['numeroCuota', 'N.º cuota'],
    ['fechaVencimiento', 'Vencimiento'],
    ['montoCapital', 'Capital'],
    ['montoInteres', 'Interés'],
    ['montoTotal', 'Total'],
    ['estadoCuota', 'Estado'],
    ['totalPagado', 'Total pagado'],
  ];

  return (
    <section className="crud">
      <div className="crud-encabezado">
        <div>
          <h2>Cuotas</h2>
          <p>Consulta y administra las cuotas de los créditos.</p>
        </div>
      </div>

      <form className="formulario" onSubmit={guardar}>
        <h3>{idEditar !== null ? 'Editar cuota' : 'Registrar cuota'}</h3>

        <div className="campos-grid">
          <label>
            ID de crédito
            <input
              name="creditoId"
              type="number"
              min="1"
              value={formulario.creditoId}
              onChange={cambiarCampo}
              required
            />
          </label>

          <label>
            Número de cuota
            <input
              name="numeroCuota"
              type="number"
              min="1"
              value={formulario.numeroCuota}
              onChange={cambiarCampo}
              required
            />
          </label>

          <label>
            Fecha de vencimiento
            <input
              name="fechaVencimiento"
              type="date"
              value={formulario.fechaVencimiento}
              onChange={cambiarCampo}
              required
            />
          </label>

          <label>
            Monto de capital
            <input
              name="montoCapital"
              type="number"
              min="0"
              step="0.01"
              value={formulario.montoCapital}
              onChange={cambiarCampo}
              required
            />
          </label>

          <label>
            Monto de interés
            <input
              name="montoInteres"
              type="number"
              min="0"
              step="0.01"
              value={formulario.montoInteres}
              onChange={cambiarCampo}
              required
            />
          </label>

          <label>
            Monto total
            <input
              name="montoTotal"
              type="number"
              min="0"
              step="0.01"
              value={formulario.montoTotal}
              onChange={cambiarCampo}
              required
            />
          </label>
        </div>

        <div className="acciones">
          <button type="submit">
            {idEditar !== null ? 'Guardar cambios' : 'Registrar cuota'}
          </button>

          {idEditar !== null && (
            <button
              type="button"
              className="btn-secundario"
              onClick={limpiar}
            >
              Cancelar
            </button>
          )}
        </div>
      </form>

      {error && <p className="mensaje error">{error}</p>}
      {mensaje && <p className="mensaje exito">{mensaje}</p>}

      <div className="busqueda">
        <input
          type="search"
          placeholder="Buscar cuotas..."
          value={criterio}
          onChange={(evento) => setCriterio(evento.target.value)}
        />

        <button type="button" onClick={cargarCuotas}>
          Buscar
        </button>
      </div>

      <div className="tabla-contenedor">
        <table>
          <thead>
            <tr>
              {columnas.map(([clave, titulo]) => (
                <th key={clave}>{titulo}</th>
              ))}
              <th>Acciones</th>
            </tr>
          </thead>

          <tbody>
            {cargando ? (
              <tr>
                <td colSpan={columnas.length + 1}>
                  Cargando cuotas...
                </td>
              </tr>
            ) : cuotas.length === 0 ? (
              <tr>
                <td colSpan={columnas.length + 1}>
                  No hay cuotas para mostrar.
                </td>
              </tr>
            ) : (
              cuotas.map((cuota) => (
                <tr key={idCuota(cuota)}>
                  {columnas.map(([clave]) => (
                    <td key={clave}>
                      {clave === 'estadoCuota'
                        ? obtenerEstado(cuota)
                        : String(campo(cuota, clave))}
                    </td>
                  ))}

                  <td className="acciones-tabla">
                    <button
                      type="button"
                      className="btn-secundario"
                      onClick={() => editar(cuota)}
                    >
                      Editar
                    </button>

                    <button
                      type="button"
                      className="btn-peligro"
                      onClick={() => eliminar(cuota)}
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
