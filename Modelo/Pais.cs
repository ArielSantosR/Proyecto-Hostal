using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class Pais
    {
        public short ID_PAIS { get; set; }
        public string NOMBRE_PAIS { get; set; }

        public bool BuscarPais() {
            ServicioCliente serv = new ServicioCliente();
            PAIS datos = serv.BuscarPais(this.ID_PAIS);
            if (datos != null) {
                this.NOMBRE_PAIS = datos.NOMBRE_PAIS;

                return true;
            }
            else {
                return false;

            }
        }
    }
}
