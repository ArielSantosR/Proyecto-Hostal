using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo {
    public class ProveedorCollection {
        public static List<Proveedor> ListaProveedores() {
            ServicioProveedor serv = new ServicioProveedor();
            return GenerarLista(serv.ListarProveedorFull());
        }

        private static List<Proveedor> GenerarLista(List<PROVEEDOR> listDatos) {
            List<Proveedor> list = new List<Proveedor>();
            Proveedor proveedor;
            foreach (PROVEEDOR dato in listDatos) {
                proveedor = new Proveedor();
                proveedor.APP_MATERNO_PROVEEDOR = dato.APP_MATERNO_PROVEEDOR;
                proveedor.APP_PATERNO_PROVEEDOR = dato.APP_PATERNO_PROVEEDOR;
                proveedor.DV_PROVEEDOR = dato.DV_PROVEEDOR;
                proveedor.ID_TIPO_PROVEEDOR = dato.ID_TIPO_PROVEEDOR;
                proveedor.ID_USUARIO = dato.ID_USUARIO;
                proveedor.PNOMBRE_PROVEEDOR = dato.PNOMBRE_PROVEEDOR;
                proveedor.RUT_PROVEEDOR = dato.RUT_PROVEEDOR;
                proveedor.SNOMBRE_PROVEEDOR = dato.SNOMBRE_PROVEEDOR;

                list.Add(proveedor);
            }
            return list;
        }
    }
}
