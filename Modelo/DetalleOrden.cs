using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class DetalleOrden
    {
        public int ID_DETALLE { get; set; }
        public short NUMERO_ORDEN { get; set; }
        public int RUT_HUESPED { get; set; }
        public short ID_CATEGORIA_HABITACION { get; set; }
        public short ID_PENSION { get; set; }
    }
}
