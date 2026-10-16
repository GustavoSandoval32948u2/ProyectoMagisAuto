import {
  listar,
  guardar,
  eliminar,
} from './crudService';

const ENDPOINT = '/api/Cliente';

export function obtenerClientes(criterio = '') {
  return listar(ENDPOINT, criterio);
}

export function crearCliente(datos) {
  return guardar(ENDPOINT, datos);
}

export function actualizarCliente(id, datos) {
  return guardar(ENDPOINT, datos, id);
}

export function eliminarCliente(id) {
  return eliminar(ENDPOINT, id);
}
