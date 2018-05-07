using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class DetallePedido
    {
        public short ID_DETALLE_PEDIDO { get; set; }
        public int CANTIDAD { get; set; }
        public Nullable<short> NUMERO_PEDIDO { get; set; }
        public long ID_PRODUCTO { get; set; }
    }
}
