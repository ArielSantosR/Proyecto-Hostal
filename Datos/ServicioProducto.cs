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

        public List<PRODUCTO> ListarProducto()
        {
            return ent.PRODUCTO.ToList<PRODUCTO>();
        }
        public List<FAMILIA> ListarFamilia()
        {
            return ent.FAMILIA.ToList<FAMILIA>();
        }

    }
}
