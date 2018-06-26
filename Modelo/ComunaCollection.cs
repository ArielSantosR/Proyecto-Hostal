using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class ComunaCollection : List<Comuna>
    {
        public static List<Comuna> ListaComuna() {
            ServicioDireccion serv = new ServicioDireccion();
            return GenerarLista(serv.ListarComuna());
        }

        private static List<Comuna> GenerarLista(List<COMUNA> listDatos) {
            List<Comuna> list = new List<Comuna>();
            Comuna comuna;
            foreach (COMUNA dato in listDatos) {
                comuna = new Comuna();
                comuna.Id_Region = dato.ID_REGION;
                comuna.Nombre = dato.NOMBRE_COMUNA;
                comuna.Id_Comuna = dato.ID_COMUNA;

                list.Add(comuna);
            }
            return list;
        }
    }
}
