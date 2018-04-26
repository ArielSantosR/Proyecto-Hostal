using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ServicioHabitacion
    {
        HostalEntities ent = new HostalEntities();

        public List<TIPO_HABITACION> ListarTipoHabitacion()
        {
            return ent.TIPO_HABITACION.ToList();
        }

        public bool AgregarHabitacion(HABITACION habitacion)
        {
            ent.HABITACION.Add(habitacion);
            ent.SaveChanges();
            return true;
        }

        public List<HABITACION> listarHabitacion()
        {
            return ent.HABITACION.ToList();
        }

        public bool ExisteHabitacion(HABITACION habitacion)
        {
            HABITACION h = ent.HABITACION.FirstOrDefault(objeto =>
            objeto.NUMERO_HABITACION.Equals(habitacion.NUMERO_HABITACION));
            if (h != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
