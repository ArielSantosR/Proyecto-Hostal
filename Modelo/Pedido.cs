using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class Pedido
    {
        public short NUMERO_PEDIDO { get; set; }
        public System.DateTime FECHA_PEDIDO { get; set; }
        public string ESTADO_PEDIDO { get; set; }
        public int RUT_EMPLEADO { get; set; }
        public Nullable<short> NUMERO_RECEPCION { get; set; }
        public int RUT_PROVEEDOR { get; set; }
    }
}
