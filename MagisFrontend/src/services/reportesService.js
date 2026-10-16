
const API_URL = 'https://localhost:7299';
const ENDPOINT = '/api/Reportes';

export async function obtenerReporte(ruta, parametros = {}) {
  const query = new URLSearchParams();

  Object.entries(parametros).forEach(([clave, valor]) => {
    if (valor !== '' && valor !== null && valor !== undefined) {
      query.set(clave, String(valor));
    }
  });

  const sufijo = query.toString();
  const url = `${API_URL}${ENDPOINT}/${ruta}${sufijo ? `?${sufijo}` : ''}`;

  const respuesta = await fetch(url, {
    headers: { Accept: 'application/json' },
  });

  if (!respuesta.ok) {
    let detalle = '';

    try {
      const texto = await respuesta.text();

      if (texto) {
        try {
          const json = JSON.parse(texto);
          detalle = json.message || json.mensaje || json.detail || json.title || texto;
        } catch {
          detalle = texto;
        }
      }
    } catch {
      // La respuesta puede no contener un mensaje legible.
    }

    throw new Error(
      `HTTP ${respuesta.status}${detalle ? `: ${detalle}` : ''}`
    );
  }

  if (respuesta.status === 204) return null;

  const texto = await respuesta.text();

  if (!texto) return null;

  try {
    return JSON.parse(texto);
  } catch {
    return texto;
  }
}
