
import {
  listar,
  guardar,
  eliminar,
} from './crudService';

const ENDPOINT = '/api/Cuota';

export function obtenerCuotas(criterio = '') {
  return listar(ENDPOINT, criterio);
}

export function crearCuota(datos) {
  return guardar(ENDPOINT, datos);
}

export function actualizarCuota(id, datos) {
  return guardar(ENDPOINT, datos, id);
}

export function eliminarCuota(id) {
  return eliminar(ENDPOINT, id);
}
