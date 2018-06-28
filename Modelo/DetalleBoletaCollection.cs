using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo {
    [Serializable]
    public class DetalleBoletaCollection : List<DetalleBoleta>{

        public static List<DetalleBoleta> ListarDetallesBoleta() {
            ServicioPagos serv = new ServicioPagos();
            return GenerarLista(serv.ListarDetalleBoletas());
        }

        private static List<DetalleBoleta> GenerarLista(List<DETALLE_BOLETA> listDatos) {
            List<DetalleBoleta> list = new List<DetalleBoleta>();
            DetalleBoleta detalle;
            foreach (DETALLE_BOLETA dato in listDatos) {
                detalle = new DetalleBoleta();
                detalle.ID_DETALLE_BOLETA = dato.ID_DETALLE_BOLETA;
                detalle.ID_BOLETA = dato.ID_BOLETA;
                detalle.CANTIDAD = dato.CANTIDAD;
                detalle.DESCRIPCION_DETALLE = dato.DESCRIPCION_DETALLE;
                detalle.VALOR_TOTAL = dato.VALOR_TOTAL;

                list.Add(detalle);
            }
            return list;
        }
    }
}
