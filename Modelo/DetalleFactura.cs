using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo {
    [Serializable]
    public class DetalleFactura{
        public int ID_DETALLE_FACTURA { get; set; }
        public string DESCRIPCION_DETALLE { get; set; }
        public int CANTIDAD { get; set; }
        public long VALOR_TOTAL { get; set; }
        public int ID_FACTURA { get; set; }
        public int VALOR_UNITARIO { get; set; }
        public long? VALOR_DESC { get; set; }

        public bool Crear() {
            ServicioPagos serv = new ServicioPagos();
            DETALLE_FACTURA datos = new DETALLE_FACTURA();
            datos.CANTIDAD = this.CANTIDAD;
            datos.DESCRIPCION_DETALLE = this.DESCRIPCION_DETALLE;
            datos.VALOR_DESC = this.VALOR_DESC;
            datos.VALOR_TOTAL = this.VALOR_TOTAL;
            datos.VALOR_UNITARIO = this.VALOR_UNITARIO;

            return serv.AgregarDetalleFactura(datos);
        }
    }
}
