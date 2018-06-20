using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class DetalleHabitacion
    {
        public short ID_DETALLE_H { get; set; }
        public short NUMERO_HABITACION { get; set; }
        public int RUT_CLIENTE { get; set; }
        public int RUT_HUESPED { get; set; }
        public short ID_PENSION { get; set; }
        public System.DateTime FECHA_LLEGADA { get; set; }
        public System.DateTime FECHA_SALIDA { get; set; }
    }
}
