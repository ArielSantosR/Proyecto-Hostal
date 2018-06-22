using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelo_Vistas {
    [Serializable]
    public class V_Cliente_Frecuente_Collection {

        public static List<V_Cliente_Frecuente> Listar() {
            ServicioVistas serv = new ServicioVistas();
            return GenerarLista(serv.ListarClienteFrecuente());
        }

        private static List<V_Cliente_Frecuente> GenerarLista(List<V_CLIENTE_FRECUENTE> listDatos) {
            List<V_Cliente_Frecuente> list = new List<V_Cliente_Frecuente>();
            V_Cliente_Frecuente cliente;
            foreach (V_CLIENTE_FRECUENTE dato in listDatos) {
                cliente = new V_Cliente_Frecuente();
                cliente.RUT = dato.RUT;
                cliente.NOMBRE_CLIENTE = dato.NOMBRE_CLIENTE;
                cliente.GIRO = dato.GIRO;
                cliente.CANTIDAD_ORDENES = dato.CANTIDAD_ORDENES;

                list.Add(cliente);
            }
            return list;
        }
    }
}
