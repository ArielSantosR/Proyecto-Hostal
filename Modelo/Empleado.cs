using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class Empleado
    {
        public int RUT_EMPLEADO { get; set; }
        public string DV_EMPLEADO { get; set; }
        public string PNOMBRE_EMPLEADO { get; set; }
        public string SNOMBRE_EMPLEADO { get; set; }
        public string APP_PATERNO_EMPLEADO { get; set; }
        public string APP_MATERNO_EMPLEADO { get; set; }
        public short ID_USUARIO { get; set; }
    }
}
