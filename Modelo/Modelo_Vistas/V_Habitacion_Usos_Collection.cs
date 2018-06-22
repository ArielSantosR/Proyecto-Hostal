using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelo_Vistas {
    [Serializable]
    public class V_Habitacion_Usos_Collection {

        public static List<V_Habitacion_Usos> Listar() {
            ServicioVistas serv = new ServicioVistas();
            return GenerarLista(serv.ListarUsosHabitaciones());
        }

        private static List<V_Habitacion_Usos> GenerarLista(List<V_HABITACION_USOS> listDatos) {
            List<V_Habitacion_Usos> list = new List<V_Habitacion_Usos>();
            V_Habitacion_Usos habi;
            foreach (V_HABITACION_USOS dato in listDatos) {
                habi = new V_Habitacion_Usos();
                habi.NUMERO_HABITACION = dato.NUMERO_HABITACION;
                habi.NUMERO_USOS = dato.NUMERO_USOS;

                list.Add(habi);
            }
            return list;
        }
    }
}
