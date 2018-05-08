using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class TipoProveedorCollection : List<TipoProveedor>
    {
        public static List<TipoProveedor> ListaTiposProveedor() {
            ServicioProveedor serv = new ServicioProveedor();
            return GenerarLista(serv.ListarTipoProveedor());
        }

        private static List<TipoProveedor> GenerarLista(List<TIPO_PROVEEDOR> listDatos) {
            List<TipoProveedor> list = new List<TipoProveedor>();
            TipoProveedor tipo;
            foreach (TIPO_PROVEEDOR dato in listDatos) {
                tipo = new TipoProveedor();

                tipo.ID_TIPO_PROVEEDOR = dato.ID_TIPO_PROVEEDOR;
                tipo.NOMBRE_TIPO = dato.NOMBRE_TIPO;

                list.Add(tipo);
            }
            return list;
        }
    }
}
