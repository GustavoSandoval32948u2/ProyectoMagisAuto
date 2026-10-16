import { useState } from 'react';
import CrudCatalogo from './CrudCatalogo';

function FiadoresGarantias() {
  const [seccion, setSeccion] = useState('Fiadores');

  return (
    <div>
      <h2>Fiadores y Garantías</h2>

      <div className="acciones-modulo">
        <button
          type="button"
          onClick={() => setSeccion('Fiadores')}
        >
          Fiadores
        </button>

        <button
          type="button"
          onClick={() => setSeccion('Garantias')}
        >
          Garantías
        </button>
      </div>

      {seccion === 'Fiadores' && (
        <CrudCatalogo
          titulo="Fiadores de créditos"
          endpoint="/api/CreditoFiador"
          idCampo="creditoFiadorId"
          campos={[
            {
              nombre: 'creditoId',
              etiqueta: 'ID del crédito',
              tipo: 'number',
            },
            {
              nombre: 'personaId',
              etiqueta: 'ID de la persona fiadora',
              tipo: 'number',
            },
          ]}
        />
      )}

      {seccion === 'Garantias' && (
        <CrudCatalogo
          titulo="Garantías"
          endpoint="/api/Garantia"
          idCampo="garantiaId"
          campos={[
            {
              nombre: 'creditoId',
              etiqueta: 'ID del crédito',
              tipo: 'number',
            },
            {
              nombre: 'tipoGarantiaId',
              etiqueta: 'ID del tipo de garantía',
              tipo: 'number',
            },
            {
              nombre: 'descripcion',
              etiqueta: 'Descripción',
              tipo: 'text',
            },
            {
              nombre: 'valorGarantia',
              etiqueta: 'Valor de la garantía',
              tipo: 'number',
            },
          ]}
        />
      )}
    </div>
  );
}

export default FiadoresGarantias;