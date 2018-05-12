using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ServicioRecepcion
    {
        HostalEntities ent = new HostalEntities();

        public bool AgregarRecepcion(RECEPCION recepcion)
        {
            ent.RECEPCION.Add(recepcion);
            ent.SaveChanges();
            return true;
        }

        public bool AgregarDetalleRecepcion(DETALLE_RECEPCION detalle)
        {
            ent.DETALLE_RECEPCION.Add(detalle);
            ent.SaveChanges();
            return true;
        }
    }
}
