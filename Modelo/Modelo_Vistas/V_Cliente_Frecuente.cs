using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelo_Vistas {
    [Serializable]
    public class V_Cliente_Frecuente {

        public string RUT { get; set; }
        public string NOMBRE_CLIENTE { get; set; }
        public string GIRO { get; set; }
        public decimal? CANTIDAD_ORDENES { get; set; }
    }
}
