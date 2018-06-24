using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ServicioCategoria
    {
        HostalEntities ent = new HostalEntities();

        public List<CATEGORIA_HABITACION> ListarCategoriaHabitacion()
        {
            return ent.CATEGORIA_HABITACION.ToList();
        }

        public CATEGORIA_HABITACION BuscarCategoria(short iD_CATEGORIA_HABITACION) {
            CATEGORIA_HABITACION c = ent.CATEGORIA_HABITACION.FirstOrDefault(x => x.ID_CATEGORIA_HABITACION == iD_CATEGORIA_HABITACION);
            return c;
        }
    }
}
