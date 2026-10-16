
import { useCallback, useEffect, useState } from 'react';
import { listar, guardar, eliminar } from '../services/crudService';

function CrudCatalogo({ titulo, endpoint, idCampo, campos }) {
  const [datos, setDatos] = useState([]);
  const [valores, setValores] = useState({});
  const [editando, setEditando] = useState(null);
  const [busqueda, setBusqueda] = useState('');
  const [cargando, setCargando] = useState(false);
  const [guardando, setGuardando] = useState(false);
  const [error, setError] = useState('');
  const [mensaje, setMensaje] = useState('');

  const cargar = useCallback(async (criterio = '') => {
    setCargando(true);
    setError('');

    try {
      const resultado = await listar(endpoint, criterio);
      setDatos(Array.isArray(resultado) ? resultado : []);
    } catch (e) {
      setDatos([]);
      setError(
        `No se pudieron cargar ${titulo.toLowerCase()}: ${e.message}`
      );
    } finally {
      setCargando(false);
    }
  }, [endpoint, titulo]);

  useEffect(() => {
    cargar();
  }, [cargar]);

  function nuevoFormulario() {
    setValores({});
    setEditando(null);
    setMensaje('');
    setError('');
  }

  function editar(item) {
    const id = item[idCampo];

    if (id === null || id === undefined) {
      setError(
        `No se encontró el identificador "${idCampo}" en el registro.`
      );
      return;
    }

    const formulario = {};

    campos.forEach(campo => {
      formulario[campo.nombre] = item[campo.nombre] ?? '';
    });

    setValores(formulario);
    setEditando(id);
    setMensaje('');
    setError('');
  }

  async function enviar(e) {
    e.preventDefault();
    setError('');
    setMensaje('');

    // Validar campos obligatorios.
    for (const campo of campos) {
      const valor = valores[campo.nombre];

      if (
        campo.requerido !== false &&
        (valor === '' || valor === null || valor === undefined)
      ) {
        setError(`El campo "${campo.etiqueta}" es obligatorio.`);
        return;
      }
    }

    // Validar campos numéricos.
    for (const campo of campos) {
      const valor = valores[campo.nombre];

      if (
        campo.tipo === 'number' &&
        valor !== '' &&
        valor !== null &&
        valor !== undefined &&
        !Number.isFinite(Number(valor))
      ) {
        setError(
          `El campo "${campo.etiqueta}" debe ser un número válido.`
        );
        return;
      }
    }

    // Construir el objeto que recibirá la API.
    const cuerpo = {};

    campos.forEach(campo => {
      const valor = valores[campo.nombre];

      if (campo.tipo === 'number') {
        cuerpo[campo.nombre] =
          valor === '' || valor === null || valor === undefined
            ? null
            : Number(valor);
      } else if (
        ['boolean', 'checkbox'].includes(campo.tipo)
      ) {
        cuerpo[campo.nombre] = Boolean(valor);
      } else {
        cuerpo[campo.nombre] =
          valor === '' || valor === undefined
            ? null
            : valor;
      }
    });

    setGuardando(true);

    try {
      await guardar(endpoint, cuerpo, editando);

      setMensaje(
        editando !== null
          ? 'Registro actualizado correctamente.'
          : 'Registro creado correctamente.'
      );

      setValores({});
      setEditando(null);

      await cargar(busqueda);
    } catch (e) {
      setError(`No se pudo guardar el registro: ${e.message}`);
    } finally {
      setGuardando(false);
    }
  }

  async function borrar(item) {
    const id = item[idCampo];

    if (id === null || id === undefined) {
      setError(
        `No se puede eliminar: falta el identificador "${idCampo}".`
      );
      return;
    }

    if (!window.confirm('¿Seguro que deseas eliminar este registro?')) {
      return;
    }

    setError('');
    setMensaje('');

    try {
      await eliminar(endpoint, id);
      setMensaje('Registro eliminado correctamente.');
      await cargar(busqueda);
    } catch (e) {
      setError(`No se pudo eliminar el registro: ${e.message}`);
    }
  }

  async function buscar(e) {
    e.preventDefault();
    await cargar(busqueda);
  }

  async function limpiarBusqueda() {
    setBusqueda('');
    await cargar('');
  }

  return (
    <section className="crud">
      <div className="crud-encabezado">
        <h2>{titulo}</h2>

        <button type="button" onClick={nuevoFormulario}>
          Nuevo registro
        </button>
      </div>

      <form className="busqueda" onSubmit={buscar}>
        <input
          value={busqueda}
          onChange={e => setBusqueda(e.target.value)}
          placeholder={`Buscar ${titulo.toLowerCase()}...`}
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

      {error && (
        <p className="mensaje error" role="alert">
          {error}
        </p>
      )}

      {mensaje && (
        <p className="mensaje exito" role="status">
          {mensaje}
        </p>
      )}

      <form className="formulario" onSubmit={enviar}>
        <h3>
          {editando !== null ? 'Editar registro' : 'Crear registro'}
        </h3>

        <div className="campos-grid">
          {campos.map(campo => (
            <label key={campo.nombre}>
              {campo.etiqueta}

              {campo.opciones ? (
                <select
                  required={campo.requerido !== false}
                  value={valores[campo.nombre] ?? ''}
                  onChange={e =>
                    setValores(prev => ({
                      ...prev,
                      [campo.nombre]: e.target.value,
                    }))
                  }
                >
                  <option value="">Selecciona...</option>

                  {campo.opciones.map(opcion => (
                    <option
                      key={opcion.valor}
                      value={opcion.valor}
                    >
                      {opcion.texto}
                    </option>
                  ))}
                </select>
              ) : ['boolean', 'checkbox'].includes(campo.tipo) ? (
                <input
                  type="checkbox"
                  checked={Boolean(valores[campo.nombre])}
                  onChange={e =>
                    setValores(prev => ({
                      ...prev,
                      [campo.nombre]: e.target.checked,
                    }))
                  }
                />
              ) : (
                <input
                  type={campo.tipo || 'text'}
                  required={campo.requerido !== false}
                  step={
                    campo.tipo === 'number' ? 'any' : undefined
                  }
                  value={valores[campo.nombre] ?? ''}
                  onChange={e =>
                    setValores(prev => ({
                      ...prev,
                      [campo.nombre]: e.target.value,
                    }))
                  }
                />
              )}
            </label>
          ))}
        </div>

        <div className="acciones">
          <button type="submit" disabled={guardando}>
            {guardando
              ? 'Guardando...'
              : editando !== null
                ? 'Guardar cambios'
                : 'Crear'}
          </button>

          <button
            type="button"
            className="btn-secundario"
            onClick={nuevoFormulario}
          >
            Limpiar formulario
          </button>
        </div>
      </form>

      <p>Total de registros: {datos.length}</p>

      <div className="tabla-contenedor">
        <table>
          <thead>
            <tr>
              <th>ID</th>

              {campos.map(campo => (
                <th key={campo.nombre}>{campo.etiqueta}</th>
              ))}

              <th>Acciones</th>
            </tr>
          </thead>

          <tbody>
            {cargando ? (
              <tr>
                <td colSpan={campos.length + 2}>
                  Cargando...
                </td>
              </tr>
            ) : datos.length === 0 ? (
              <tr>
                <td colSpan={campos.length + 2}>
                  No hay registros.
                </td>
              </tr>
            ) : (
              datos.map((item, indice) => {
                const id = item[idCampo];

                return (
                  <tr key={id ?? `registro-${indice}`}>
                    <td>{id ?? 'Sin ID'}</td>

                    {campos.map(campo => (
                      <td key={campo.nombre}>
                        {campo.opciones
                          ? campo.opciones.find(
                              opcion =>
                                String(opcion.valor) ===
                                String(item[campo.nombre])
                            )?.texto ??
                            item[campo.nombre] ??
                            '—'
                          : ['boolean', 'checkbox'].includes(campo.tipo)
                            ? item[campo.nombre]
                              ? 'Sí'
                              : 'No'
                            : item[campo.nombre] ?? '—'}
                      </td>
                    ))}

                    <td className="acciones-tabla">
                      <button
                        type="button"
                        onClick={() => editar(item)}
                        disabled={id === null || id === undefined}
                      >
                        Editar
                      </button>

                      <button
                        type="button"
                        className="btn-peligro"
                        onClick={() => borrar(item)}
                        disabled={id === null || id === undefined}
                      >
                        Eliminar
                      </button>
                    </td>
                  </tr>
                );
              })
            )}
          </tbody>
        </table>
      </div>
    </section>
  );
}

export default CrudCatalogo;
