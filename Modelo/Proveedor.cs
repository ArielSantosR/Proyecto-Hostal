using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class Proveedor
    {
        public int RUT_PROVEEDOR { get; set; }
        public string DV_PROVEEDOR { get; set; }
        public string PNOMBRE_PROVEEDOR { get; set; }
        public string SNOMBRE_PROVEEDOR { get; set; }
        public string APP_PATERNO_PROVEEDOR { get; set; }
        public string APP_MATERNO_PROVEEDOR { get; set; }
        public short ID_TIPO_PROVEEDOR { get; set; }
        public short ID_USUARIO { get; set; }
    }
}
