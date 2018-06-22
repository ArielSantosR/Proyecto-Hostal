using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelo_Vistas {
    [Serializable]
    public class V_Cliente_Ordenes {

        public string RUT { get; set; }
        public string NOMBRE_CLIENTE { get; set; }
        public string GIRO { get; set; }
        public decimal? ORDENES_PENDIENTES { get; set; }
        public decimal? ORDENES_ASIGNADAS { get; set; }
        public decimal? ORDENES_ACEPTADAS { get; set; }
        public decimal? ORDENES_RECHAZADAS { get; set; }
        public decimal? ORDENES_CANCELADAS { get; set; }
        public decimal? CANTIDAD_ORDENES { get; set; }
    }
}
