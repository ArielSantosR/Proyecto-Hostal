using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class TipoProveedor
    {
        public short ID_TIPO_PROVEEDOR { get; set; }
        public string NOMBRE_TIPO { get; set; }
    }
}
