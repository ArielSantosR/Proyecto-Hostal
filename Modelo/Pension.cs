using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class Pension
    {
        public short ID_PENSION { get; set; }
        public string NOMBRE_PENSION { get; set; }
        public int VALOR_PENSION { get; set; }
        public Nullable<short> NUMERO_HABITACION { get; set; }

        public string NombreYPrecio
        {
            get { return this.NOMBRE_PENSION + " $" + this.VALOR_PENSION; }
        }
    }
}
