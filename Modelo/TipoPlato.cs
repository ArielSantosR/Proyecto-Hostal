using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class TipoPlato
    {
        public short ID_TIPO_PLATO { get; set; }
        public string NOMBRE_TIPO_PLATO { get; set; }

        public bool BuscarTipo () {
            ServicioMinuta serv = new ServicioMinuta();
            TIPO_PLATO dato = serv.BuscarTipoPlato(this.ID_TIPO_PLATO);
            if (dato != null) {
                this.NOMBRE_TIPO_PLATO = dato.NOMBRE_TIPO_PLATO;

                return true;
            } else {
                return false;

            }
        }
    }
}
