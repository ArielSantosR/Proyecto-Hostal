using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelo_Vistas {
    [Serializable]
    public class V_Cliente_Ordenes_Collection {

        public static List<V_Cliente_Ordenes> Listar() {
            ServicioVistas serv = new ServicioVistas();
            return GenerarLista(serv.ListarOrdenesClientes());
        }

        private static List<V_Cliente_Ordenes> GenerarLista(List<V_CLIENTE_ORDENES> listDatos) {
            List<V_Cliente_Ordenes> list = new List<V_Cliente_Ordenes>();
            V_Cliente_Ordenes cliente;
            foreach (V_CLIENTE_ORDENES dato in listDatos) {
                cliente = new V_Cliente_Ordenes();
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
