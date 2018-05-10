using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class Habitacion
    {
        public short NUMERO_HABITACION { get; set; }
        public string ESTADO_HABITACION { get; set; }
        public short ID_TIPO_HABITACION { get; set; }
        public Nullable<int> RUT_CLIENTE { get; set; }
        public short ID_CATEGORIA_HABITACION { get; set; }
    }
}
