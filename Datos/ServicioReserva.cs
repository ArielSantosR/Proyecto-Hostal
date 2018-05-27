using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ServicioReserva
    {
        HostalEntities ent = new HostalEntities();

        public bool AgregarOrdenCompra(ORDEN_COMPRA orden)
        {
            ent.ORDEN_COMPRA.Add(orden);
            ent.SaveChanges();
            return true;
        }

        public bool AgregarDetalleOrden(DETALLE_ORDEN d)
        {
            ent.DETALLE_ORDEN.Add(d);
            ent.SaveChanges();
            return true;
        }

        public List<ORDEN_COMPRA> HistorialOrdenCompra(CLIENTE cliente)
        {
            var lista = (from consulta in ent.ORDEN_COMPRA
                         where consulta.RUT_CLIENTE == cliente.RUT_CLIENTE
                         orderby consulta.NUMERO_ORDEN
                         select consulta).ToList();
            return lista;
        }

        public ORDEN_COMPRA ObtenerReserva(ORDEN_COMPRA orden)
        {
            ORDEN_COMPRA o = ent.ORDEN_COMPRA.FirstOrDefault(objeto =>
            objeto.NUMERO_ORDEN.Equals(orden.NUMERO_ORDEN));

            if (o != null)
            {
                return o;
            }
            else
            {
                return null;
            }
        }

        public List<DETALLE_ORDEN> ListaDetalleReserva(ORDEN_COMPRA orden)
        {
            var lista = (from consulta in ent.DETALLE_ORDEN
                         where consulta.NUMERO_ORDEN == orden.NUMERO_ORDEN
                         orderby consulta.ID_DETALLE
                         select consulta).ToList();
            return lista;
        }

        public List<ORDEN_COMPRA> ListarReservaAceptada()
        {
            var lista = (from consulta in ent.ORDEN_COMPRA
                         where consulta.ESTADO_ORDEN.Equals("Aceptado")
                         orderby consulta.NUMERO_ORDEN
                         select consulta).ToList();
            return lista;
        }

        public List<ORDEN_COMPRA> ListarReservaAdmin()
        {
            return (from a in ent.ORDEN_COMPRA where a.ESTADO_ORDEN.Equals("Pendiente") select a).ToList();
        }

        public bool EditarEstadoReserva(ORDEN_COMPRA orden)
        {
            ORDEN_COMPRA o = ent.ORDEN_COMPRA.FirstOrDefault(objeto =>
            objeto.NUMERO_ORDEN.Equals(orden.NUMERO_ORDEN));

            if (o != null)
            {
                o.NUMERO_ORDEN = orden.NUMERO_ORDEN;
                o.FECHA_LLEGADA = orden.FECHA_LLEGADA;
                o.FECHA_SALIDA = orden.FECHA_SALIDA;
                o.COMENTARIO = orden.COMENTARIO;
                o.RUT_EMPLEADO = orden.RUT_EMPLEADO;
                o.RUT_CLIENTE = orden.RUT_CLIENTE;
                o.ESTADO_ORDEN = orden.ESTADO_ORDEN;

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
