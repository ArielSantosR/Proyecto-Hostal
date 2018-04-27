using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class Plato
    {
        public short ID_PLATO { get; set; }
        public string NOMBRE_PLATO { get; set; }
        public int PRECIO_PLATO { get; set; }
        public short ID_CATEGORIA { get; set; }
        public short ID_TIPO_PLATO { get; set; }
    }
}
