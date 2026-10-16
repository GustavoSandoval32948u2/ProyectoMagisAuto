
function Header({ title, subtitle }) {
  return (
    <header className="encabezado-principal">
      <div className="encabezado-marca">
        <div className="encabezado-logo" aria-hidden="true">
          <span>MA</span>
          <i />
        </div>

        <div className="encabezado-texto">
          <span className="encabezado-superetiqueta">
            AUTOMOTIVE MANAGEMENT SYSTEM
          </span>
          <h1>{title}</h1>
          <p>{subtitle}</p>
        </div>
      </div>

      <div className="encabezado-derecha">
        <div className="encabezado-estado">
          <span className="estado-indicador" />
          Sistema de gestión
        </div>
        <div className="encabezado-etiqueta">
          <span className="etiqueta-simbolo">◆</span>
          MAGIS <span>AUTO</span>
        </div>
      </div>
    </header>
  );
}

export default Header;
