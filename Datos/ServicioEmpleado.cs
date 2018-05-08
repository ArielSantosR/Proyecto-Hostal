using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ServicioEmpleado
    {
        HostalEntities ent = new HostalEntities();

        public bool AgregarEmpleado(EMPLEADO empleado)
        {
            ent.EMPLEADO.Add(empleado);
            ent.SaveChanges();
            return true;
        }

        public bool ExisteRut(EMPLEADO empleado)
        {
            EMPLEADO e = ent.EMPLEADO.FirstOrDefault(objeto =>
            objeto.RUT_EMPLEADO.Equals(empleado.RUT_EMPLEADO));
            if (e != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public EMPLEADO BuscarIDE(EMPLEADO empleado)
        {
            EMPLEADO e = ent.EMPLEADO.FirstOrDefault(objeto =>
            objeto.ID_USUARIO.Equals(empleado.ID_USUARIO));
            if (e != null)
            {
                return e;
            }
            else
            {
                return null;
            }
        }

        public List<EMPLEADO> ListarEmpleado()
        {
            var lista = (from consulta in ent.EMPLEADO
                         orderby consulta.ID_USUARIO
                         select consulta).ToList();
            return lista;
        }

        public bool UpdateEmpleado(EMPLEADO empleado) {
            EMPLEADO e = ent.EMPLEADO.FirstOrDefault(x => x.RUT_EMPLEADO == empleado.RUT_EMPLEADO);
            if (e != null) {
                e.APP_MATERNO_EMPLEADO = empleado.APP_MATERNO_EMPLEADO;
                e.APP_PATERNO_EMPLEADO = empleado.APP_PATERNO_EMPLEADO;
                e.PNOMBRE_EMPLEADO = empleado.PNOMBRE_EMPLEADO;
                e.SNOMBRE_EMPLEADO = empleado.SNOMBRE_EMPLEADO;

                ent.SaveChanges();
                return true;
            }
            else {
                return false;
            }
        }

        public EMPLEADO BuscarEmpleado(short ID_USUARIO) {
            EMPLEADO e = ent.EMPLEADO.FirstOrDefault(x => x.ID_USUARIO == ID_USUARIO);
            return e;
        }

    }
}
