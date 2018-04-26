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

        public List<EMPLEADO> ListarEmpleado()
        {
            var lista = (from consulta in ent.EMPLEADO
                         orderby consulta.ID_USUARIO
                         select consulta).ToList();
            return lista;
        }

    }
}
