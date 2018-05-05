using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class Producto

        
    {
        public short ID_PRODUCTO { get; set; }
        public string NOMBRE_PRODUCTO { get; set; }
        public DateTime? FECHA_VENCIMIENTO_PRODUCTO { get; set; }
        public short STOCK_PRODUCTO { get; set; }
        public short STOCK_CRITICO_PRODUCTO { get; set; }
        public string DESCRIPCION_PRODUCTO { get; set; }
        public int PRECIO_PRODUCTO { get; set; }
        public short ID_FAMILIA { get; set; }
        public int RUT_PROVEEDOR { get; set; }
        public short ID_PRODUCTO_SEQ { get; set; }
    }
}
