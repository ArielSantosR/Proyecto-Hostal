using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class PaisCollection : List<Pais> {
        public static List<Pais> ListaPais() {
            ServicioDireccion serv = new ServicioDireccion();
            return GenerarLista(serv.ListarPaises());
        }

        private static List<Pais> GenerarLista(List<PAIS> listDatos) {
            List<Pais> list = new List<Pais>();
            Pais pais;
            foreach (PAIS dato in listDatos) {
                pais = new Pais();
                pais.ID_PAIS = dato.ID_PAIS;
                pais.NOMBRE_PAIS = dato.NOMBRE_PAIS;

                list.Add(pais);
            }
            return list;
        }
    }
}
