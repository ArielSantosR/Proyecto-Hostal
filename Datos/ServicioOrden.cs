using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ServicioOrden
    {
        HostalEntities ent = new HostalEntities();

        public bool AgregarPedido(PEDIDO pedido)
        {
            ent.PEDIDO.Add(pedido);
            ent.SaveChanges();
            return true;
        }

        public bool EditarEstadoPedido(PEDIDO pedido)
        {
            PEDIDO p = ent.PEDIDO.FirstOrDefault(objeto =>
            objeto.NUMERO_PEDIDO.Equals(pedido.NUMERO_PEDIDO));

            if (p != null)
            {
                p.NUMERO_PEDIDO = pedido.NUMERO_PEDIDO;
                p.ESTADO_DESPACHO = pedido.ESTADO_DESPACHO;
                p.ESTADO_PEDIDO = pedido.ESTADO_PEDIDO;
                p.COMENTARIO = pedido.COMENTARIO;
                p.RUT_EMPLEADO = pedido.RUT_EMPLEADO;
                p.RUT_PROVEEDOR = pedido.RUT_PROVEEDOR;
                p.FECHA_PEDIDO = pedido.FECHA_PEDIDO;

                ent.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AgregarDetallePedido(DETALLE_PEDIDO d)
        {
            ent.DETALLE_PEDIDO.Add(d);
            ent.SaveChanges();
            return true;
        }

        public List<PEDIDO> ListarPedidoProveedor(PROVEEDOR proveedor)
        {
            var lista = (from consulta in ent.PEDIDO
                         where consulta.RUT_PROVEEDOR == proveedor.RUT_PROVEEDOR
                         && consulta.ESTADO_PEDIDO.Equals("Aceptado")
                         && consulta.ESTADO_DESPACHO.Equals("Pendiente")
                         orderby consulta.NUMERO_PEDIDO
                         select consulta).ToList();
            return lista;
        }

        public List<PEDIDO> ListarHistorialProveedor(PROVEEDOR proveedor)
        {
            var lista = (from consulta in ent.PEDIDO
                         where consulta.RUT_PROVEEDOR == proveedor.RUT_PROVEEDOR
                         && (consulta.ESTADO_PEDIDO.Equals("Aceptado")
                         || consulta.ESTADO_PEDIDO.Equals("Recepcionado"))
                         && !consulta.ESTADO_DESPACHO.Equals("Aceptado")
                         orderby consulta.NUMERO_PEDIDO
                         select consulta).ToList();
            return lista;
        }

        public List<PEDIDO> ListarPedidoDespacho(PROVEEDOR proveedor)
        {
            var lista = (from consulta in ent.PEDIDO
                         where consulta.RUT_PROVEEDOR == proveedor.RUT_PROVEEDOR
                         && consulta.ESTADO_PEDIDO.Equals("Aceptado")
                         && consulta.ESTADO_DESPACHO.Equals("Aceptado")
                         orderby consulta.NUMERO_PEDIDO
                         select consulta).ToList();
            return lista;
        }

        public List<PEDIDO> ListarPedidosPorDespachar(PROVEEDOR proveedor)
        {
            var lista = (from consulta in ent.PEDIDO
                         where consulta.RUT_PROVEEDOR == proveedor.RUT_PROVEEDOR
                         && consulta.ESTADO_PEDIDO.Equals("Aceptado")
                         && consulta.ESTADO_DESPACHO.Equals("Despachado")
                         orderby consulta.NUMERO_PEDIDO
                         select consulta).ToList();
            return lista;
        }

        public List<PEDIDO> ListarPedidoAdmin()
        {
            return (from a in ent.PEDIDO where a.ESTADO_PEDIDO.Equals("Pendiente") select a).ToList();
        }

        public List<PEDIDO> ListarPedidoRecepcion()
        {
            var lista = (from consulta in ent.PEDIDO
                         where (!consulta.ESTADO_PEDIDO.Equals("Recepcionado")
                         && !consulta.ESTADO_PEDIDO.Equals("No Recepcionado"))
                         && consulta.ESTADO_DESPACHO.Equals("Despachado")
                         orderby consulta.NUMERO_PEDIDO
                         select consulta).ToList();
            return lista;
        }

        //Falta filtrar por rut de empleado
        public List<PEDIDO> ListarPedidoEmpleadoPendiente(EMPLEADO empleado)
        {
            var lista = (from consulta in ent.PEDIDO
                         where consulta.RUT_EMPLEADO == empleado.RUT_EMPLEADO
                         && consulta.ESTADO_PEDIDO.Equals("Pendiente")
                         orderby consulta.NUMERO_PEDIDO
                         select consulta).ToList();
            return lista;
        }
        public List<PEDIDO> ListarPedidoEmpleadoListo(EMPLEADO empleado)
        {
            var lista = (from consulta in ent.PEDIDO
                         where consulta.RUT_EMPLEADO == empleado.RUT_EMPLEADO
                         && !consulta.ESTADO_PEDIDO.Equals("Pendiente") 
                         orderby consulta.NUMERO_PEDIDO
                         select consulta).ToList();
            return lista;
        }

        public PEDIDO ObtenerPedido(PEDIDO pedido)
        {
            PEDIDO p = ent.PEDIDO.FirstOrDefault(objeto =>
            objeto.NUMERO_PEDIDO.Equals(pedido.NUMERO_PEDIDO));

            if (p != null)
            {
                return p;
            }
            else
            {
                return null;
            }
        }

        public DETALLE_PEDIDO obtenerDetallePedido(DETALLE_PEDIDO detalle)
        {
            DETALLE_PEDIDO d = ent.DETALLE_PEDIDO.FirstOrDefault(objeto =>
            objeto.ID_DETALLE_PEDIDO == detalle.ID_DETALLE_PEDIDO);

            if (d != null)
            {
                return d;
            }
            else
            {
                return null;
            }
        }

        public bool EditarDetallePedido(DETALLE_PEDIDO detalle)
        {
            DETALLE_PEDIDO d = ent.DETALLE_PEDIDO.FirstOrDefault(objeto =>
            objeto.ID_DETALLE_PEDIDO == detalle.ID_DETALLE_PEDIDO);

            if (d != null)
            {
                d.ID_DETALLE_PEDIDO = detalle.ID_DETALLE_PEDIDO;
                d.CANTIDAD = detalle.CANTIDAD;
                d.NUMERO_PEDIDO = detalle.NUMERO_PEDIDO;
                d.ID_PRODUCTO = detalle.ID_PRODUCTO;

                ent.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        //Filtro Detalle Pedido
        public List<DETALLE_PEDIDO> ListaDetallePedido(PEDIDO pedido)
        {
            var lista = (from consulta in ent.DETALLE_PEDIDO
                         where consulta.NUMERO_PEDIDO == pedido.NUMERO_PEDIDO
                         orderby consulta.ID_DETALLE_PEDIDO
                         select consulta).ToList();
            return lista;
        }

        public bool EliminarDetallePedido(DETALLE_PEDIDO detalle)
        {
            DETALLE_PEDIDO d = ent.DETALLE_PEDIDO.FirstOrDefault(objeto =>
                objeto.ID_DETALLE_PEDIDO.Equals(detalle.ID_DETALLE_PEDIDO));

            if (d != null)
            {
                ent.DETALLE_PEDIDO.Remove(d);
                ent.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
