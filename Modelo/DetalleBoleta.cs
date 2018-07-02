using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo {
    [Serializable]
    public class DetalleBoleta {

        public int ID_DETALLE_BOLETA { get; set; }
        public string DESCRIPCION_DETALLE { get; set; }
        public int CANTIDAD { get; set; }
        public int VALOR_TOTAL { get; set; }
        public int ID_BOLETA { get; set; }

        public bool Crear () {
            ServicioPagos serv = new ServicioPagos();
            DETALLE_BOLETA datos = new DETALLE_BOLETA();
            datos.ID_BOLETA = this.ID_BOLETA;
            datos.DESCRIPCION_DETALLE = this.DESCRIPCION_DETALLE;
            datos.CANTIDAD = this.CANTIDAD;
            datos.VALOR_TOTAL = this.VALOR_TOTAL;
            datos.ID_DETALLE_BOLETA = this.ID_DETALLE_BOLETA;

            return serv.AgregarDetalleBoleta(datos);
        }
    }
}
