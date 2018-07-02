using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo {
    [Serializable]
    public class DetalleFacturaCollection : List<DetalleFactura>{

        public static List<DetalleFactura> ListarDetalleFacturas() {
            ServicioPagos serv = new ServicioPagos();
            return GenerarLista(serv.ListarDetalleFacturas());
        }

        private static List<DetalleFactura> GenerarLista(List<DETALLE_FACTURA> listDatos) {
            List<DetalleFactura> list = new List<DetalleFactura>();
            DetalleFactura detalle;
            foreach (DETALLE_FACTURA dato in listDatos) {
                detalle = new DetalleFactura();
                detalle.CANTIDAD = dato.CANTIDAD;
                detalle.DESCRIPCION_DETALLE = dato.DESCRIPCION_DETALLE;
                detalle.ID_DETALLE_FACTURA = dato.ID_DETALLE_FACTURA;
                detalle.ID_FACTURA = dato.ID_FACTURA;
                detalle.VALOR_TOTAL = dato.VALOR_TOTAL;
                detalle.VALOR_UNITARIO = dato.VALOR_UNITARIO;
                
                list.Add(detalle);
            }
            return list;
        }
    }
}
