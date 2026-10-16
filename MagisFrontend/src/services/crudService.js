const API_URL = 'https://localhost:7299';

async function procesarRespuesta(respuesta) {
  if (!respuesta.ok) {
    let detalle = '';

    try {
      const texto = await respuesta.text();

      if (texto) {
        try {
          const json = JSON.parse(texto);

          detalle = json.errors
            ? Object.entries(json.errors)
                .map(([campo, mensajes]) =>
                  `${campo}: ${
                    Array.isArray(mensajes)
                      ? mensajes.join(', ')
                      : mensajes
                  }`
                )
                .join(' | ')
            : json.detail ||
              json.title ||
              json.message ||
              json.mensaje ||
              texto;
        } catch {
          detalle = texto;
        }
      }
    } catch {
      // La respuesta puede no incluir un cuerpo legible.
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

export async function listar(endpoint, criterio = '') {
  const ruta = criterio.trim()
    ? `${endpoint}/buscar?criterio=${encodeURIComponent(criterio)}`
    : endpoint;

  const respuesta = await fetch(`${API_URL}${ruta}`, {
    headers: { Accept: 'application/json' },
  });

  return procesarRespuesta(respuesta);
}

export async function guardar(endpoint, datos, id = null) {
  const editando = id !== null && id !== undefined;

  const respuesta = await fetch(
    editando
      ? `${API_URL}${endpoint}/${encodeURIComponent(id)}`
      : `${API_URL}${endpoint}`,
    {
      method: editando ? 'PUT' : 'POST',
      headers: {
        'Content-Type': 'application/json',
        Accept: 'application/json',
      },
      body: JSON.stringify(datos),
    }
  );

  return procesarRespuesta(respuesta);
}

export async function eliminar(endpoint, id) {
  const respuesta = await fetch(
    `${API_URL}${endpoint}/${encodeURIComponent(id)}`,
    { method: 'DELETE' }
  );

  return procesarRespuesta(respuesta);
}