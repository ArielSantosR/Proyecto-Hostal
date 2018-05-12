using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class DetalleRecepcion
    {
        public short ID_DETALLE_RECEPCION { get; set; }
        public int CANTIDAD { get; set; }
        public long ID_PRODUCTO { get; set; }
        public short NUMERO_RECEPCION { get; set; }
    }
}
