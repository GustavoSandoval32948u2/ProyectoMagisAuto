
import {
  listar,
  guardar,
  eliminar,
} from './crudService';

const ENDPOINT = '/api/Vendedor';

export function obtenerVendedores(criterio = '') {
  return listar(ENDPOINT, criterio);
}

export function crearVendedor(datos) {
  return guardar(ENDPOINT, datos);
}

export function actualizarVendedor(id, datos) {
  return guardar(ENDPOINT, datos, id);
}

export function eliminarVendedor(id) {
  return eliminar(ENDPOINT, id);
}
