using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{

    [Serializable]
   public class Minuta
    {
        public short ID_PENSION { get; set; }
        public string NOMBRE_PENSION { get; set; }
        public int VALOR_PENSION { get; set; }
        public string HABILITADO { get; set; }

        public bool BuscarMinuta() {
            ServicioMinuta serv = new ServicioMinuta();
            PENSION datos = serv.BuscarPension(this.ID_PENSION);
            if (datos != null) {
                this.NOMBRE_PENSION = datos.NOMBRE_PENSION;
                this.VALOR_PENSION = datos.VALOR_PENSION;
                this.HABILITADO = datos.HABILITADO;

                return true;
            }
            else {
                return false;

            }
        }

        public string NombreYPrecio
        {
            get { return this.NOMBRE_PENSION + " $" + this.VALOR_PENSION; }
        }

    }
}
