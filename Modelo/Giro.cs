using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo {
    [Serializable]
    public class Giro {
        public short ID_GIRO { get; set; }
        public string NOMBRE_GIRO { get; set; }

        public bool BuscarGiro () {
            ServicioCliente serv = new ServicioCliente();
            GIRO datos = serv.BuscarGiro(this.ID_GIRO);
            if (datos != null) {
                this.NOMBRE_GIRO = datos.NOMBRE_GIRO;

                return true;
            } else {
                return false;
            }
        }
    }
}
