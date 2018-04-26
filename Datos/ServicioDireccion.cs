using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ServicioDireccion
    {
        HostalEntities ent = new HostalEntities();

        public List<PAIS> ListarPaises()
        {
            return ent.PAIS.ToList<PAIS>();
        }

        public List<REGION> ListarRegion()
        {
            return ent.REGION.ToList<REGION>();
        }

        public List<COMUNA> ListarComuna()
        {
            return ent.COMUNA.ToList<COMUNA>();
        }

    }
}
