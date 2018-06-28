using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo {
    [Serializable]
    public class FacturaCollection : Factura{

        public static List<Factura> ListarFacturas() {
            ServicioPagos serv = new ServicioPagos();
            return GenerarLista(serv.ListarFacturas());
        }

        private static List<Factura> GenerarLista(List<FACTURA> listDatos) {
            List<Factura> list = new List<Factura>();
            Factura factura;
            foreach (FACTURA dato in listDatos) {
                factura = new Factura();
                factura.FECHA_EMISION_FACTURA = dato.FECHA_EMISION_FACTURA;
                factura.ID_FACTURA = dato.ID_FACTURA;
                factura.NUMERO_ORDEN = dato.NUMERO_ORDEN;
                factura.RUT_CLIENTE = dato.RUT_CLIENTE;
                factura.RUT_EMPLEADO = dato.RUT_EMPLEADO;
                factura.VALOR_DESC_FACTURA = dato.VALOR_DESC_FACTURA;
                factura.VALOR_IVA_FACTURA = dato.VALOR_IVA_FACTURA;
                factura.VALOR_NETO_FACTURA = dato.VALOR_NETO_FACTURA;
                factura.VALOR_TOTAL_FACTURA = dato.VALOR_TOTAL_FACTURA;

                list.Add(factura);
            }
            return list;
        }
    }
}
