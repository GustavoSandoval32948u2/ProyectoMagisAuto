
import { useCallback, useEffect, useState } from 'react';
import {
  obtenerPagos,
  crearPago,
  actualizarPago,
  eliminarPago,
} from '../services/pagosService';

const formularioInicial = {
  cuotaId: '',
  numeroComprobante: '',
  montoPagado: '',
  medioPagoId: '',
  bancoId: '',
  referenciaPago: '',
  fechaHoraPago: '',
};

function campo(objeto, nombre) {
  const Pascal = nombre.charAt(0).toUpperCase() + nombre.slice(1);
  return objeto[nombre] ?? objeto[Pascal] ?? '';
}

function obtenerId(pago) {
  return campo(pago, 'pagoId');
}

function fechaParaInput(valor) {
  if (!valor) return '';

  const fecha = new Date(valor);

  if (Number.isNaN(fecha.getTime())) {
    return String(valor).slice(0, 16);
  }

  const zona = fecha.getTimezoneOffset() * 60000;

  return new Date(fecha.getTime() - zona)
    .toISOString()
    .slice(0, 16);
}

export default function TablaPagos() {
  const [pagos, setPagos] = useState([]);
  const [criterio, setCriterio] = useState('');
  const [formulario, setFormulario] = useState(formularioInicial);
  const [idEditar, setIdEditar] = useState(null);
  const [cargando, setCargando] = useState(false);
  const [error, setError] = useState('');
  const [mensaje, setMensaje] = useState('');

  const cargarPagos = useCallback(async () => {
    setCargando(true);
    setError('');

    try {
      const datos = await obtenerPagos(criterio);
      setPagos(Array.isArray(datos) ? datos : []);
    } catch (err) {
      setError(err.message || 'No se pudieron cargar los pagos.');
    } finally {
      setCargando(false);
    }
  }, [criterio]);

  useEffect(() => {
    cargarPagos();
  }, [cargarPagos]);

  function cambiarCampo(evento) {
    const { name, value } = evento.target;

    setFormulario((actual) => ({
      ...actual,
      [name]: value,
    }));
  }

  function limpiar() {
    setFormulario(formularioInicial);
    setIdEditar(null);
  }

  function editar(pago) {
    setIdEditar(obtenerId(pago));

    setFormulario({
      cuotaId: String(campo(pago, 'cuotaId')),
      numeroComprobante: String(campo(pago, 'numeroComprobante')),
      montoPagado: String(campo(pago, 'montoPagado')),
      medioPagoId: String(campo(pago, 'medioPagoId')),
      bancoId: campo(pago, 'bancoId') === ''
        ? ''
        : String(campo(pago, 'bancoId')),
      referenciaPago: String(campo(pago, 'referenciaPago')),
      fechaHoraPago: fechaParaInput(campo(pago, 'fechaHoraPago')),
    });

    setError('');
    setMensaje('');
  }

  async function guardar(evento) {
    evento.preventDefault();
    setError('');
    setMensaje('');

    const cuotaId = Number(formulario.cuotaId);
    const medioPagoId = Number(formulario.medioPagoId);
    const montoPagado = Number(formulario.montoPagado);
    const bancoId = formulario.bancoId.trim() === ''
      ? null
      : Number(formulario.bancoId);

    if (!Number.isInteger(cuotaId) || cuotaId <= 0) {
      setError('Ingresa un ID de cuota válido.');
      return;
    }

    if (!formulario.numeroComprobante.trim()) {
      setError('El número de comprobante es obligatorio.');
      return;
    }

    if (!Number.isFinite(montoPagado) || montoPagado <= 0) {
      setError('El monto pagado debe ser mayor que cero.');
      return;
    }

    if (!Number.isInteger(medioPagoId) || medioPagoId <= 0) {
      setError('Ingresa un ID de medio de pago válido.');
      return;
    }

    if (
      bancoId !== null &&
      (!Number.isInteger(bancoId) || bancoId <= 0)
    ) {
      setError('El ID del banco debe ser un entero positivo.');
      return;
    }

    const datos = {
      cuotaId,
      numeroComprobante: formulario.numeroComprobante.trim(),
      montoPagado,
      medioPagoId,
      bancoId,
      referenciaPago: formulario.referenciaPago.trim() || null,
      fechaHoraPago: formulario.fechaHoraPago
        ? new Date(formulario.fechaHoraPago).toISOString()
        : null,
    };

    try {
      if (idEditar !== null) {
        await actualizarPago(idEditar, datos);
        setMensaje('Pago actualizado correctamente.');
      } else {
        await crearPago(datos);
        setMensaje('Pago registrado correctamente.');
      }

      limpiar();
      await cargarPagos();
    } catch (err) {
      setError(err.message || 'No se pudo guardar el pago.');
    }
  }

  async function borrar(pago) {
    if (!window.confirm(`¿Eliminar el pago ${obtenerId(pago)}?`)) {
      return;
    }

    setError('');
    setMensaje('');

    try {
      await eliminarPago(obtenerId(pago));

      if (idEditar === obtenerId(pago)) {
        limpiar();
      }

      setMensaje('Pago eliminado correctamente.');
      await cargarPagos();
    } catch (err) {
      setError(err.message || 'No se pudo eliminar el pago.');
    }
  }

  const columnas = [
    ['pagoId', 'ID'],
    ['cuotaId', 'Cuota'],
    ['numeroComprobante', 'Comprobante'],
    ['montoPagado', 'Monto pagado'],
    ['medioPagoId', 'Medio de pago'],
    ['bancoId', 'Banco'],
    ['referenciaPago', 'Referencia'],
    ['fechaHoraPago', 'Fecha y hora'],
  ];

  return (
    <section className="crud">
      <div className="crud-encabezado">
        <div>
          <h2>Pagos</h2>
          <p>Consulta y administra los pagos de las cuotas.</p>
        </div>
      </div>

      <form className="formulario" onSubmit={guardar}>
        <h3>{idEditar !== null ? 'Editar pago' : 'Registrar pago'}</h3>

        <div className="campos-grid">
          <label>
            ID de cuota
            <input
              name="cuotaId"
              type="number"
              min="1"
              step="1"
              value={formulario.cuotaId}
              onChange={cambiarCampo}
              required
            />
          </label>

          <label>
            Número de comprobante
            <input
              name="numeroComprobante"
              value={formulario.numeroComprobante}
              onChange={cambiarCampo}
              required
            />
          </label>

          <label>
            Monto pagado
            <input
              name="montoPagado"
              type="number"
              min="0.01"
              step="0.01"
              value={formulario.montoPagado}
              onChange={cambiarCampo}
              required
            />
          </label>

          <label>
            ID de medio de pago
            <input
              name="medioPagoId"
              type="number"
              min="1"
              step="1"
              value={formulario.medioPagoId}
              onChange={cambiarCampo}
              required
            />
          </label>

          <label>
            ID de banco (opcional)
            <input
              name="bancoId"
              type="number"
              min="1"
              step="1"
              value={formulario.bancoId}
              onChange={cambiarCampo}
            />
          </label>

          <label>
            Referencia de pago
            <input
              name="referenciaPago"
              value={formulario.referenciaPago}
              onChange={cambiarCampo}
            />
          </label>

          <label>
            Fecha y hora de pago
            <input
              name="fechaHoraPago"
              type="datetime-local"
              value={formulario.fechaHoraPago}
              onChange={cambiarCampo}
            />
          </label>
        </div>

        <div className="acciones">
          <button type="submit">
            {idEditar !== null ? 'Guardar cambios' : 'Registrar pago'}
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
          placeholder="Buscar pagos..."
          value={criterio}
          onChange={(evento) => setCriterio(evento.target.value)}
        />
        <button type="button" onClick={cargarPagos}>
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
                <td colSpan={9}>Cargando pagos...</td>
              </tr>
            ) : pagos.length === 0 ? (
              <tr>
                <td colSpan={9}>No hay pagos para mostrar.</td>
              </tr>
            ) : (
              pagos.map((pago) => (
                <tr key={obtenerId(pago)}>
                  {columnas.map(([clave]) => (
                    <td key={clave}>
                      {String(campo(pago, clave) ?? '')}
                    </td>
                  ))}
                  <td className="acciones-tabla">
                    <button
                      type="button"
                      className="btn-secundario"
                      onClick={() => editar(pago)}
                    >
                      Editar
                    </button>
                    <button
                      type="button"
                      className="btn-peligro"
                      onClick={() => borrar(pago)}
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
