
const accesos = [
  {
    nombre: 'Clientes',
    descripcion: 'Consulta y administra la información de tus clientes.',
    icono: '01',
    categoria: 'GESTIÓN COMERCIAL',
    color: 'azul',
  },
  {
    nombre: 'Vehículos',
    descripcion: 'Gestiona el catálogo y la información de vehículos.',
    icono: '02',
    categoria: 'INVENTARIO',
    color: 'rojo',
  },
  {
    nombre: 'Créditos',
    descripcion: 'Accede a los créditos registrados y su información.',
    icono: '03',
    categoria: 'FINANCIAMIENTO',
    color: 'azul',
  },
  {
    nombre: 'Cuotas',
    descripcion: 'Consulta las cuotas asociadas a los créditos.',
    icono: '04',
    categoria: 'COBROS',
    color: 'dorado',
  },
  {
    nombre: 'Pagos',
    descripcion: 'Accede al módulo de registro y consulta de pagos.',
    icono: '05',
    categoria: 'OPERACIONES',
    color: 'rojo',
  },
  {
    nombre: 'Reportes',
    descripcion: 'Consulta la información operativa y financiera.',
    icono: '06',
    categoria: 'ANÁLISIS',
    color: 'azul',
  },
];

function Dashboard({ cambiarSeccion }) {
  return (
    <div className="dashboard">
      <section className="dashboard-hero">
        <div className="dashboard-hero-contenido">
          <span className="dashboard-etiqueta">
            <span className="dashboard-etiqueta-punto" />
            CENTRO DE OPERACIONES
          </span>

          <h2>
            Todo el control de tu
            <br />
            <span>gestión automotriz.</span>
          </h2>

          <p>
            Administra clientes, vehículos y financiamiento desde un
            solo lugar. Selecciona un módulo para comenzar a trabajar.
          </p>

          <button
            type="button"
            className="dashboard-boton-principal"
            onClick={() => cambiarSeccion('Clientes')}
          >
            Ir a clientes <span aria-hidden="true">→</span>
          </button>
        </div>

        <div className="dashboard-hero-grafico" aria-hidden="true">
          <div className="hero-circulo hero-circulo-externo" />
          <div className="hero-circulo hero-circulo-medio" />
          <div className="hero-circulo hero-circulo-interno">
            <span>MA</span>
          </div>
          <span className="hero-linea hero-linea-uno" />
          <span className="hero-linea hero-linea-dos" />
          <span className="hero-dato hero-dato-uno">MAGIS</span>
          <span className="hero-dato hero-dato-dos">AUTO</span>
        </div>

        <div className="dashboard-hero-acento" />
      </section>

      <div className="dashboard-seccion-encabezado">
        <div>
          <span className="dashboard-overline">ACCESO RÁPIDO</span>
          <h2>Explora tus módulos</h2>
          <p>Las áreas principales de tu operación, a un clic de distancia.</p>
        </div>

        <span className="dashboard-conteo">06 accesos</span>
      </div>

      <div className="dashboard-grid">
        {accesos.map((acceso, indice) => (
          <button
            type="button"
            key={acceso.nombre}
            className={`dashboard-tarjeta tarjeta-${acceso.color}`}
            onClick={() => cambiarSeccion(acceso.nombre)}
            style={{ '--indice-tarjeta': indice }}
          >
            <div className="tarjeta-superior">
              <span className="tarjeta-icono">{acceso.icono}</span>
              <span className="tarjeta-flecha" aria-hidden="true">
                ↗
              </span>
            </div>

            <span className="tarjeta-categoria">{acceso.categoria}</span>
            <strong className="tarjeta-titulo">{acceso.nombre}</strong>
            <span className="tarjeta-descripcion">{acceso.descripcion}</span>

            <span className="tarjeta-enlace">
              Abrir módulo <span aria-hidden="true">→</span>
            </span>
          </button>
        ))}
      </div>

      <div className="dashboard-pie">
        <div className="dashboard-pie-icono" aria-hidden="true">
          ✓
        </div>
        <div>
          <strong>Un espacio de trabajo centralizado</strong>
          <p>
            Utiliza la navegación superior para acceder a los demás
            catálogos, operaciones y reportes del sistema.
          </p>
        </div>
      </div>
    </div>
  );
}

export default Dashboard;
