using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo {
    [Serializable]
    public class DetalleBoleta {

        public int ID_DETALLE_BOLETA { get; set; }
        public string DESCRIPCION_DETALLE { get; set; }
        public int CANTIDAD { get; set; }
        public int VALOR_TOTAL { get; set; }
        public int ID_BOLETA { get; set; }

    }
}
