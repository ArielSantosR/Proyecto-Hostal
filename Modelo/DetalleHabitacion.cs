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
    }
}
