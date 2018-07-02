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

        public List<DETALLE_ORDEN> ListaDetalleReserva() {
            return ent.DETALLE_ORDEN.ToList();
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
                         && !consulta.ESTADO_ORDEN.Equals("Pendiente")
                         && !consulta.ESTADO_ORDEN.Equals("Asignado") 
                         orderby consulta.NUMERO_ORDEN
                         select consulta).ToList();
            return lista;
        }

        public List<ORDEN_COMPRA> HistorialOrdenCompraPendiente(CLIENTE cliente)
        {
            var lista = (from consulta in ent.ORDEN_COMPRA
                         where consulta.RUT_CLIENTE == cliente.RUT_CLIENTE && consulta.ESTADO_ORDEN.Equals("Pendiente")
                         orderby consulta.NUMERO_ORDEN
                         select consulta).ToList();
            return lista;
        }

        public List<ORDEN_COMPRA> HistorialOrdenCompraAsignado(CLIENTE cliente)
        {
            var lista = (from consulta in ent.ORDEN_COMPRA
                         where consulta.RUT_CLIENTE == cliente.RUT_CLIENTE && consulta.ESTADO_ORDEN.Equals("Asignado")
                         orderby consulta.NUMERO_ORDEN
                         select consulta).ToList();
            return lista;
        }

        public ORDEN_COMPRA BuscarOrden(short nUMERO_ORDEN) {
            ORDEN_COMPRA o = ent.ORDEN_COMPRA.FirstOrDefault(x => x.NUMERO_ORDEN == nUMERO_ORDEN);
            if (o != null) {
                return o;
            }
            else {
                return null;
            }
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

        public List<DETALLE_ORDEN> ListaHuespedesNoAsignados(ORDEN_COMPRA orden)
        {
            var lista = (from consulta in ent.DETALLE_ORDEN
                         where consulta.NUMERO_ORDEN == orden.NUMERO_ORDEN
                         && consulta.ESTADO.Equals("Pendiente")
                         orderby consulta.ID_DETALLE
                         select consulta).ToList();
            return lista;
        }

        public List<DETALLE_ORDEN> ListaHuespedesAsignados(ORDEN_COMPRA orden)
        {
            var lista = (from consulta in ent.DETALLE_ORDEN
                         where consulta.NUMERO_ORDEN == orden.NUMERO_ORDEN
                         && consulta.ESTADO.Equals("Asignado")
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

        public List<ORDEN_COMPRA> ListarReservaAsignada() {
            var lista = (from consulta in ent.ORDEN_COMPRA
                         where consulta.ESTADO_ORDEN.Equals("Asignado")
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
                o.ESTADO_ORDEN = orden.ESTADO_ORDEN;
                o.COMENTARIO = orden.COMENTARIO;

                ent.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public DETALLE_ORDEN ObtenerDetalleReserva(DETALLE_ORDEN detalle)
        {
            DETALLE_ORDEN d = ent.DETALLE_ORDEN.FirstOrDefault(objeto => objeto.ID_DETALLE == detalle.ID_DETALLE);

            if (d != null)
            {
                return d;
            }
            else
            {
                return null;
            }
        }

        public bool AgregarDetalleHabitacion(DETALLE_HABITACION detalle)
        {
            ent.DETALLE_HABITACION.Add(detalle);
            ent.SaveChanges();
            return true;
        }

        public bool EditarDetalleHabitacion(DETALLE_HABITACION detalle)
        {
            DETALLE_HABITACION d = ent.DETALLE_HABITACION.FirstOrDefault(objeto => objeto.ID_DETALLE_H == detalle.ID_DETALLE_H);

            if (d != null)
            {
                d.ID_DETALLE_H = detalle.ID_DETALLE_H;
                d.NUMERO_HABITACION = detalle.NUMERO_HABITACION;
                ent.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public DETALLE_HABITACION obtenerDetalleHabitacion(DETALLE_HABITACION detalle)
        {
            DETALLE_HABITACION d = ent.DETALLE_HABITACION.FirstOrDefault(objeto => objeto.ID_DETALLE_H == detalle.ID_DETALLE_H);

            if (d != null)
            {
                return d;
            }
            else
            {
                return null;
            }
        }
        /*
        public bool AgregarDetallePasajeros(DETALLE_PASAJEROS detalle)
        {
            ent.DETALLE_PASAJEROS.Add(detalle);
            ent.SaveChanges();
            return true;
        }
        */
        public bool EditarDetalleReserva(DETALLE_ORDEN detalle)
        {
            DETALLE_ORDEN d = ent.DETALLE_ORDEN.FirstOrDefault(objeto => objeto.ID_DETALLE == detalle.ID_DETALLE);

            if (d != null)
            {
                d.ID_DETALLE = detalle.ID_DETALLE;
                d.ID_PENSION = detalle.ID_PENSION;
                d.NUMERO_ORDEN = detalle.NUMERO_ORDEN;
                d.RUT_HUESPED = detalle.RUT_HUESPED;
                d.ESTADO = detalle.ESTADO;
                d.ORDEN_COMPRA = detalle.ORDEN_COMPRA;
                d.ID_CATEGORIA_HABITACION = detalle.ID_CATEGORIA_HABITACION;
                d.VALOR_HABITACION = detalle.VALOR_HABITACION;
                d.VALOR_MINUTA = detalle.VALOR_MINUTA;

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
