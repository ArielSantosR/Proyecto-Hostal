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

        public PAIS BuscarPais(short iD_PAIS) {
            PAIS p = ent.PAIS.FirstOrDefault(x => x.ID_PAIS == iD_PAIS);
            return p;
        }

        public REGION BuscarRegion(short id_Region) {
            REGION r = ent.REGION.FirstOrDefault(x => x.ID_REGION == id_Region);
            return r;
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

        public COMUNA BuscarComuna(short id_Comuna) {
            COMUNA c = ent.COMUNA.FirstOrDefault(x => x.ID_COMUNA == id_Comuna);
            return c;
        }

        public List<CLIENTE> ListarCliente()
        {
            var lista = (from consulta in ent.CLIENTE
                         orderby consulta.ID_USUARIO
                         select consulta).ToList();
            return lista;
        }

        public List<HUESPED> ListarHuesped(CLIENTE cliente)
        {
            var lista = (from consulta in ent.HUESPED
                         where consulta.RUT_CLIENTE == cliente.RUT_CLIENTE
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
                c.ID_GIRO = cliente.ID_GIRO;

                ent.SaveChanges();
                return true;
            }
            else {
                return false;
            }
        }

        public CLIENTE BuscarCliente(int rUT_CLIENTE) {
            CLIENTE c = ent.CLIENTE.FirstOrDefault(x => x.RUT_CLIENTE == rUT_CLIENTE);
            return c;
        }

        public List<GIRO> ListarGiro() {
            return ent.GIRO.ToList();
        }

        public CLIENTE BuscarCliente(short ID_USUARIO) {
            CLIENTE c = ent.CLIENTE.FirstOrDefault(x => x.ID_USUARIO == ID_USUARIO);
            return c;
        }

        public CLIENTE BuscarIDC(CLIENTE cliente)
        {
            CLIENTE p = ent.CLIENTE.FirstOrDefault(objeto =>
            objeto.ID_USUARIO.Equals(cliente.ID_USUARIO));
            if (p != null)
            {
                return p;
            }
            else
            {
                return null;
            }
        }
    }
}
