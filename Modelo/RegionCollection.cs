using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class RegionCollection : List<Region>
    {
        public static List<Region> ListaRegion() {
            ServicioDireccion serv = new ServicioDireccion();
            return GenerarLista(serv.ListarRegion());
        }

        private static List<Region> GenerarLista(List<REGION> listDatos) {
            List<Region> list = new List<Region>();
            Region region;
            foreach (REGION dato in listDatos) {
                region = new Region();
                region.Id_Region = dato.ID_REGION;
                region.Nombre = dato.NOMBRE_REGION;
                region.Id_Pais = dato.ID_PAIS;

                list.Add(region);
            }
            return list;
        }
    }
}