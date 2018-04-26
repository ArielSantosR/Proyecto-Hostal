using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo {
    public class EmpleadoCollection {
        public static List<Empleado> ListaEmpleados() {
            ServicioEmpleado serv = new ServicioEmpleado();
            return GenerarLista(serv.ListarEmpleado());
        }

        private static List<Empleado> GenerarLista(List<EMPLEADO> listDatos) {
            List<Empleado> list = new List<Empleado>();
            Empleado empleado;
            foreach (EMPLEADO dato in listDatos) {
                empleado = new Empleado();
                empleado.APP_MATERNO_EMPLEADO = dato.APP_MATERNO_EMPLEADO;
                empleado.APP_PATERNO_EMPLEADO = dato.APP_PATERNO_EMPLEADO;
                empleado.DV_EMPLEADO = dato.DV_EMPLEADO;
                empleado.ID_USUARIO = dato.ID_USUARIO;
                empleado.PNOMBRE_EMPLEADO = dato.PNOMBRE_EMPLEADO;
                empleado.RUT_EMPLEADO = dato.RUT_EMPLEADO;
                empleado.SNOMBRE_EMPLEADO = dato.SNOMBRE_EMPLEADO;

                list.Add(empleado);
            }
            return list;
        }
    }
}
