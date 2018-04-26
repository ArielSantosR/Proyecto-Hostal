using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class TipoHabitacion
    {
        public short ID_TIPO_HABITACION { get; set; }
        public string NOMBRE_TIPO_HABITACION { get; set; }
        public short CANTIDAD_PASAJERO { get; set; }
    }
}
