
import {
  listar,
  guardar,
  eliminar,
} from './crudService';

const ENDPOINT = '/api/Pago';

export function obtenerPagos(criterio = '') {
  return listar(ENDPOINT, criterio);
}

export function crearPago(datos) {
  return guardar(ENDPOINT, datos);
}

export function actualizarPago(id, datos) {
  return guardar(ENDPOINT, datos, id);
}

export function eliminarPago(id) {
  return eliminar(ENDPOINT, id);
}
