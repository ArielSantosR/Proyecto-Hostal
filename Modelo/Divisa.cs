using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class Divisa
    {
        public short ID_DIVISA { get; set; }
        public string NOMBRE_DIVISA { get; set; }
        public decimal PRECIO_DIVISA { get; set; }
    }
}
