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

        public bool Agregar(ORDEN_COMPRA orden)
        {
            ent.ORDEN_COMPRA.Add(orden);
            ent.SaveChanges();
            return true;
        }

        public bool AgregarDetallePedido(DETALLE_ORDEN d)
        {
            ent.DETALLE_ORDEN.Add(d);
            ent.SaveChanges();
            return true;
        }
    }
}
