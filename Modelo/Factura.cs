using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo {
    [Serializable]
    public class Factura {
        public int ID_FACTURA { get; set; }
        public long VALOR_NETO_FACTURA { get; set; }
        public long VALOR_IVA_FACTURA { get; set; }
        public decimal? VALOR_DESC_FACTURA { get; set; }
        public long VALOR_TOTAL_FACTURA { get; set; }
        public DateTime FECHA_EMISION_FACTURA { get; set; }
        public int RUT_CLIENTE { get; set; }
        public int RUT_EMPLEADO { get; set; }
        public string METODO_PAGO { get; set; }
        public short? NUMERO_ORDEN { get; set; }

        public bool BuscarFactura () {
            ServicioPagos serv = new ServicioPagos();
            FACTURA dato = serv.BuscarFactura(this.ID_FACTURA);
            if (dato != null) {
                this.FECHA_EMISION_FACTURA = dato.FECHA_EMISION_FACTURA;
                this.METODO_PAGO = dato.METODO_PAGO;
                this.NUMERO_ORDEN = dato.NUMERO_ORDEN;
                this.RUT_CLIENTE = dato.RUT_CLIENTE;
                this.RUT_EMPLEADO = dato.RUT_EMPLEADO;
                this.VALOR_DESC_FACTURA = dato.VALOR_DESC_FACTURA;
                this.VALOR_IVA_FACTURA = dato.VALOR_IVA_FACTURA;
                this.VALOR_NETO_FACTURA = dato.VALOR_NETO_FACTURA;
                this.VALOR_TOTAL_FACTURA = dato.VALOR_TOTAL_FACTURA;

                return true;
            } else {
                return false;

            }
        }

        public bool Crear() {
            ServicioPagos serv = new ServicioPagos();
            FACTURA datos = new FACTURA();
            datos.ID_FACTURA = this.ID_FACTURA;
            datos.FECHA_EMISION_FACTURA = this.FECHA_EMISION_FACTURA;
            datos.METODO_PAGO = this.METODO_PAGO;
            datos.NUMERO_ORDEN = this.NUMERO_ORDEN;
            datos.RUT_CLIENTE = this.RUT_CLIENTE;
            datos.RUT_EMPLEADO = this.RUT_EMPLEADO;
            datos.VALOR_DESC_FACTURA = this.VALOR_DESC_FACTURA;
            datos.VALOR_IVA_FACTURA = this.VALOR_IVA_FACTURA;
            datos.VALOR_NETO_FACTURA = this.VALOR_NETO_FACTURA;
            datos.VALOR_TOTAL_FACTURA = this.VALOR_TOTAL_FACTURA;

            return serv.AgregarFactura(datos);
        }
    }
}
