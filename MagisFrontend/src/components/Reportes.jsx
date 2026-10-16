
import { useState } from 'react';
import { obtenerReporte } from '../services/reportesService';

const TIPOS_REPORTES = [
  {
    id: 'creditos-por-estado',
    nombre: 'Créditos por estado',
    descripcion: 'Permite conocer cómo se distribuyen los créditos según su estado y revisar la situación general de la cartera.',
  },
  {
    id: 'cuotas-por-estado',
    nombre: 'Cuotas por estado',
    descripcion: 'Permite identificar la situación de las cuotas y analizar cuáles están pagadas o pendientes, según los estados registrados.',
  },
  {
    id: 'vehiculos-por-marca',
    nombre: 'Vehículos por marca',
    descripcion: 'Resume los vehículos por marca para conocer la composición de los vehículos registrados en el sistema.',
  },
  {
    id: 'creditos-por-agencia',
    nombre: 'Créditos por agencia',
    descripcion: 'Permite comparar la distribución de los créditos entre las agencias registradas.',
  },
  {
    id: 'creditos-por-region',
    nombre: 'Créditos por región',
    descripcion: 'Ayuda a analizar la actividad crediticia según la región a la que pertenece cada agencia o vendedor.',
  },
  {
    id: 'creditos-aprobados-por-fecha',
    nombre: 'Créditos aprobados por fecha',
    descripcion: 'Consulta los créditos aprobados en un período para analizar la actividad de aprobación.',
    filtros: [
      { id: 'fechaInicio', nombre: 'Fecha inicial', tipo: 'date', requerido: true },
      { id: 'fechaFin', nombre: 'Fecha final', tipo: 'date', requerido: true },
    ],
  },
  {
    id: 'pagos-por-fecha-y-medio',
    nombre: 'Pagos por fecha y medio',
    descripcion: 'Permite revisar los pagos recibidos durante un período y, opcionalmente, filtrarlos por medio de pago.',
    filtros: [
      { id: 'fechaInicio', nombre: 'Fecha inicial', tipo: 'date', requerido: true },
      { id: 'fechaFin', nombre: 'Fecha final', tipo: 'date', requerido: true },
      { id: 'medioPagoId', nombre: 'ID del medio de pago', tipo: 'number' },
    ],
  },
  {
    id: 'ventas-por-vendedor',
    nombre: 'Ventas por vendedor',
    descripcion: 'Permite consultar la actividad registrada por vendedor durante un período y comparar sus resultados.',
    filtros: [
      { id: 'fechaInicio', nombre: 'Fecha inicial', tipo: 'date' },
      { id: 'fechaFin', nombre: 'Fecha final', tipo: 'date' },
    ],
  },
  {
    id: 'cumpleaneros-mes',
    nombre: 'Cumpleañeros del mes',
    descripcion: 'Identifica a los clientes que cumplen años en el mes seleccionado. Permite imprimir una carta de felicitación individual desde cada fila.',
    filtros: [
      { id: 'mes', nombre: 'Mes (1-12)', tipo: 'number', min: 1, max: 12, requerido: true },
    ],
  },
  {
    id: 'estado-cuenta-cliente',
    nombre: 'Estado de cuenta del cliente',
    descripcion: 'Permite consultar los movimientos y la situación financiera de un cliente para dar seguimiento a sus obligaciones.',
    filtros: [
      { id: 'clienteId', nombre: 'ID del cliente', tipo: 'number', min: 1, requerido: true },
    ],
  },
  {
    id: 'plan-pagos-credito',
    nombre: 'Plan de pagos del crédito',
    descripcion: 'Muestra el calendario de pagos de un crédito para consultar los vencimientos y los importes registrados.',
    filtros: [
      { id: 'creditoId', nombre: 'ID del crédito', tipo: 'number', min: 1, requerido: true },
    ],
  },
  {
    id: 'monto-total-financiado',
    nombre: 'Monto total financiado',
    descripcion: 'Permite consultar el monto financiado en el período seleccionado para analizar la actividad de financiamiento.',
    filtros: [
      { id: 'fechaInicio', nombre: 'Fecha inicial', tipo: 'date' },
      { id: 'fechaFin', nombre: 'Fecha final', tipo: 'date' },
    ],
  },
  {
    id: 'comisiones-por-vendedor',
    nombre: 'Comisiones por vendedor',
    descripcion: 'Permite revisar las comisiones reportadas por vendedor durante un período y apoyar su seguimiento.',
    filtros: [
      { id: 'fechaInicio', nombre: 'Fecha inicial', tipo: 'date' },
      { id: 'fechaFin', nombre: 'Fecha final', tipo: 'date' },
    ],
  },
  {
    id: 'saldo-pendiente-credito',
    nombre: 'Saldo pendiente del crédito',
    descripcion: 'Consulta el saldo pendiente de una obligación para dar seguimiento al monto que todavía debe pagarse.',
    filtros: [
      { id: 'creditoId', nombre: 'ID del crédito', tipo: 'number', min: 1, requerido: true },
    ],
  },
  {
    id: 'garantias-credito',
    nombre: 'Garantías del crédito',
    descripcion: 'Permite consultar las garantías vinculadas a un crédito y revisar los bienes registrados como respaldo.',
    filtros: [
      { id: 'creditoId', nombre: 'ID del crédito', tipo: 'number', min: 1, requerido: true },
    ],
  },
  {
    id: 'depreciacion-vehiculos',
    nombre: 'Depreciación de vehículos',
    descripcion: 'Permite revisar la información de depreciación disponible para los vehículos registrados.',
    filtros: [
      { id: 'vehiculoId', nombre: 'ID del vehículo', tipo: 'number', min: 1 },
    ],
  },
  {
    id: 'total-pagado-por-cliente',
    nombre: 'Total pagado por cliente',
    descripcion: 'Resume los pagos atribuidos a un cliente específico o a los clientes incluidos en la consulta.',
    filtros: [
      { id: 'clienteId', nombre: 'ID del cliente', tipo: 'number', min: 1 },
    ],
  },
  {
    id: 'ingresos-por-mes',
    nombre: 'Ingresos por mes',
    descripcion: 'Agrupa los ingresos por mes del año seleccionado para facilitar el análisis de su distribución temporal.',
    filtros: [
      { id: 'anio', nombre: 'Año', tipo: 'number', min: 2000, max: 2100, requerido: true },
    ],
  },
  {
    id: 'cartera-mora',
    nombre: 'Cartera en mora',
    descripcion: 'Permite identificar registros de cartera con atrasos a partir del mínimo de días indicado, para apoyar el seguimiento de cobros.',
    filtros: [
      {
        id: 'diasAtrasoMinimo',
        nombre: 'Días mínimos de atraso',
        tipo: 'number',
        min: 1,
        requerido: true,
        defecto: '1',
      },
    ],
  },
  {
    id: 'resumen-financiero-general',
    nombre: 'Resumen financiero general',
    descripcion: 'Ofrece una vista consolidada de los indicadores financieros que devuelve la API para apoyar la revisión general del negocio.',
  },
];

function obtenerFilas(datos) {
  if (Array.isArray(datos)) return datos;

  if (datos && typeof datos === 'object') {
    for (const clave of ['datos', 'resultado', 'resultados', 'items', 'data']) {
      if (Array.isArray(datos[clave])) return datos[clave];
    }

    return [datos];
  }

  if (datos === null || datos === undefined || datos === '') return [];

  return [{ Resultado: datos }];
}

function mostrarValor(valor) {
  if (valor === null || valor === undefined) return '';
  if (typeof valor === 'boolean') return valor ? 'Sí' : 'No';

  if (typeof valor === 'object') {
    return JSON.stringify(valor);
  }

  return String(valor);
}

function escaparCSV(valor) {
  return `"${mostrarValor(valor).replace(/"/g, '""')}"`;
}

function escaparHTML(valor) {
  return String(valor).replace(/[&<>"']/g, caracter => ({
    '&': '&amp;',
    '<': '&lt;',
    '>': '&gt;',
    '"': '&quot;',
    "'": '&#39;',
  })[caracter]);
}

function obtenerNombreCliente(cliente) {
  const nombreCompleto =
    cliente.NombreCompleto ??
    cliente.nombreCompleto ??
    cliente.NombreCliente ??
    cliente.nombreCliente ??
    cliente.Nombre ??
    cliente.nombre;

  if (nombreCompleto) return String(nombreCompleto);

  const nombres = [
    cliente.PrimerNombre ?? cliente.primerNombre ?? '',
    cliente.SegundoNombre ?? cliente.segundoNombre ?? '',
    cliente.PrimerApellido ?? cliente.primerApellido ?? '',
    cliente.SegundoApellido ?? cliente.segundoApellido ?? '',
  ].filter(Boolean).join(' ');

  return nombres || 'Estimado/a cliente';
}

function imprimirCartaCumpleanos(cliente) {
  const nombre = obtenerNombreCliente(cliente);
  const ventana = window.open('', '_blank', 'width=800,height=700');

  if (!ventana) {
    alert(
      'El navegador bloqueó la ventana. Permite las ventanas emergentes para imprimir la carta.'
    );
    return;
  }

  const contenido = `
    <!DOCTYPE html>
    <html lang="es">
      <head>
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <title>Felicitación de cumpleaños - MAGIS-AUTO</title>

        <style>
          @page {
            size: letter;
            margin: 2.5cm;
          }

          body {
            font-family: Arial, sans-serif;
            color: #1f2937;
            line-height: 1.8;
            max-width: 700px;
            margin: 40px auto;
            padding: 20px;
          }

          header {
            text-align: center;
            color: #17365d;
            border-bottom: 2px solid #17365d;
            padding-bottom: 20px;
            margin-bottom: 35px;
          }

          h1 {
            font-size: 25px;
            margin-bottom: 8px;
          }

          .fecha {
            text-align: right;
            margin-bottom: 35px;
          }

          .mensaje {
            font-size: 16px;
            text-align: justify;
          }

          .firma {
            margin-top: 45px;
          }

          @media print {
            body {
              margin: 0 auto;
            }
          }
        </style>
      </head>

      <body>
        <header>
          <h1>MAGIS-AUTO</h1>
          <p>Sistema de gestión de vehículos y créditos</p>
        </header>

        <p class="fecha">
          Guatemala, ${escaparHTML(new Date().toLocaleDateString('es-GT'))}
        </p>

        <section class="mensaje">
          <p>Estimado/a <strong>${escaparHTML(nombre)}</strong>:</p>

          <p>
            En MAGIS-AUTO queremos desearle un muy feliz cumpleaños.
            Esperamos que este nuevo año de vida esté lleno de salud,
            alegría, éxitos y nuevas oportunidades.
          </p>

          <p>
            Agradecemos su confianza y la oportunidad de acompañarle.
            Le deseamos que disfrute este día junto a sus seres queridos.
          </p>

          <p>
            Reciba un cordial saludo y nuestros mejores deseos.
          </p>
        </section>

        <footer class="firma">
          <p>Atentamente,</p>
          <p><strong>Equipo MAGIS-AUTO</strong></p>
        </footer>

        <script>
          window.onload = function () {
            window.print();
          };
        <\/script>
      </body>
    </html>
  `;

  ventana.document.open();
  ventana.document.write(contenido);
  ventana.document.close();
}

function Reportes() {
  const [reporteId, setReporteId] = useState('');
  const [filtros, setFiltros] = useState({});
  const [resultados, setResultados] = useState([]);
  const [cargando, setCargando] = useState(false);
  const [error, setError] = useState('');
  const [mensaje, setMensaje] = useState('');
  const [reporteEjecutado, setReporteEjecutado] = useState(null);
  const [filtrosEjecutados, setFiltrosEjecutados] = useState({});

  const reporteSeleccionado = TIPOS_REPORTES.find(
    reporte => reporte.id === reporteId
  );

  const columnas = resultados.length > 0
    ? [...new Set(resultados.flatMap(fila => Object.keys(fila)))]
    : [];

  function cambiarReporte(evento) {
    const nuevoId = evento.target.value;

    setReporteId(nuevoId);
    setFiltros({});
    setResultados([]);
    setError('');
    setMensaje('');
    setReporteEjecutado(null);
    setFiltrosEjecutados({});
  }

  function cambiarFiltro(id, valor) {
    setFiltros(actuales => ({
      ...actuales,
      [id]: valor,
    }));
  }

  function limpiarFiltros() {
    setFiltros({});
    setResultados([]);
    setError('');
    setMensaje('');
    setReporteEjecutado(null);
    setFiltrosEjecutados({});
  }

  async function ejecutarReporte(evento) {
    evento.preventDefault();

    if (!reporteSeleccionado) {
      setError('Selecciona un reporte.');
      return;
    }

    const definiciones = reporteSeleccionado.filtros || [];

    for (const filtro of definiciones) {
      const valor = filtros[filtro.id] ?? filtro.defecto ?? '';

      if (filtro.requerido && String(valor).trim() === '') {
        setError(`El campo "${filtro.nombre}" es obligatorio.`);
        return;
      }

      if (
        valor !== '' &&
        filtro.tipo === 'number' &&
        Number(valor) < (filtro.min ?? -Infinity)
      ) {
        setError(`El valor de "${filtro.nombre}" no es válido.`);
        return;
      }

      if (
        valor !== '' &&
        filtro.tipo === 'number' &&
        Number(valor) > (filtro.max ?? Infinity)
      ) {
        setError(`El valor de "${filtro.nombre}" no es válido.`);
        return;
      }
    }

    if (
      filtros.fechaInicio &&
      filtros.fechaFin &&
      filtros.fechaInicio > filtros.fechaFin
    ) {
      setError('La fecha inicial no puede ser posterior a la fecha final.');
      return;
    }

    const parametros = {};

    for (const filtro of definiciones) {
      const valor = filtros[filtro.id] ?? filtro.defecto ?? '';

      if (valor !== '') parametros[filtro.id] = valor;
    }

    setCargando(true);
    setError('');
    setMensaje('');
    setResultados([]);
    setReporteEjecutado(null);

    try {
      const datos = await obtenerReporte(reporteId, parametros);
      const filas = obtenerFilas(datos);

      setResultados(filas);
      setReporteEjecutado(reporteSeleccionado);
      setFiltrosEjecutados({ ...parametros });

      if (filas.length === 0) {
        setMensaje('El reporte se ejecutó correctamente, pero no hay resultados.');
      } else {
        setMensaje(`Reporte generado. Registros encontrados: ${filas.length}.`);
      }
    } catch (err) {
      setError(err.message || 'No fue posible generar el reporte.');
    } finally {
      setCargando(false);
    }
  }

  function exportarCSV() {
    if (resultados.length === 0 || columnas.length === 0) return;

    const filasCSV = [
      columnas.map(escaparCSV),
      ...resultados.map(fila =>
        columnas.map(columna => escaparCSV(fila[columna]))
      ),
    ];

    const contenido = '\uFEFF' +
      filasCSV.map(fila => fila.join(',')).join('\r\n');

    const archivo = new Blob([contenido], {
      type: 'text/csv;charset=utf-8;',
    });

    const url = URL.createObjectURL(archivo);
    const enlace = document.createElement('a');

    enlace.href = url;
    enlace.download = `reporte-${reporteId}-${new Date()
      .toISOString()
      .slice(0, 10)}.csv`;

    document.body.appendChild(enlace);
    enlace.click();
    enlace.remove();
    URL.revokeObjectURL(url);
  }

  function imprimirReporte() {
    if (resultados.length > 0) window.print();
  }

  return (
    <section className="modulo-reportes">
      <div className="reportes-encabezado">
        <div>
          <h2>Reportes</h2>
          <p>Consulta información operativa y financiera de MAGIS-AUTO.</p>
        </div>
      </div>

      <form className="formulario no-imprimir" onSubmit={ejecutarReporte}>
        <div className="campos-grid">
          <label>
            Tipo de reporte
            <select value={reporteId} onChange={cambiarReporte} required>
              <option value="">Selecciona un reporte</option>

              {TIPOS_REPORTES.map(reporte => (
                <option key={reporte.id} value={reporte.id}>
                  {reporte.nombre}
                </option>
              ))}
            </select>
          </label>

          {(reporteSeleccionado?.filtros || []).map(filtro => (
            <label key={filtro.id}>
              {filtro.nombre}
              <input
                type={filtro.tipo || 'text'}
                value={filtros[filtro.id] ?? filtro.defecto ?? ''}
                onChange={evento =>
                  cambiarFiltro(filtro.id, evento.target.value)
                }
                min={filtro.min}
                max={filtro.max}
                required={Boolean(filtro.requerido)}
              />
            </label>
          ))}
        </div>

        {reporteSeleccionado && (
          <div className="reporte-descripcion">
            <strong>¿Para qué sirve este reporte?</strong>
            <p>{reporteSeleccionado.descripcion}</p>
          </div>
        )}

        <div className="acciones">
          <button type="submit" disabled={cargando || !reporteId}>
            {cargando ? 'Generando reporte...' : 'Generar reporte'}
          </button>

          <button
            type="button"
            className="btn-secundario"
            onClick={limpiarFiltros}
            disabled={cargando}
          >
            Limpiar
          </button>
        </div>
      </form>

      {error && (
        <div className="mensaje error no-imprimir" role="alert">
          {error}
        </div>
      )}

      {mensaje && (
        <div className="mensaje exito no-imprimir" role="status">
          {mensaje}
        </div>
      )}

      {reporteEjecutado && resultados.length > 0 && (
        <div className="reporte-resultados">
          <div className="reporte-documento-encabezado">
            <h3>{reporteEjecutado.nombre}</h3>
            <p>MAGIS-AUTO · Sistema de gestión de vehículos y créditos</p>
            <p>Fecha de generación: {new Date().toLocaleString('es-GT')}</p>

            {Object.keys(filtrosEjecutados).length > 0 && (
              <p>
                Filtros aplicados:{' '}
                {Object.entries(filtrosEjecutados)
                  .map(([clave, valor]) => `${clave}: ${valor}`)
                  .join(' · ')}
              </p>
            )}

            <p>Total de registros: {resultados.length}</p>
          </div>

          <div className="reportes-acciones no-imprimir">
            <button type="button" onClick={exportarCSV}>
              Exportar a CSV
            </button>

            <button
              type="button"
              className="btn-secundario"
              onClick={imprimirReporte}
            >
              Imprimir / Guardar PDF
            </button>
          </div>

          <div className="tabla-contenedor">
            <table>
              <thead>
                <tr>
                  {columnas.map(columna => (
                    <th key={columna}>{columna}</th>
                  ))}

                  {reporteId === 'cumpleaneros-mes' && (
                    <th className="no-imprimir">Acciones</th>
                  )}
                </tr>
              </thead>

              <tbody>
                {resultados.map((fila, indice) => (
                  <tr key={indice}>
                    {columnas.map(columna => (
                      <td key={columna}>{mostrarValor(fila[columna])}</td>
                    ))}

                    {reporteId === 'cumpleaneros-mes' && (
                      <td className="no-imprimir">
                        <button
                          type="button"
                          className="btn-secundario"
                          onClick={() => imprimirCartaCumpleanos(fila)}
                          title={`Imprimir carta para ${obtenerNombreCliente(fila)}`}
                        >
                          Imprimir carta
                        </button>
                      </td>
                    )}
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
      )}

      {!reporteEjecutado && !cargando && !error && (
        <p className="reportes-instruccion no-imprimir">
          Selecciona un reporte y sus filtros para consultar la información.
        </p>
      )}
    </section>
  );
}

export default Reportes;
