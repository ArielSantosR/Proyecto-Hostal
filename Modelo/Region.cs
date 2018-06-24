using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class Region
    {
        public short Id_Region { get; set; }
        public string Nombre { get; set; }
        public short Id_Pais { get; set; }

        public bool BuscarRegion() {
            ServicioCliente serv = new ServicioCliente();
            REGION datos = serv.BuscarRegion(this.Id_Region);
            if (datos != null) {
                this.Nombre = datos.NOMBRE_REGION;
                this.Id_Pais = datos.ID_PAIS;

                return true;
            }
            else {
                return false;

            }
        }
    }
}
