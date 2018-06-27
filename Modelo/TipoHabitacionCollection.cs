using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class TipoHabitacionCollection : List<TipoHabitacion>
    {
        public static List<TipoHabitacion> ListarTipos() {
            ServicioHabitacion serv = new ServicioHabitacion();
            return GenerarLista(serv.ListarTipoHabitacion());
        }

        private static List<TipoHabitacion> GenerarLista(List<TIPO_HABITACION> listDatos) {
            List<TipoHabitacion> list = new List<TipoHabitacion>();
            TipoHabitacion tipo;
            foreach (TIPO_HABITACION dato in listDatos) {
                tipo = new TipoHabitacion();

                tipo.ID_TIPO_HABITACION = dato.ID_TIPO_HABITACION;
                tipo.NOMBRE_TIPO_HABITACION = dato.NOMBRE_TIPO_HABITACION;
                tipo.PRECIO_TIPO = dato.PRECIO_TIPO;
                tipo.CANTIDAD_PASAJERO = dato.CANTIDAD_PASAJERO;

                list.Add(tipo);
            }
            return list;
        }
    }
}
