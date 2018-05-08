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

        public List<PROVEEDOR> ListarProveedorProducto(PRODUCTO p)
        {
            return (from a in ent.PROVEEDOR where a.RUT_PROVEEDOR.Equals(p.RUT_PROVEEDOR) select a).ToList();
        }

        //TIPO PROVEEDOR


        public bool AgregarTipoProveedor(TIPO_PROVEEDOR p)
        {
            ent.TIPO_PROVEEDOR.Add(p);
            ent.SaveChanges();
            return true;
        }
        public bool ExisteTipoProveedor(TIPO_PROVEEDOR tipoProveedor)
        {
            TIPO_PROVEEDOR tp = ent.TIPO_PROVEEDOR.FirstOrDefault(objeto =>
            objeto.NOMBRE_TIPO.Equals(tipoProveedor.NOMBRE_TIPO));
            if (tp != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ExisteTipoProveedorID(TIPO_PROVEEDOR tipoProveedor)
        {
            TIPO_PROVEEDOR tp = ent.TIPO_PROVEEDOR.FirstOrDefault(objeto =>
            objeto.ID_TIPO_PROVEEDOR.Equals(tipoProveedor.ID_TIPO_PROVEEDOR));
            if (tp != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public TIPO_PROVEEDOR obtenerTipoProveedor(TIPO_PROVEEDOR tipoProveedor)
        {

            TIPO_PROVEEDOR tp = ent.TIPO_PROVEEDOR.FirstOrDefault(objeto =>
            objeto.ID_TIPO_PROVEEDOR.Equals(tipoProveedor.ID_TIPO_PROVEEDOR));

            if (tp != null)
            {
                return tp;
            }
            else
            {
                return null;
            }
        }
        public bool EditarTipoProveedor(TIPO_PROVEEDOR tipoProveedor)
        {
            TIPO_PROVEEDOR tp = ent.TIPO_PROVEEDOR.FirstOrDefault(objeto =>
            objeto.ID_TIPO_PROVEEDOR.Equals(tipoProveedor.ID_TIPO_PROVEEDOR));

            if (tp != null)
            {
                tp.ID_TIPO_PROVEEDOR = tipoProveedor.ID_TIPO_PROVEEDOR;
                tp.NOMBRE_TIPO = tipoProveedor.NOMBRE_TIPO;

                ent.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool EliminarTipoProveedor(TIPO_PROVEEDOR tipoProveedor)
        {
            TIPO_PROVEEDOR tp = ent.TIPO_PROVEEDOR.FirstOrDefault(objeto =>
            objeto.ID_TIPO_PROVEEDOR.Equals(tipoProveedor.ID_TIPO_PROVEEDOR));

            if (tp != null)
            {
                ent.TIPO_PROVEEDOR.Remove(tp);
                ent.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateProveedor(PROVEEDOR proveedor) {
            PROVEEDOR p = ent.PROVEEDOR.FirstOrDefault(x => x.RUT_PROVEEDOR == proveedor.RUT_PROVEEDOR);
            if (p != null) {
                p.APP_MATERNO_PROVEEDOR = proveedor.APP_MATERNO_PROVEEDOR;
                p.APP_PATERNO_PROVEEDOR = proveedor.APP_PATERNO_PROVEEDOR;
                p.ID_TIPO_PROVEEDOR = proveedor.ID_TIPO_PROVEEDOR;
                p.PNOMBRE_PROVEEDOR = proveedor.PNOMBRE_PROVEEDOR;
                p.SNOMBRE_PROVEEDOR = proveedor.SNOMBRE_PROVEEDOR;

                ent.SaveChanges();
                return true;
            }
            else {
                return false;
            }
        }

        public PROVEEDOR BuscarProveedor(short ID_USUARIO) {
            PROVEEDOR p = ent.PROVEEDOR.FirstOrDefault(x => x.ID_USUARIO == ID_USUARIO);
            return p;
        }
    }
}
