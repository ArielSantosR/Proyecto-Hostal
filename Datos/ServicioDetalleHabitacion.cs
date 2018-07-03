using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    

    public class ServicioDetalleHabitacion
    {
        HostalEntities ent = new HostalEntities();

        public List<DETALLE_HABITACION> listarDetalleHabitacion(CLIENTE cliente)
        {
            List<DETALLE_HABITACION> lista = (from detalle in ent.DETALLE_HABITACION
                                              where detalle.RUT_CLIENTE == cliente.RUT_CLIENTE
                                              orderby detalle.ID_DETALLE_H
                                              select detalle).ToList();
            return lista;
        }

        public List<DETALLE_HABITACION> listarDetalleHabitacionOrden(DETALLE_HABITACION detalles)
        {
            List<DETALLE_HABITACION> lista = (from detalle in ent.DETALLE_HABITACION
                                              where detalle.NUMERO_ORDEN == detalles.NUMERO_ORDEN
                                              orderby detalle.ID_DETALLE_H
                                              select detalle).ToList();
            return lista;
        }

    }
}
