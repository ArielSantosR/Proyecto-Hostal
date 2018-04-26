using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ServicioProveedor
    {
        HostalEntities ent = new HostalEntities();

        public List<TIPO_PROVEEDOR> ListarTipoProveedor()
        {
            return ent.TIPO_PROVEEDOR.ToList<TIPO_PROVEEDOR>();
        }

        public bool AgregarProveedor(PROVEEDOR proveedor)
        {
            ent.PROVEEDOR.Add(proveedor);
            ent.SaveChanges();
            return true;
        }

        public bool ExisteRut(PROVEEDOR proveedor)
        {
            PROVEEDOR p = ent.PROVEEDOR.FirstOrDefault(objeto =>
            objeto.RUT_PROVEEDOR.Equals(proveedor.RUT_PROVEEDOR));
            if (p != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<PROVEEDOR> ListarProveedor()
        {
            var lista = (from consulta in ent.PROVEEDOR
                         orderby consulta.ID_USUARIO
                         select consulta).ToList();
            return lista;
        }
    }
}
