using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ServicioCliente
    {
        HostalEntities ent = new HostalEntities();

        public bool AgregarCliente(CLIENTE cliente)
        {
            ent.CLIENTE.Add(cliente);
            ent.SaveChanges();
            return true;
        }

        public bool ExisteRut(CLIENTE cliente)
        {
            CLIENTE c = ent.CLIENTE.FirstOrDefault(objeto =>
            objeto.RUT_CLIENTE.Equals(cliente.RUT_CLIENTE));
            if (c != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<CLIENTE> ListarCliente()
        {
            var lista = (from consulta in ent.CLIENTE
                         orderby consulta.ID_USUARIO
                         select consulta).ToList();
            return lista;
        }

    }
}
