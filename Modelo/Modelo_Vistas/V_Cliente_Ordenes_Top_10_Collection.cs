using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelo_Vistas {
    [Serializable]
    public class V_Cliente_Ordenes_Top_10_Collection {

        public static List<V_Cliente_Ordenes_Top_10> Listar() {
            ServicioVistas serv = new ServicioVistas();
            return GenerarLista(serv.ListarTopClientes());
        }

        private static List<V_Cliente_Ordenes_Top_10> GenerarLista(List<V_CLIENTE_ORDENES_TOP_10> listDatos) {
            List<V_Cliente_Ordenes_Top_10> list = new List<V_Cliente_Ordenes_Top_10>();
            V_Cliente_Ordenes_Top_10 cliente;
            foreach (V_CLIENTE_ORDENES_TOP_10 dato in listDatos) {
                cliente = new V_Cliente_Ordenes_Top_10();
                cliente.RUT = dato.RUT;
                cliente.NOMBRE_CLIENTE = dato.NOMBRE_CLIENTE;
                cliente.GIRO = dato.GIRO;
                cliente.CANTIDAD_ORDENES = dato.CANTIDAD_ORDENES;
                cliente.ORDENES_ACEPTADAS = dato.ORDENES_ACEPTADAS;
                cliente.ORDENES_ASIGNADAS = dato.ORDENES_ASIGNADAS;
                cliente.ORDENES_CANCELADAS = dato.ORDENES_CANCELADAS;
                cliente.ORDENES_PENDIENTES = dato.ORDENES_PENDIENTES;
                cliente.ORDENES_RECHAZADAS = dato.ORDENES_RECHAZADAS;

                list.Add(cliente);
            }
            return list;
        }
    }
}
