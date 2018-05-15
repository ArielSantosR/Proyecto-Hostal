using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos {
    public class ServicioOrdenCompra {
        /*
        HostalEntities ent = new HostalEntities();

        public bool AgregarOrdenCompra(ORDEN_COMPRA orden) {
            ent.ORDEN_COMPRA.Add(orden);
            ent.SaveChanges();
            return true;
        }
        /*
        public bool EditarEstadoOrden(ORDEN_COMPRA orden) {
            ORDEN_COMPRA oc = ent.ORDEN_COMPRA.FirstOrDefault(objeto =>
            objeto.NUMERO_ORDEN.Equals(orden.NUMERO_ORDEN));

            if (oc != null) {
                oc.NUMERO_ORDEN = orden.NUMERO_ORDEN;
                p.ESTADO_DESPACHO = pedido.ESTADO_DESPACHO;
                p.ESTADO_PEDIDO = pedido.ESTADO_PEDIDO;
                p.COMENTARIO = pedido.COMENTARIO;
                p.RUT_EMPLEADO = pedido.RUT_EMPLEADO;
                p.RUT_PROVEEDOR = pedido.RUT_PROVEEDOR;
                p.FECHA_PEDIDO = pedido.FECHA_PEDIDO;

                ent.SaveChanges();
                return true;
            }
            else {
                return false;
            }
        }
        */
        public bool AgregarDetalleOrden(DETALLE_ORDEN d) {
            ent.DETALLE_ORDEN.Add(d);
            ent.SaveChanges();
            return true;
        }
        /*
        public List<ORDEN_COMPRA> ListarOrdenes(ORDEN_COMPRA orden) {
            var lista = (from consulta in ent.PEDIDO
                         where consulta.RUT_PROVEEDOR == proveedor.RUT_PROVEEDOR
                         && consulta.ESTADO_PEDIDO.Equals("Aceptado")
                         && consulta.ESTADO_DESPACHO.Equals("Pendiente")
                         orderby consulta.NUMERO_PEDIDO
                         select consulta).ToList();
            return lista;
        }

        public List<ORDEN_COMPRA> ListarHistorialProveedor(PROVEEDOR proveedor) {
            var lista = (from consulta in ent.PEDIDO
                         where consulta.RUT_PROVEEDOR == proveedor.RUT_PROVEEDOR
                         && (consulta.ESTADO_PEDIDO.Equals("Aceptado")
                         || consulta.ESTADO_PEDIDO.Equals("Recepcionado"))
                         && !consulta.ESTADO_DESPACHO.Equals("Aceptado")
                         orderby consulta.NUMERO_PEDIDO
                         select consulta).ToList();
            return lista;
        }

        public List<ORDEN_COMPRA> ListarPedidoDespacho(PROVEEDOR proveedor) {
            var lista = (from consulta in ent.PEDIDO
                         where consulta.RUT_PROVEEDOR == proveedor.RUT_PROVEEDOR
                         && consulta.ESTADO_PEDIDO.Equals("Aceptado")
                         && consulta.ESTADO_DESPACHO.Equals("Aceptado")
                         orderby consulta.NUMERO_PEDIDO
                         select consulta).ToList();
            return lista;
        }

        public List<ORDEN_COMPRA> ListarPedidosPorDespachar(PROVEEDOR proveedor) {
            var lista = (from consulta in ent.PEDIDO
                         where consulta.RUT_PROVEEDOR == proveedor.RUT_PROVEEDOR
                         && consulta.ESTADO_PEDIDO.Equals("Aceptado")
                         && consulta.ESTADO_DESPACHO.Equals("Despachado")
                         orderby consulta.NUMERO_PEDIDO
                         select consulta).ToList();
            return lista;
        }

        public List<ORDEN_COMPRA> ListarPedidoAdmin() {
            return (from a in ent.PEDIDO where a.ESTADO_PEDIDO.Equals("Pendiente") select a).ToList();
        }

        public List<PEDORDEN_COMPRA> ListarPedidoRecepcion() {
            var lista = (from consulta in ent.PEDIDO
                         where (!consulta.ESTADO_PEDIDO.Equals("Recepcionado")
                         && !consulta.ESTADO_PEDIDO.Equals("No Recepcionado"))
                         && consulta.ESTADO_DESPACHO.Equals("Despachado")
                         orderby consulta.NUMERO_PEDIDO
                         select consulta).ToList();
            return lista;
        }

        //Falta filtrar por rut de empleado
        public List<ORDEN_COMPRA> ListarPedidoEmpleadoPendiente() {
            return (from a in ent.PEDIDO where a.ESTADO_PEDIDO.Equals("Pendiente") select a).ToList();
        }
        public List<PEDIDO> ListarPedidoEmpleadoListo(EMPLEADO empleado) {
            var lista = (from consulta in ent.PEDIDO
                         where consulta.RUT_EMPLEADO == empleado.RUT_EMPLEADO
                         && !consulta.ESTADO_PEDIDO.Equals("Pendiente")
                         orderby consulta.NUMERO_PEDIDO
                         select consulta).ToList();
            return lista;
        }

        public PEDIDO ObtenerPedido(PEDIDO pedido) {
            PEDIDO p = ent.PEDIDO.FirstOrDefault(objeto =>
            objeto.NUMERO_PEDIDO.Equals(pedido.NUMERO_PEDIDO));

            if (p != null) {
                return p;
            }
            else {
                return null;
            }
        }

        //Filtro Detalle Pedido
        public List<DETALLE_PEDIDO> ListaDetallePedido(PEDIDO pedido) {
            var lista = (from consulta in ent.DETALLE_PEDIDO
                         where consulta.NUMERO_PEDIDO == pedido.NUMERO_PEDIDO
                         orderby consulta.ID_DETALLE_PEDIDO
                         select consulta).ToList();
            return lista;
        }
        */
    }
    
}
