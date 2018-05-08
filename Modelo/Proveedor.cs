using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class Proveedor
    {
        public int RUT_PROVEEDOR { get; set; }
        public string DV_PROVEEDOR { get; set; }
        public string PNOMBRE_PROVEEDOR { get; set; }
        public string SNOMBRE_PROVEEDOR { get; set; }
        public string APP_PATERNO_PROVEEDOR { get; set; }
        public string APP_MATERNO_PROVEEDOR { get; set; }
        public short ID_TIPO_PROVEEDOR { get; set; }
        public short ID_USUARIO { get; set; }

        public bool Update() {
            ServicioProveedor serv = new ServicioProveedor();
            PROVEEDOR datos = new PROVEEDOR();
            datos.APP_MATERNO_PROVEEDOR = this.APP_MATERNO_PROVEEDOR;
            datos.APP_PATERNO_PROVEEDOR = this.APP_PATERNO_PROVEEDOR;
            datos.DV_PROVEEDOR = this.DV_PROVEEDOR;
            datos.PNOMBRE_PROVEEDOR = this.PNOMBRE_PROVEEDOR;
            datos.ID_USUARIO = this.ID_USUARIO;
            datos.SNOMBRE_PROVEEDOR = this.SNOMBRE_PROVEEDOR;
            datos.RUT_PROVEEDOR = this.RUT_PROVEEDOR;
            datos.ID_TIPO_PROVEEDOR = this.ID_TIPO_PROVEEDOR;

            return serv.UpdateProveedor(datos);
        }

        public bool BuscarProveedor(short ID_USUARIO) {
            ServicioProveedor serv = new ServicioProveedor();
            PROVEEDOR datos = serv.BuscarProveedor(ID_USUARIO);
            if (datos != null) {
                this.APP_MATERNO_PROVEEDOR = datos.APP_MATERNO_PROVEEDOR;
                this.APP_PATERNO_PROVEEDOR = datos.APP_PATERNO_PROVEEDOR;
                this.DV_PROVEEDOR = datos.DV_PROVEEDOR;
                this.PNOMBRE_PROVEEDOR = datos.PNOMBRE_PROVEEDOR;
                this.ID_USUARIO = datos.ID_USUARIO;
                this.SNOMBRE_PROVEEDOR = datos.SNOMBRE_PROVEEDOR;
                this.RUT_PROVEEDOR = datos.RUT_PROVEEDOR;
                this.ID_TIPO_PROVEEDOR = datos.ID_TIPO_PROVEEDOR;

                return true;
            }
            else {
                return false;

            }
        }
    }
}
