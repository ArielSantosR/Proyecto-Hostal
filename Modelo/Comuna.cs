using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Modelo
{
    [Serializable]
    public class Comuna
    {
        public short Id_Comuna { get; set; }
        public string Nombre { get; set; }
        public short Id_Region { get; set; }

        public bool BuscarComuna() {
            ServicioCliente serv = new ServicioCliente();
            COMUNA datos = serv.BuscarComuna(this.Id_Comuna);
            if (datos != null) {
                this.Nombre = datos.NOMBRE_COMUNA;
                this.Id_Region = datos.ID_REGION;

                return true;
            }
            else {
                return false;

            }
        }
    }
}
