using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelo_Vistas {
    [Serializable]
    public class V_Dias_Usos_Collection {

        public static List<V_Dias_Usos> Listar() {
            ServicioVistas serv = new ServicioVistas();
            return GenerarLista(serv.ListarDiasUsoHabitaciones());
        }

        private static List<V_Dias_Usos> GenerarLista(List<V_DIAS_USOS> listDatos) {
            List<V_Dias_Usos> list = new List<V_Dias_Usos>();
            V_Dias_Usos habi;
            foreach (V_DIAS_USOS dato in listDatos) {
                habi = new V_Dias_Usos();
                habi.NUMERO_HABITACION = dato.NUMERO_HABITACION;
                habi.DIAS_USO = dato.DIAS_USO;

                list.Add(habi);
            }
            return list;
        }
    }
}
