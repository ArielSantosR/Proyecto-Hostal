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
    }
}
