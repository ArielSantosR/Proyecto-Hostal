using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ServicioPedido
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
                         && consulta.ESTADO_PEDIDO.Equals("Aceptado")
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

        public List<PEDIDO> ListarPedidoAdmin()
        {
            return (from a in ent.PEDIDO where a.ESTADO_PEDIDO.Equals("Pendiente") select a).ToList();
        }

        //Falta filtrar por rut de empleado
        public List<PEDIDO> ListarPedidoEmpleadoPendiente()
        {
            return (from a in ent.PEDIDO where a.ESTADO_PEDIDO.Equals("Pendiente") select a).ToList();
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
    }
}
