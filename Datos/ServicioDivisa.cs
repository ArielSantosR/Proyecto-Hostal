using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos {
    public class ServicioDivisa {

        private HostalEntities ent = new HostalEntities();

        public bool EditarDivisa (DIVISA dDatos) {
            DIVISA d = ent.DIVISA.FirstOrDefault(x => x.ID_DIVISA == dDatos.ID_DIVISA);
            if (d != null) {
                d.NOMBRE_DIVISA = dDatos.NOMBRE_DIVISA;
                d.PRECIO_DIVISA = d.PRECIO_DIVISA;

                ent.SaveChanges();
                return true;
            } else {
                return false;
            }
        }

        public List<DIVISA> ListarDivisa () {
            return ent.DIVISA.ToList();
        }
    }
}
