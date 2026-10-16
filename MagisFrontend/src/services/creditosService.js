import {
  listar,
  guardar,
  eliminar,
} from './crudService';

const ENDPOINT = '/api/Credito';

export function obtenerCreditos(criterio = '') {
  return listar(ENDPOINT, criterio);
}

export function crearCredito(datos) {
  return guardar(ENDPOINT, datos);
}

export function actualizarCredito(id, datos) {
  return guardar(ENDPOINT, datos, id);
}

export function eliminarCredito(id) {
  return eliminar(ENDPOINT, id);
}