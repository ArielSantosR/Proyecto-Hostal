using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class Huesped
    {
        public int RUT_HUESPED { get; set; }
        public string DV_HUESPED { get; set; }
        public string PNOMBRE_HUESPED { get; set; }
        public string SNOMBRE_HUESPED { get; set; }
        public string APP_PATERNO_HUESPED { get; set; }
        public string APP_MATERNO_HUESPED { get; set; }
        public Nullable<long> TELEFONO_HUESPED { get; set; }
        public string REGISTRADO { get; set; }
        public Nullable<short> NUMERO_HABITACION { get; set; }
        public int RUT_CLIENTE { get; set; }

    }
}
