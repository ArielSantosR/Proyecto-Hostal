using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelo_Vistas {
    [Serializable]
    public class V_Habitacion_Usos_Top_10_Collection {

        public static List<V_Habitacion_Usos_Top_10> Listar() {
            ServicioVistas serv = new ServicioVistas();
            return GenerarLista(serv.ListarTopUsoHabitaciones());
        }

        private static List<V_Habitacion_Usos_Top_10> GenerarLista(List<V_HABITACION_USOS_TOP_10> listDatos) {
            List<V_Habitacion_Usos_Top_10> list = new List<V_Habitacion_Usos_Top_10>();
            V_Habitacion_Usos_Top_10 habi;
            foreach (V_HABITACION_USOS_TOP_10 dato in listDatos) {
                habi = new V_Habitacion_Usos_Top_10();
                habi.NUMERO_HABITACION = dato.NUMERO_HABITACION;
                habi.NUMERO_USOS = dato.NUMERO_USOS;

                list.Add(habi);
            }
            return list;
        }
    }
}
