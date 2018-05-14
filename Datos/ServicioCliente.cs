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

        public bool UpdateCliente(CLIENTE cliente) {
            CLIENTE c = ent.CLIENTE.FirstOrDefault(x => x.RUT_CLIENTE == cliente.RUT_CLIENTE);
            if (c != null) {
                c.NOMBRE_CLIENTE = cliente.NOMBRE_CLIENTE;
                c.CORREO_CLIENTE = cliente.CORREO_CLIENTE;
                c.DIRECCION_CLIENTE = cliente.DIRECCION_CLIENTE;
                c.ID_COMUNA = cliente.ID_COMUNA;
                c.TELEFONO_CLIENTE = cliente.TELEFONO_CLIENTE;

                ent.SaveChanges();
                return true;
            }
            else {
                return false;
            }
        }

        public CLIENTE BuscarCliente(short ID_USUARIO) {
            CLIENTE c = ent.CLIENTE.FirstOrDefault(x => x.ID_USUARIO == ID_USUARIO);
            return c;
        }
    }
}
