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
                                                select detalle).ToList();
            return lista;
        }

    }
}
