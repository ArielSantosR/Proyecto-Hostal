using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class Familia
    {
        public short ID_FAMILIA { get; set; }
        public string NOMBRE_FAMILIA { get; set; }

        public bool BuscarFamilia () {
            ServicioProducto serv = new ServicioProducto();
            FAMILIA dato = serv.BuscarFamilia(this.ID_FAMILIA);
            if (dato != null) {
                this.NOMBRE_FAMILIA = dato.NOMBRE_FAMILIA;

                return true;
            } else {
                return false;

            }
        }
    }
}
