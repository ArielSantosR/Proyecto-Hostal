using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class Empleado
    {
        public int RUT_EMPLEADO { get; set; }
        public string DV_EMPLEADO { get; set; }
        public string PNOMBRE_EMPLEADO { get; set; }
        public string SNOMBRE_EMPLEADO { get; set; }
        public string APP_PATERNO_EMPLEADO { get; set; }
        public string APP_MATERNO_EMPLEADO { get; set; }
        public short ID_USUARIO { get; set; }

        public bool Update() {
            ServicioEmpleado serv = new ServicioEmpleado();
            EMPLEADO datos = new EMPLEADO();
            datos.APP_MATERNO_EMPLEADO = this.APP_MATERNO_EMPLEADO;
            datos.APP_PATERNO_EMPLEADO = this.APP_PATERNO_EMPLEADO;
            datos.DV_EMPLEADO = this.DV_EMPLEADO;
            datos.PNOMBRE_EMPLEADO = this.PNOMBRE_EMPLEADO;
            datos.ID_USUARIO = this.ID_USUARIO;
            datos.SNOMBRE_EMPLEADO = this.SNOMBRE_EMPLEADO;
            datos.RUT_EMPLEADO = this.RUT_EMPLEADO;

            return serv.UpdateEmpleado(datos);
        }

        public bool BuscarEmpleado(short ID_USUARIO) {
            ServicioEmpleado serv = new ServicioEmpleado();
            EMPLEADO datos = serv.BuscarEmpleado(ID_USUARIO);
            if (datos != null) {
                this.APP_MATERNO_EMPLEADO = datos.APP_MATERNO_EMPLEADO;
                this.APP_PATERNO_EMPLEADO = datos.APP_PATERNO_EMPLEADO;
                this.DV_EMPLEADO = datos.DV_EMPLEADO;
                this.PNOMBRE_EMPLEADO = datos.PNOMBRE_EMPLEADO;
                this.ID_USUARIO = datos.ID_USUARIO;
                this.SNOMBRE_EMPLEADO = datos.SNOMBRE_EMPLEADO;
                this.RUT_EMPLEADO = datos.RUT_EMPLEADO;

                return true;
            }
            else {
                return false;

            }
        }
    }
}
