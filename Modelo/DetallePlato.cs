using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class DetallePlato
    {
        public short ID_DETALLE_PLATOS { get; set; }
        public short CANTIDAD { get; set; }
        public short ID_PLATO { get; set; }
        public short ID_PENSION { get; set; }
    }
}
