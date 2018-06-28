using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo {
    [Serializable]
    public class Boleta {
        public int ID_BOLETA { get; set; }
        public decimal? VALOR_DESC_BOLETA { get; set; }
        public long VALOR_TOTAL_BOLETA { get; set; }
        public DateTime FECHA_EMISION_BOLETA { get; set; }
        public int RUT_HUESPED { get; set; }
        public int RUT_EMPLEADO { get; set; }
    }
}
