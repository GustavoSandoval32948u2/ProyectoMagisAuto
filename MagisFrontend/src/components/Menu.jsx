
import { useState } from 'react';

const categorias = {
  Principal: ['Inicio', 'Reportes'],
  Clientes: [
    'Clientes',
    'Personas',
    'Referencias',
    'Parentescos',
    'Teléfonos de Persona',
    'Teléfonos de Referencia',
    'Direcciones de Persona',
    'Direcciones de Referencia',
    'Tipos de Teléfono',
  ],
  Vehículos: [
    'Vehículos',
    'Marcas',
    'Líneas',
    'Concesionarias',
    'Historial de Depreciación',
  ],
  Créditos: [
    'Créditos',
    'Cuotas',
    'Pagos',
    'Fiadores y Garantías',
    'Vendedores',
    'Agencias',
    'Regiones',
    'Departamentos',
    'Municipios',
    'Bancos',
    'Medios de Pago',
    'Estados de Crédito',
    'Estados de Cuota',
    'Niveles de Riesgo',
    'Tipos de Garantía',
    'Parámetros Financieros',
    'Comisiones de Pago',
  ],
};

const iconos = {
  Principal: '⌂',
  Clientes: '◎',
  Vehículos: '◇',
  Créditos: '▤',
};

function Menu({ cambiarSeccion, seccionActual }) {
  const [categoriaAbierta, setCategoriaAbierta] = useState('Principal');

  function seleccionarSeccion(nombre) {
    cambiarSeccion(nombre);
  }

  return (
    <nav className="menu" aria-label="Navegación principal">
      <div className="menu-superior">
        <div>
          <span className="menu-eyebrow">ESPACIO DE TRABAJO</span>
          <h2 className="menu-titulo">Navegación</h2>
        </div>

        <span className="menu-total">
          {Object.values(categorias).reduce(
            (total, opciones) => total + opciones.length,
            0
          )}{' '}
          módulos
        </span>
      </div>

      <div className="menu-categorias">
        {Object.entries(categorias).map(([categoria, opciones]) => {
          const abierta = categoriaAbierta === categoria;
          const contieneActual = opciones.includes(seccionActual);

          return (
            <div
              className={`menu-categoria ${
                abierta ? 'categoria-abierta' : ''
              } ${contieneActual ? 'categoria-actual' : ''}`}
              key={categoria}
            >
              <button
                type="button"
                className="menu-categoria-boton"
                aria-expanded={abierta}
                onClick={() =>
                  setCategoriaAbierta(abierta ? '' : categoria)
                }
              >
                <span className="categoria-icono" aria-hidden="true">
                  {iconos[categoria]}
                </span>

                <span className="categoria-nombre">
                  <span>{categoria}</span>
                  <small>
                    {opciones.length}{' '}
                    {opciones.length === 1 ? 'módulo' : 'módulos'}
                  </small>
                </span>

                <span className="categoria-flecha" aria-hidden="true">
                  {abierta ? '−' : '+'}
                </span>
              </button>

              {abierta && (
                <div className="menu-opciones">
                  {opciones.map((nombre, indice) => {
                    const activo = seccionActual === nombre;

                    return (
                      <button
                        key={nombre}
                        type="button"
                        className={activo ? 'nav-activo' : ''}
                        aria-current={activo ? 'page' : undefined}
                        onClick={() => seleccionarSeccion(nombre)}
                        style={{ '--indice-menu': indice }}
                      >
                        <span
                          className="opcion-punto"
                          aria-hidden="true"
                        />
                        <span className="opcion-nombre">{nombre}</span>
                        {activo && (
                          <span className="opcion-flecha" aria-hidden="true">
                            →
                          </span>
                        )}
                      </button>
                    );
                  })}
                </div>
              )}
            </div>
          );
        })}
      </div>

      <div className="menu-pie">
        <span className="menu-pie-marca">MA</span>
        <div>
          <strong>MAGIS-AUTO</strong>
          <small>Gestión integral</small>
        </div>
        <span className="menu-pie-punto" />
      </div>
    </nav>
  );
}

export default Menu;
