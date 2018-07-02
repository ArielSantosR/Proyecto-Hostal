using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo {
    [Serializable]
    public class Boleta {
        public int ID_BOLETA { get; set; }
        public decimal? VALOR_DESC_BOLETA { get; set; }
        public long VALOR_TOTAL_BOLETA { get; set; }
        public DateTime FECHA_EMISION_BOLETA { get; set; }
        public int RUT_HUESPED { get; set; }
        public int RUT_EMPLEADO { get; set; }

        public bool BuscarBoleta () {
            ServicioPagos serv = new ServicioPagos();
            BOLETA dato = serv.BuscarBoleta(this.ID_BOLETA);
            if (dato != null) {
                this.FECHA_EMISION_BOLETA = dato.FECHA_EMISION_BOLETA;
                this.RUT_EMPLEADO = dato.RUT_EMPLEADO;
                this.RUT_HUESPED = dato.RUT_HUESPED;
                this.VALOR_DESC_BOLETA = dato.VALOR_DESC_BOLETA;
                this.VALOR_TOTAL_BOLETA = dato.VALOR_TOTAL_BOLETA;

                return true;
            } else {
                return false;

            }
        }

        public bool Crear () {
            ServicioPagos serv = new ServicioPagos();
            BOLETA datos = new BOLETA();
            datos.ID_BOLETA = this.ID_BOLETA;
            datos.FECHA_EMISION_BOLETA = this.FECHA_EMISION_BOLETA;
            datos.RUT_EMPLEADO = this.RUT_EMPLEADO;
            datos.RUT_HUESPED = this.RUT_HUESPED;
            datos.VALOR_DESC_BOLETA = this.VALOR_DESC_BOLETA;
            datos.VALOR_TOTAL_BOLETA = this.VALOR_TOTAL_BOLETA;

            return serv.AgregarBoleta(datos);
        }
    }
}
