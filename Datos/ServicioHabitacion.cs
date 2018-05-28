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

        public HABITACION obtenerHabitacion(HABITACION habitacion)
        {

            HABITACION h = ent.HABITACION.FirstOrDefault(objeto =>
            objeto.NUMERO_HABITACION.Equals(habitacion.NUMERO_HABITACION));

            if (h != null)
            {
                return h;
            }
            else
            {
                return null;
            }
        }

        public bool EditarHabitacion(HABITACION habitacion)
        {
            HABITACION h = ent.HABITACION.FirstOrDefault(objeto =>
            objeto.NUMERO_HABITACION.Equals(habitacion.NUMERO_HABITACION));

            if (h != null)
            {
                h.ID_TIPO_HABITACION = habitacion.ID_TIPO_HABITACION;
                h.ESTADO_HABITACION = habitacion.ESTADO_HABITACION;

                ent.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EliminarHabitacion(HABITACION habitacion)
        {
            HABITACION h = ent.HABITACION.FirstOrDefault(objeto =>
            objeto.NUMERO_HABITACION.Equals(habitacion.NUMERO_HABITACION));

            if (h != null)
            {
                ent.HABITACION.Remove(h);
                ent.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<HABITACION> listarHabitacionDisponibleCategoria(DETALLE_ORDEN detalle)
        {
            List<HABITACION> lista = (from consulta in ent.HABITACION
                                     where (consulta.ESTADO_HABITACION.Equals("Disponible")
                                     || consulta.ESTADO_HABITACION.Equals("Vacante 2")
                                     || consulta.ESTADO_HABITACION.Equals("Vacante 1"))
                                     && consulta.ID_CATEGORIA_HABITACION == detalle.ID_CATEGORIA_HABITACION
                                     select consulta).ToList();

            return lista;
        }

        public List<HABITACION> listarHabitacionDisponible(DETALLE_ORDEN detalle)
        {
            List<HABITACION> lista = (from consulta in ent.HABITACION
                                      where consulta.ESTADO_HABITACION.Equals("Disponible")
                                      || consulta.ESTADO_HABITACION.Equals("Vacante 2")
                                      || consulta.ESTADO_HABITACION.Equals("Vacante 1")
                                      select consulta).ToList();

            return lista;
        }
    }
}
