using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    
    public class ServicioProducto
    {
        HostalEntities ent = new HostalEntities();
        public bool AgregarProducto(PRODUCTO p)
        {
            ent.PRODUCTO.Add(p);
            ent.SaveChanges();
            return true;
        }

        public List<PRODUCTO> ListarProveedorProducto(PROVEEDOR p)
        {
            return (from a in ent.PRODUCTO where a.RUT_PROVEEDOR.Equals(p.RUT_PROVEEDOR) select a).ToList();
        }

        public List<PRODUCTO> ListarProducto()
        {
            return ent.PRODUCTO.ToList<PRODUCTO>();
        }

        public List<FAMILIA> ListarFamilia()
        {
            return ent.FAMILIA.ToList<FAMILIA>();
        }

        public bool EditarProducto(PRODUCTO producto)
        {
            PRODUCTO P = ent.PRODUCTO.FirstOrDefault(objeto =>
            objeto.ID_PRODUCTO.Equals(producto.ID_PRODUCTO));

            if (P != null)
            {
                P.ID_FAMILIA = producto.ID_FAMILIA;
                P.NOMBRE_PRODUCTO = producto.NOMBRE_PRODUCTO;
                P.PRECIO_PRODUCTO = producto.PRECIO_PRODUCTO;
                P.STOCK_CRITICO_PRODUCTO = producto.STOCK_CRITICO_PRODUCTO;
                P.STOCK_PRODUCTO = producto.STOCK_PRODUCTO;
                P.FECHA_VENCIMIENTO_PRODUCTO = producto.FECHA_VENCIMIENTO_PRODUCTO;
                P.DESCRIPCION_PRODUCTO = producto.DESCRIPCION_PRODUCTO;
                P.UNIDAD_MEDIDA = producto.UNIDAD_MEDIDA;

                ent.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public PRODUCTO BuscarProducto(long ID_PRODUCTO) {
            PRODUCTO p = ent.PRODUCTO.FirstOrDefault(objeto => objeto.ID_PRODUCTO.Equals(ID_PRODUCTO));
            return p;
        }

        public bool EliminarProducto(PRODUCTO producto)
        {
            PRODUCTO P = ent.PRODUCTO.FirstOrDefault(objeto =>
            objeto.ID_PRODUCTO.Equals(producto.ID_PRODUCTO));

            if (P != null)
            {
                ent.PRODUCTO.Remove(P);
                ent.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ExisteProducto(PRODUCTO producto)
        {
            PRODUCTO p = ent.PRODUCTO.FirstOrDefault(objeto =>
            objeto.ID_PRODUCTO.Equals(producto.ID_PRODUCTO));
            if (p != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public PRODUCTO obtenerProducto(PRODUCTO producto)
        {

            PRODUCTO p = ent.PRODUCTO.FirstOrDefault(objeto =>
            objeto.ID_PRODUCTO.Equals(producto.ID_PRODUCTO));

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
