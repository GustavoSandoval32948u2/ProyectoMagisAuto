
const API_URL = 'https://localhost:7299';

async function solicitar(endpoint, opciones = {}) {
  const respuesta = await fetch(`${API_URL}${endpoint}`, {
    ...opciones,
    headers: {
      Accept: 'application/json',
      ...(opciones.body ? { 'Content-Type': 'application/json' } : {}),
      ...opciones.headers,
    },
  });

  if (!respuesta.ok) {
    let detalle = '';

    try {
      const contenido = await respuesta.text();
      detalle = contenido ? ` - ${contenido}` : '';
    } catch {
      // Conservamos el error HTTP aunque no se pueda leer el cuerpo.
    }

    throw new Error(`Error HTTP ${respuesta.status}${detalle}`);
  }

  // Algunas respuestas exitosas, como DELETE, no contienen JSON.
  if (respuesta.status === 204) {
    return null;
  }

  const contenido = await respuesta.text();

  if (!contenido) {
    return null;
  }

  try {
    return JSON.parse(contenido);
  } catch {
    return contenido;
  }
}

export async function obtenerDatos(endpoint) {
  return solicitar(endpoint, {
    method: 'GET',
  });
}

export async function listar(endpoint, criterio = '') {
  const texto = criterio.trim();

  if (!texto) {
    return obtenerDatos(endpoint);
  }

  const separador = endpoint.includes('?') ? '&' : '?';
  const ruta = `${endpoint}/buscar${separador}criterio=${encodeURIComponent(texto)}`;

  return obtenerDatos(ruta);
}

export async function guardar(endpoint, datos, id = null) {
  const actualizar = id !== null && id !== undefined;

  const ruta = actualizar
    ? `${endpoint}/${encodeURIComponent(id)}`
    : endpoint;

  return solicitar(ruta, {
    method: actualizar ? 'PUT' : 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(datos),
  });
}

export async function eliminar(endpoint, id) {
  if (id === null || id === undefined) {
    throw new Error('Se necesita un ID para eliminar el registro.');
  }

  return solicitar(`${endpoint}/${encodeURIComponent(id)}`, {
    method: 'DELETE',
  });
}
