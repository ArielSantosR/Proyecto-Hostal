using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo {
    [Serializable]
    public class GiroCollection {
        public static List<Giro> ListaGiro() {
            ServicioCliente serv = new ServicioCliente();
            return GenerarLista(serv.ListarGiro());
        }

        private static List<Giro> GenerarLista(List<GIRO> listDatos) {
            List<Giro> list = new List<Giro>();
            Giro giro;
            foreach (GIRO dato in listDatos) {
                giro = new Giro();
                giro.ID_GIRO = dato.ID_GIRO;
                giro.NOMBRE_GIRO = dato.NOMBRE_GIRO;

                list.Add(giro);
            }
            return list;
        }
    }
}
