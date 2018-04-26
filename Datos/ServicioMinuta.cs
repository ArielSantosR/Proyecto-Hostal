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
    }
}
