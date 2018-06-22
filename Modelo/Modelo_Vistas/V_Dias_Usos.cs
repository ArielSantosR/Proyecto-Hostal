using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelo_Vistas {
    [Serializable]
    public class V_Dias_Usos {

        public short NUMERO_HABITACION { get; set; }
        public decimal? DIAS_USO { get; set; }
    }
}
