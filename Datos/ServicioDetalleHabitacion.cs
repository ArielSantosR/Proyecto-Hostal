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

        public List<DETALLE_HABITACION> listarDetalleHabitacion()
        {
            return ent.DETALLE_HABITACION.ToList();
        }

    }
}
