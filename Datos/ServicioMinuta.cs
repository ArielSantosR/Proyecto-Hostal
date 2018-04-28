using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ServicioMinuta
    {
        HostalEntities ent = new HostalEntities();

        public List<TIPO_PLATO> ListarTipoPlato()
        {
            return ent.TIPO_PLATO.ToList<TIPO_PLATO>();
        }

        public List<CATEGORIA> ListarCategoria()
        {
            return ent.CATEGORIA.ToList();
        }

        public bool AgregarPlato(PLATO p)
        {
            ent.PLATO.Add(p);
            ent.SaveChanges();
            return true;
        }

        public bool existePlato(PLATO plato)
        {
            PLATO p = ent.PLATO.FirstOrDefault(objeto =>
            objeto.NOMBRE_PLATO.Equals(plato.NOMBRE_PLATO));
            if (p != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
