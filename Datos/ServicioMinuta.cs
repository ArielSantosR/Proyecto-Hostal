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

        public bool AgregarMinuta(PENSION minuta)
        {
            ent.PENSION.Add(minuta);
            ent.SaveChanges();
            return true;
        }

        public List<PENSION> ListarMinuta()
        {
            var lista = (from consulta in ent.PENSION
                         orderby consulta.ID_PENSION
                         select consulta).ToList();
            return lista;
        }

    }
}
