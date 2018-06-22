using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelo_Vistas {
    [Serializable]
    public class V_Habitacion_Preferida_Collection {

        public static List<V_Habitacion_Preferida> Listar() {
            ServicioVistas serv = new ServicioVistas();
            return GenerarLista(serv.ListarHabitacionesFrecuentes());
        }

        private static List<V_Habitacion_Preferida> GenerarLista(List<V_HABITACION_PREFERIDA> listDatos) {
            List<V_Habitacion_Preferida> list = new List<V_Habitacion_Preferida>();
            V_Habitacion_Preferida habi;
            foreach (V_HABITACION_PREFERIDA dato in listDatos) {
                habi = new V_Habitacion_Preferida();
                habi.NUMERO_HABITACION = dato.NUMERO_HABITACION;
                habi.NUMERO_USOS = dato.NUMERO_USOS;

                list.Add(habi);
            }
            return list;
        }
    }
}
